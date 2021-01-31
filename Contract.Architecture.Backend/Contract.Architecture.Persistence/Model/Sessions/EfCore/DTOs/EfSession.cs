﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contract.Architecture.Persistence.Model.Sessions.EfCore
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