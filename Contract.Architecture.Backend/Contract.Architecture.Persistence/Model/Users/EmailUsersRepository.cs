using Contract.Architecture.Contract.Persistence.Model.Users;
using Contract.Architecture.Persistence.Model.Users.DTOs;
using Contract.Architecture.Persistence.Model.Users.EfCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Contract.Architecture.Persistence.Model.Users
{
    internal class EmailUsersRepository : IEmailUsersRepository
    {
        private readonly UsersDbContext context;

        public EmailUsersRepository(UsersDbContext context)
        {
            this.context = context;
        }

        public void CreateEmailUser(IDbEmailUser emailUser)
        {
            this.context.EmailUsers.Add(DbEmailUser.ToEfEmailUser(emailUser));
            this.context.SaveChanges();
        }

        public void DeleteEmailUser(Guid emailUserId)
        {
            this.context.EmailUsers.Remove(this.context.EmailUsers.Find(emailUserId));
            this.context.SaveChanges();
        }

        public IDbEmailUser GetEmailUser(Guid emailUserId)
        {
            EfEmailUser efEmailUser = this.context.EmailUsers
                .Where(emailUser => emailUser.Id == emailUserId)
                .FirstOrDefault();

            return DbEmailUser.FromEfEmailUser(efEmailUser);
        }

        public IDbEmailUser GetEmailUser(string mail)
        {
            EfEmailUser efEmailUser = this.context.EmailUsers
                .Where(u => u.Email == mail)
                .FirstOrDefault();

            return DbEmailUser.FromEfEmailUser(efEmailUser);
        }

        public IEnumerable<IDbEmailUser> GetEmailUsers()
        {
            return this.context.EmailUsers
                .OrderBy(emailUser => emailUser.Email)
                .Select(emailUser => DbEmailUser.FromEfEmailUser(emailUser));
        }

        public void UpdateEmailUser(IDbEmailUser emailUser)
        {
            var efEmailUser = this.context.EmailUsers.Find(emailUser.Id);
            DbEmailUser.UpdateEfEmailUser(efEmailUser, emailUser);
            this.context.EmailUsers.Update(efEmailUser);

            this.context.SaveChanges();
        }
    }
}