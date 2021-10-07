using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BT2.Models
{
        public class Role
        {
            [Key]
            [StringLength(10)]
            public string RoleID { get; set; }
            [StringLength(50)]
            public string RoleName { get; set; }
        }
    }
}
}