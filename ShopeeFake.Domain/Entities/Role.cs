﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ShopeeFake.Domain.Entities
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}
