using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FacultyEval.Models
{
    public class StudentModel
    {
        [Required(ErrorMessage="Please enter your Student Number here.")]
        [Display(Name="Student Number")]
        public string studentID { get; set; }

        [Required(ErrorMessage = "Please enter your COR.")]
        [Display(Name = "COR Number")]
        public string studentCOR { get; set; }
    }
}