using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BaseModels.ApplicationUser
{
    public class ApplicationUserDt
    {
        [Key]
       
        public int ApplicationUserId { get; set; }

        public string Username { get; set; }

        public string UserPassword { get; set; }

        [NotMapped]
        public string? Token { get; set; }
    }
}
