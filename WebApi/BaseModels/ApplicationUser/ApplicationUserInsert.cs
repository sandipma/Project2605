using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModels.ApplicationUser
{
    public class ApplicationUserInsert
    {
        [Key]

        public int ApplicationUserId { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        [MinLength(10, ErrorMessage = "Can be at most 10 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "User Password is required")]
        [MinLength(10, ErrorMessage = "Must be 10-50 characters")]
        [MaxLength(50, ErrorMessage = "Must be 10-50 characters")]
        public string Userpassword { get; set; }
    }
}
