//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The Crew model represents Jasper Green crew with a foreman and two crew members. 
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JasperGreen.Models
{
    public class Crew
    {
        public int CrewID { get; set; }
        
        [Required(ErrorMessage = "Crew name required")]
        public string CrewName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Crew foreman required")]
        public int CrewForemanID { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(CrewForemanID))]
        public Employee CrewForeman { get; set; } = null!;
        
        [Required(ErrorMessage = "Crew member 1 required")]
        public int CrewMember1ID { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(CrewMember1ID))]

        public Employee CrewMember1 { get; set; } = null!;
        
        [Required(ErrorMessage = "Crew member 2 required")]
        public int CrewMember2ID { get; set; }

        [ValidateNever]
        [ForeignKey(nameof(CrewMember2ID))]
        public Employee CrewMember2 { get; set; } = null!;
        
        [ValidateNever]
        // Navigation provided services for related services
        public ICollection<ProvideService> ProvideServices { get; set; }
    }
}
