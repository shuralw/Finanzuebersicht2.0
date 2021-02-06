using Contract.Architecture.Contract.Persistence.Model.Users.EmailUsers;
using System;

namespace Contract.Architecture.Logic.Model.Users.EmailUsers
{
    internal class DbEmailUser : IDbEmailUser
    {
        public string Email { get; set; }

        public Guid Id { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
    }
}