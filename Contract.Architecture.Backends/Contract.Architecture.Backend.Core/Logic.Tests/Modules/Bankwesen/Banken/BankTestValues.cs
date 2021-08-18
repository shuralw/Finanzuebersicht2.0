using System;

namespace Contract.Architecture.Backend.Core.Logic.Tests.Modules.Bankwesen.Banken
{
    public class BankTestValues
    {
        public static readonly Guid IdDefault = Guid.Parse("a46b6780-a56a-4c7c-a5eb-623352c63561");
        public static readonly Guid IdDefault2 = Guid.Parse("43a42bb4-d035-488e-919b-3d29d3559b7a");
        public static readonly Guid IdForCreate = Guid.Parse("33f80f6a-802f-43ce-9f64-59f60adf2102");

        public static readonly string NameDefault = "NameDefault";
        public static readonly string NameDefault2 = "NameDefault2";
        public static readonly string NameForCreate = "NameForCreate";
        public static readonly string NameForUpdate = "NameForUpdate";

        public static readonly DateTime EroeffnetAmDefault = new DateTime(2019, 8, 11);
        public static readonly DateTime EroeffnetAmDefault2 = new DateTime(2020, 10, 19);
        public static readonly DateTime EroeffnetAmForCreate = new DateTime(2013, 10, 25);
        public static readonly DateTime EroeffnetAmForUpdate = new DateTime(2012, 12, 28);

        public static readonly bool IsPleiteDefault = false;
        public static readonly bool IsPleiteDefault2 = false;
        public static readonly bool IsPleiteForCreate = false;
        public static readonly bool IsPleiteForUpdate = false;
    }
}