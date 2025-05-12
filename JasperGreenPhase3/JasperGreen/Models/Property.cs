//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The Property model represents customers and their properties.
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JasperGreen.Models
{
    public class Property
    {
        public int PropertyID { get; set; }

        // Foreign key and navigation property to Customer
        [Required(ErrorMessage = "Please include a customer")]
        public int CustomerID { get; set; } //foreign key

        [ValidateNever]
        public Customer Customer { get; set; } = null!;//navigation property

        [Required(ErrorMessage = "Property address required")]
        public string PropertyAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "City required")]
        public string PropertyCity { get; set; } = string.Empty;

        [Required(ErrorMessage = "State required")]
        public string PropertyState { get; set; } = string.Empty;

        [Required(ErrorMessage = "Zip required")]
        public string PropertyZip { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a Service Fee.")]
        [Range(1, 1000000, ErrorMessage = "Service fee must be greater than zero.")]
        [Column(TypeName = "decimal(8,2)")]
        [Display(Name = "Service Fee")]
        public decimal ServiceFee { get; set; }

        [ValidateNever]
        // Navigation payment for related payments
        public ICollection<ProvideService> ProvideServices { get; set; }
    }
}
