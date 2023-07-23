using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModels.Employee
{
    public class EmplyeeData
    {
        [Key]
        public int? EmployeeId { get; set; }  

        public string EmployeeName { get; set; } 

        public string EmployeeCity { get; set; }   

        public string EmpPosition { get; set; }

        public int ApplicationUserId { get; set; }
    }
}
