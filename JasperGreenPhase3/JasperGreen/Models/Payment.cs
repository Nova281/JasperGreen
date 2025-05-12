//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The Payment model represents Jasper Green payments made by customers.
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JasperGreen.Models
{
    public class Payment
    {
        public int PaymentID { get; set; }

        [Required(ErrorMessage = "Please include a customer")]
        public int CustomerID { get; set; } //foreign key

        [ValidateNever]
        public Customer Customer { get; set; } = null!; //navigation property

        [Required(ErrorMessage = "Payment required")]
        public DateTime PaymentDate { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        [Required(ErrorMessage = "Payment Amount required")]
        public decimal PaymentAmount { get; set; }
        
        [ValidateNever]
        // Navigation provided services for related services
        public ICollection<ProvideService> ProvideServices { get; set; }
    }
}