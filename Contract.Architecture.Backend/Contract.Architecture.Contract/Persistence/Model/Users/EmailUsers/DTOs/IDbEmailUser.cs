using System;

namespace Contract.Architecture.Contract.Persistence.Model.Users.EmailUsers
{
    public interface IDbEmailUser
    {
        Guid Id { get; set; }

        string Email { get; set; }

        string PasswordHash { get; set; }

        string PasswordSalt { get; set; }
    }
}