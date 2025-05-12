//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The PaymentViewModel to pass the customer list from the controller to the view.
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.
 
namespace JasperGreen.Models
{
    public class PaymentViewModel
    {
        public Payment Payment { get; set; } = null!;

        public ProvideService Service { get; set; } = null!;

        public string Action { get; set; } = string.Empty;

        public IEnumerable<Customer> Customers { get; set; } = null!;

    }
}
