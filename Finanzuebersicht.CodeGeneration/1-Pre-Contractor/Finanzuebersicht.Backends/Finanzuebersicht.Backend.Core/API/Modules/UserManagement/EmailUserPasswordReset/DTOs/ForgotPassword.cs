﻿using System.ComponentModel.DataAnnotations;

namespace Finanzuebersicht.Backend.Core.API.Modules.Users.EmailUserPasswordReset
{
    public class ForgotPassword
    {
        [Required]
        [EmailAddress]
        [StringLength(256, MinimumLength = 1)]
        public string Email { get; set; }
    }
}