using Contract.Architecture.Contract.Persistence.Model.Users;
using System;

namespace Contract.Architecture.Logic.Model.Users
{
    internal class DbEmailUser : IDbEmailUser
    {
        public string Email { get; set; }

        public Guid Id { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
    }
}