//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The PropertyViewModel to pass the customer list from the controller to the view.
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace JasperGreen.Models
{
    public class PropertyViewModel
    {
        public Property Property { get; set; } = null!;
        public string Action { get; set; } = string.Empty;
        [ValidateNever]
        public IEnumerable<Customer> Customers { get; set; } = null!;
    }
}
