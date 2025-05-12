//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The Customer model represents Jasper Green customers with their information.
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace JasperGreen.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }

        [Required(ErrorMessage = "First name required")]
        public string CustomerFirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name required")]
        public string CustomerLastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Billing address required")]
        public string BillingAddress { get; set; } = string.Empty;

        [Required(ErrorMessage = "City required")]
        public string BillingCity { get; set; } = string.Empty;

        [Required(ErrorMessage = "State required")]
        [RegularExpression(@"^(A[LKZR]|C[AOT]|DE|FL|GA|HI|I[ADLN]|K[SY]|LA|M[ADEINOST]|N[CDEHJMVY]|O[HKR]|PA|RI|S[CD]|T[NX]|UT|V[AIT]|W[AIVY])$", ErrorMessage = "Enter valid 2-letter U.S. state abbreviation")]
        public string BillingState { get; set; } = string.Empty;

        [Required(ErrorMessage = "Zip code required")]
        [RegularExpression(@"^\d{5}(-\d{4})?$", ErrorMessage = "Enter a valid 5 or 9 digit ZIP code")]
        public string BillingZip { get; set; } = string.Empty;

        [Required(ErrorMessage = "Phone number required")]
        [RegularExpression(@"\(\d{3}\) \d{3}-\d{4}", ErrorMessage = "Enter phone number in format (123) 456-7890")]
        public string CustomerPhone { get; set; } = string.Empty;

        [ValidateNever]
        public string FullName => CustomerFirstName + " " + CustomerLastName;   // read-only property

        [ValidateNever]
        // Navigation property for related properties
        public ICollection<Property> Properties { get; set; }
        [ValidateNever]
        // Navigation payment for related payments
        public ICollection<Payment> Payments { get; set; }
        [ValidateNever]
        // Navigation provide services for related services
        public ICollection<ProvideService> ProvideServices { get; set; }

    }
}
