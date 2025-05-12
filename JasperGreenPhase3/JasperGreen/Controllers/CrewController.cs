//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The Crew Controller represents the bridge between the Crew model and all of the Views under Crew. 
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JasperGreen.Models;

namespace JasperGreen.Controllers
{
    public class CrewController : Controller
    {
        private JasperGreenContext Context { get; set; }
        public CrewController(JasperGreenContext ctx) => Context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        [Route("[controller]s")]
        public IActionResult List()
        {
            var model = new CrewListViewModel
            {
                Crews = Context.Crews
                    .Include(c => c.CrewForeman)
                    .Include(c => c.CrewMember1)
                    .Include(c => c.CrewMember2)
                    .OrderBy(c => c.CrewName)
            };
            
            return View(model);
        }

        private CrewViewModel GetViewModel(string action = "")
        {
            CrewViewModel model = new CrewViewModel
            {
                CrewForemans = Context.Employees
                    .OrderBy(e => e.EmployeeLastName),
                CrewMember1s = Context.Employees
                    .OrderBy(e => e.EmployeeLastName),
                CrewMember2s = Context.Employees
                    .OrderBy(e => e.EmployeeLastName)
            };
            
            if (!String.IsNullOrEmpty(action))
                model.Action = action;

            return model;
        }

        [HttpGet]
        public IActionResult Add()
        {
            CrewViewModel model = GetViewModel("Add");
            model.Crew = new Crew();

            return View("AddEdit", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            CrewViewModel model = GetViewModel("Edit");
            model.Crew = Context.Crews.Find(id)!;

            return View("AddEdit", model);
        }

        [HttpPost]
        public IActionResult Save(Crew crew)
        {

            if (crew.CrewForemanID == crew.CrewMember1ID ||
                crew.CrewForemanID == crew.CrewMember2ID ||
                crew.CrewMember1ID == crew.CrewMember2ID)
            {
                ModelState.AddModelError("crew.CrewForemanID", "Each role must be assigned a different employee.");
                ModelState.AddModelError("crew.CrewMember1ID", "Each role must be assigned a different employee.");
                ModelState.AddModelError("crew.CrewMember2ID", "Each role must be assigned a different employee.");
            }

            string message = string.Empty;

            if (ModelState.IsValid)
            {
                if (crew.CrewID == 0)
                {
                    Context.Crews.Add(crew);
                    message = crew.CrewName + " was added.";

                }
                else
                {
                    Context.Crews.Update(crew);
                    message = crew.CrewName + " was updated.";

                }
                Context.SaveChanges();
                TempData["message"] = message;
                return RedirectToAction("List");
            }
            else
            {
                var model = GetViewModel();
                model.Crew = crew;

                if (crew.CrewID == 0)
                {
                    model.Action = "Add";
                }
                else
                {
                    model.Action = "Edit";
                }
                return View("AddEdit", model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var crew = Context.Crews.Find(id);
            return View(crew);
        }

        [HttpPost]
        public IActionResult Delete(Crew crew)
        {
            var c = Context.Crews.Find(crew.CrewID);
            try
            {
                Context.Crews.Remove(c);
                Context.SaveChanges();
                TempData["message"] = crew.CrewName + " was deleted.";
            }
            catch (DbUpdateException)
            {
                // Provide user-friendly feedback
                TempData["message"] = $"{c.CrewName} cannot be deleted because there are related records in the system.";
            }
            return RedirectToAction("List");
        }
    }
}