//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The PaymentListViewModel to pass the database payments list from the controller to the view.
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

namespace JasperGreen.Models
{
    public class PaymentListViewModel
    {
        public IEnumerable<Payment> Payments { get; set; } = null!;
    }
}
