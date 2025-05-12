//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The ProvideService model represents the service provided for customers
//              with the properties and payment associated with the service.
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JasperGreen.Models
{
    public class ProvideService
    {
        [Key]
        public int ServiceID { get; set; }

        [Required(ErrorMessage = "Please include a crew")]
        public int CrewID { get; set; } //foreign key

        [ValidateNever]
        public Crew Crew { get; set; } = null!; //navigation property

        [Required(ErrorMessage = "Please include a customer")]
        public int CustomerID { get; set; } //foreign key

        [ValidateNever]
        public Customer Customer { get; set; } = null!; //navigation property

        [Required(ErrorMessage = "Please include a property")]
        public int PropertyID { get; set; } //foreign key

        [ValidateNever]
        public Property Property { get; set; } = null!; //navigation property

        public DateTime ServiceDate { get; set; }

        [Column(TypeName = "decimal(8,2)")]
        public decimal ServiceFee { get; set; }

        [ForeignKey("Payment")]
        public int? PaymentID { get; set; } //foreign key

        [ValidateNever]
        public Payment? Payment { get; set; } = null!; //navigation property
    }
}
