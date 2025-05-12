//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The ProvideServiceListViewModel to pass the database service list from the controller to the view.
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

namespace JasperGreen.Models
{
    public class ProvideServiceListViewModel
    {
        public string Filter { get; set; } = string.Empty;
        public IEnumerable<ProvideService> ProvideServices { get; set; } = null!;
    }
}
