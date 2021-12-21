﻿using System;

namespace Finanzuebersicht.Backend.Core.Contract.Persistence.Modules.UserManagement.EmailUserPasswortResetTokens
{
    public interface IDbEmailUserPasswordResetToken
    {
        DateTime ExpiresOn { get; set; }

        string Token { get; set; }

        Guid EmailUserId { get; set; }
    }
}