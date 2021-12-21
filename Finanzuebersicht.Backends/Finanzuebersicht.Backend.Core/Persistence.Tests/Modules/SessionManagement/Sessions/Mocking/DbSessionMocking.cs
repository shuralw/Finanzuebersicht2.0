﻿using Finanzuebersicht.Backend.Core.Persistence.Modules.SessionManagement.Sessions;
using System;

namespace Finanzuebersicht.Backend.Core.Persistence.Tests.Modules.SessionManagement.Sessions
{
    internal static class DbSessionMocking
    {
        internal static DbSession Create()
        {
            return Create(Guid.NewGuid().ToString());
        }

        internal static DbSession Create(string token)
        {
            return new DbSession()
            {
                Token = token,
                ExpiresOn = DateTime.Now.AddMinutes(30),
                EmailUserId = null,
            };
        }

        internal static DbSession CreateForEmailUser(Guid emailUserId, string token)
        {
            return new DbSession()
            {
                Token = token,
                ExpiresOn = DateTime.Now.AddMinutes(30),
                EmailUserId = emailUserId,
            };
        }
    }
}