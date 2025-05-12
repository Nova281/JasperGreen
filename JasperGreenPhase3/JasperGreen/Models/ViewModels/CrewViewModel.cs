//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The CrewViewModel to pass the employee list from the controller to the view.
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

namespace JasperGreen.Models
{
    public class CrewViewModel
    {
        public Crew Crew { get; set; } = null!;
        public string Action { get; set; } = string.Empty;

        public IEnumerable<Employee> CrewForemans { get; set; } = null!;
        public IEnumerable<Employee> CrewMember1s { get; set; } = null!;
        public IEnumerable<Employee> CrewMember2s { get; set; } = null!;
    }
}
