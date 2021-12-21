using Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.UserManagement.EmailUsers;
using System;

namespace Finanzuebersicht.Backend.Core.Logic.Modules.UserManagement.EmailUsers
{
    internal class DbEmailUser : IDbEmailUser
    {
        public string Email { get; set; }

        public Guid Id { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
    }
}