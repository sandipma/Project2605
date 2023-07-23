using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModels.Employee
{
    public class EmployeeInsert
    {
        [Key]
        public int ? EmployeeId { get; set; }


        [Required(ErrorMessage = "EmployeeName is required")]
        [MinLength(10, ErrorMessage = "Must be 10-20 characters")]
        [MaxLength(20, ErrorMessage = "Must be 10-20 characters")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "EmployeeCity is required")]
        [MinLength(10, ErrorMessage = "Must be 10-20 characters")]
        [MaxLength(20, ErrorMessage = "Must be 10-20 characters")]
        public string EmployeeCity { get; set; }

        [Required(ErrorMessage = "EmployeePositon is required")]
        [MinLength(10, ErrorMessage = "Must be 10-20 characters")]
        [MaxLength(20, ErrorMessage = "Must be 10-20 characters")]
        public string EmpPosition { get; set; }

        [Required(ErrorMessage = "Image is required")]
        [NotMapped]
        public IFormFile ProfileImage { get; set; }
        public int ApplicationUserId { get; set; }

    }
}
