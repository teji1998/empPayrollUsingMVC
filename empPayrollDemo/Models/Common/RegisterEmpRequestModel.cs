using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace empPayrollDemo.Models.Common
{
    public class RegisterEmpRequestModel
    {
        public int EmpId { get; set; }
        [Required(ErrorMessage = "required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "required")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "required")]
        public string Department { get; set; }
        [Required(ErrorMessage = "required")]
        public string SalaryId { get; set; }
        [Required(ErrorMessage = "required")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime StartDate { get; set; }
        public string Description { get; set; }
    }
}