//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The CrewListViewModel to pass the database crew list from the controller to the view.
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

namespace JasperGreen.Models
{
    public class CrewListViewModel
    {
        public IEnumerable<Crew> Crews { get; set; } = null!;

    }
}
