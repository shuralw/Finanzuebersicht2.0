using Contract.Architecture.Contract.Persistence.Model.Users;
using Contract.Architecture.Persistence.Model.Users.DTOs;
using Contract.Architecture.Persistence.Model.Users.EfCore;
using System;
using System.Linq;

namespace Contract.Architecture.Persistence.Model.Users
{
    internal class EmailUserPasswordResetTokenRepository : IEmailUserPasswordResetTokenRepository
    {
        private readonly UsersDbContext context;

        public EmailUserPasswordResetTokenRepository(UsersDbContext context)
        {
            this.context = context;
        }

        public IDbEmailUserPasswordResetToken GetToken(string token)
        {
            EfEmailUserPasswordResetToken emailUserPasswordResetToken = this.context.EmailUserPasswordResetTokens.Find(token);
            return DbEmailUserPasswordResetToken.FromEfEmailUserPasswordResetToken(emailUserPasswordResetToken);
        }

        public void CreateToken(IDbEmailUserPasswordResetToken emailUserPasswordResetToken)
        {
            EfEmailUserPasswordResetToken efEmailUserPasswordResetToken = DbEmailUserPasswordResetToken.ToEfEmailUserPasswordResetToken(emailUserPasswordResetToken);
            this.context.EmailUserPasswordResetTokens.Add(efEmailUserPasswordResetToken);
            this.context.SaveChanges();
        }

        public void DeleteToken(string token)
        {
            this.context.EmailUserPasswordResetTokens.Remove(this.context.EmailUserPasswordResetTokens.Find(token));
            this.context.SaveChanges();
        }

        public void DeleteToken(DateTime olderThan)
        {
            var tokensToDelete = this.context.EmailUserPasswordResetTokens
                .Where(attempt => attempt.ExpiresOn < olderThan);

            this.context.EmailUserPasswordResetTokens.RemoveRange(tokensToDelete);
            this.context.SaveChanges();
        }
    }
}