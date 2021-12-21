﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finanzuebersicht.Backend.Core.Persistence.Modules.SessionManagement.Sessions
{
    [Table("Sessions")]
    public partial class EfSession
    {
        public string Token { get; set; }

        public string Name { get; set; }

        public Guid? EmailUserId { get; set; }

        public DateTime ExpiresOn { get; set; }
    }
}