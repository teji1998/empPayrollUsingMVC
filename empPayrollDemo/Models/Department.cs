﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace empPayrollDemo.Models
{
    public class Department
    {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

       public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

    }
}