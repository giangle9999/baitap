using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BT2.Models
{
    public class AccountModel
    {
        [Required(ErrorMessage = "Username is required.")]
        [Key]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [StringLength(10)]
        public string RoleID { get; set; }
    }
}