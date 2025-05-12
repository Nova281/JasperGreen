//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The Employee model represents Jasper Green employees information
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace JasperGreen.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "First name required")]
        public string EmployeeFirstName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Last name required")]
        public string EmployeeLastName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "SSN required")]
        public string SSN { get; set; } = string.Empty;

        [Required(ErrorMessage = "JobTitle required")]
        public string JobTitle { get; set; } = string.Empty;

        [Required(ErrorMessage = "HireDate required")]
        public DateTime HireDate { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        [Range(1, 1000000, ErrorMessage = "Hourly rate must be greater than zero.")]
        [Required(ErrorMessage = "Hourly Rate required")]
        public decimal HourlyRate { get; set; }

        public string FullName => EmployeeFirstName + " " + EmployeeLastName;   // read-only property

        // Foreman role
        public ICollection<Crew> Crews { get; set; } = null!;
        // Crew Member 1 role
        public ICollection<Crew> Member1 { get; set; } = null!;
        // Crew Member 2 role
        public ICollection<Crew> Member2 { get; set; } = null!;
    }
}