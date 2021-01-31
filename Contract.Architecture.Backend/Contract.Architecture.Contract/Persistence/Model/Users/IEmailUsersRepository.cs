﻿using System;
using System.Collections.Generic;

namespace Contract.Architecture.Contract.Persistence.Model.Users
{
    public interface IEmailUsersRepository
    {
        void CreateEmailUser(IDbEmailUser emailUser);

        void DeleteEmailUser(Guid emailUserId);

        IDbEmailUser GetEmailUser(Guid emailUserId);

        IDbEmailUser GetEmailUser(string mail);

        IEnumerable<IDbEmailUser> GetEmailUsers();

        void UpdateEmailUser(IDbEmailUser emailUser);
    }
}