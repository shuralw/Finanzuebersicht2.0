﻿using Finanzuebersicht.Backend.Core.Contract.Logic.Modules.UserManagement.EmailUserPasswordReset;
using System.ComponentModel.DataAnnotations;

namespace Finanzuebersicht.Backend.Core.API.Modules.Users.EmailUserPasswordReset
{
    public class BrowserInfo : IBrowserInfo
    {
        [Required]
        [StringLength(64, MinimumLength = 1)]
        public string Browser { get; set; }

        [Required]
        [StringLength(64, MinimumLength = 1)]
        public string OperatingSystem { get; set; }
    }
}