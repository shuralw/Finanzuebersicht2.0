using Contract.Architecture.Backend.Core.Contract.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Contract.Architecture.Backend.Core.Persistence.Tools.Pagination
{
    internal class PaginationQueries
    {
        internal static IQueryable<T> FilterByString<T>(
            IQueryable<T> query,
            string queryString,
            IPaginationFilterItem filterItem)
        {
            if (filterItem == null)
            {
                return query;
            }

            // prop =    x => x.Filiale.BankId
            var param = Expression.Parameter(typeof(T), "x");
            Expression prop = param;
            foreach (var member in queryString.Split(new char[] { '.', '<' }))
            {
                prop = Expression.PropertyOrField(prop, member);
            }

            var filterValues = GetFilterValues(prop.Type, filterItem);
            var valuesConstant = Expression.Constant(filterValues);
            var method = filterValues.GetType().GetMethod(nameof(List<object>.Contains));
            var body = Expression.Call(valuesConstant, method, prop);
            var expr = Expression.Lambda<Func<T, bool>>(body, param);

            return query.Where(expr);
        }

        internal static IQueryable<T> FilterByStringAny<T>(
            IQueryable<T> query,
            IPaginationFilterItem filterItem)
        {
            if (filterItem == null)
            {
                return query;
            }

            PaginationQueryStep[] querySteps = PaginationQueryStep.Parse(filterItem);

            var param = Expression.Parameter(typeof(T), "x");

            var body = Recursive(querySteps, 0, param, filterItem);

            var expr = Expression.Lambda<Func<T, bool>>(body, param);
            return query.Where(expr);
        }

        private static MethodCallExpression Recursive(
            PaginationQueryStep[] querySteps,
            int anyLevel,
            Expression prop,
            IPaginationFilterItem filterItem)
        {
            PaginationQueryStep currentQueryStep = querySteps[0];
            prop = Expression.PropertyOrField(prop, currentQueryStep.Name);
            if (currentQueryStep.PaginationQueryStepType == PaginationQueryStepType.Property)
            {
                return Recursive(querySteps[1..], anyLevel, prop, filterItem);
            }
            else if (currentQueryStep.PaginationQueryStepType == PaginationQueryStepType.Any)
            {
                return AnyFilterExpression(anyLevel, prop, (param, level) =>
                {
                    return Recursive(querySteps[1..], anyLevel + 1, param, filterItem);
                });
            }
            else
            {
                return ContainsFilterExpression(filterItem, prop);
            }
        }

        private static MethodCallExpression AnyFilterExpression(
            int level,
            Expression prop,
            Func<ParameterExpression, int, MethodCallExpression> innerExprResolver)
        {
            // efFiliale.Kunden
            var propType = prop.Type.GenericTypeArguments.First();

            // Enumerable<EfKunde>.Any(
            var anyMethod =
                typeof(Enumerable)
                    .GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.Instance)
                    .FirstOrDefault(x => x.Name == "Any" && x.GetParameters().Length == 2 && x.GetGenericArguments().Length == 1)
                    .MakeGenericMethod(propType);

            // efKunde
            var innerParam = Expression.Parameter(propType, "x" + level);

            // bankIds.Contains(efKunde.BankId)
            var innerBody = innerExprResolver(innerParam, level);

            // efKunde => bankIds.Contains(efKunde.BankId)
            var innerExpr = Expression.Lambda(innerBody, innerParam);

            // efFiliale.Kunden.Any(efKunde => bankIds.Contains(efKunde.BankId))
            var body = Expression.Call(null, anyMethod, prop, innerExpr);

            return body;
        }

        private static MethodCallExpression ContainsFilterExpression(
            IPaginationFilterItem filterItem,
            Expression prop)
        {
            // bankIds
            var filterValues = GetFilterValues(prop.Type, filterItem);
            var valuesConstant = Expression.Constant(filterValues);

            // List<object>.Contains
            var containsMethod = filterValues.GetType().GetMethod(nameof(List<object>.Contains));

            // bankIds.Contains(efKunde.BankId)
            var body = Expression.Call(valuesConstant, containsMethod, prop);
            return body;
        }

        private static IQueryable<T> Filter<T, TField>(
            IQueryable<T> query,
            Expression<Func<T, TField>> field,
            IPaginationFilterItem filterItem)
        {
            if (filterItem == null)
            {
                return query;
            }

            var member = field.Body as MemberExpression;
            var propInfo = member.Member as PropertyInfo;

            var param = Expression.Parameter(typeof(T), "x");
            var prop = Expression.Property(param, propInfo);

            var filterValues = GetFilterValues(prop.Type, filterItem);
            var valuesConstant = Expression.Constant(filterValues);
            var method = filterValues.GetType().GetMethod(nameof(List<object>.Contains));
            var body = Expression.Call(valuesConstant, method, prop);
            var expr = Expression.Lambda<Func<T, bool>>(body, param);

            return query.Where(expr);
        }

        private static IQueryable<T> Like<T, TField>(
            IQueryable<T> query,
            Expression<Func<T, TField>> field,
            IPaginationFilterItem filterItem)
        {
            if (filterItem == null)
            {
                return query;
            }

            var member = field.Body as MemberExpression;
            var propInfo = member.Member as PropertyInfo;

            var param = Expression.Parameter(typeof(T), "x");
            var prop = Expression.Property(param, propInfo);

            var functions = Expression.Property(null, typeof(EF).GetProperty(nameof(EF.Functions)));
            var method = typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like), new Type[] { functions.Type, typeof(string), typeof(string) });
            var body = Expression.Call(null, method, functions, prop, Expression.Constant(filterItem.PropertyValue));
            var expr = Expression.Lambda<Func<T, bool>>(body, param);

            return query.Where(expr);
        }

        private static IQueryable<T> Sort<T, TField>(IQueryable<T> query, Expression<Func<T, TField>> field, IPaginationSortItem sortItem)
        {
            if (sortItem == null)
            {
                return query;
            }

            switch (sortItem.OrderBy)
            {
                case SortOrder.ASC:
                    return query.OrderBy(field);

                case SortOrder.DESC:
                    return query.OrderByDescending(field);

                default:
                    return query;
            }
        }

        private static object GetPropertyValue(Type type, IPaginationFilterItem filterItem)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    return bool.Parse(filterItem.PropertyValue);

                case TypeCode.DateTime:
                    return DateTime.Parse(filterItem.PropertyValue);

                case TypeCode.Int32:
                    return int.Parse(filterItem.PropertyValue);

                case TypeCode.Double:
                    return double.Parse(filterItem.PropertyValue);

                case TypeCode.String:
                    return filterItem.PropertyValue;

                case TypeCode.Object:
                    if (type == typeof(Guid))
                    {
                        return Guid.Parse(filterItem.PropertyValue);
                    }

                    break;
            }

            return filterItem.PropertyValue;
        }

        private static object GetFilterValues(
            Type type,
            IPaginationFilterItem filterItem)
        {
            string[] propertyValueSplit = filterItem.PropertyValue.Split('|');
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Boolean:
                    return propertyValueSplit.Select(propertyValue => bool.Parse(propertyValue));

                case TypeCode.DateTime:
                    return propertyValueSplit.Select(propertyValue => DateTime.Parse(propertyValue));

                case TypeCode.Int32:
                    return propertyValueSplit.Select(propertyValue => int.Parse(propertyValue));

                case TypeCode.Double:
                    return propertyValueSplit.Select(propertyValue => double.Parse(propertyValue));

                case TypeCode.String:
                    return propertyValueSplit;

                case TypeCode.Object:
                    if (type == typeof(Guid))
                    {
                        return propertyValueSplit.Select(propertyValue => Guid.Parse(propertyValue)).ToList();
                    }

                    break;
            }

            return propertyValueSplit;
        }
    }
}