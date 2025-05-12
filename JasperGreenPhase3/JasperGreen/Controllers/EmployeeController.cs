//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The Employee Controller represents the bridge between the Employee model and all of the Views under Employee. 
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

using Microsoft.AspNetCore.Mvc;
using JasperGreen.Models;
using Microsoft.EntityFrameworkCore;

namespace JasperGreen.Controllers
{
    public class EmployeeController : Controller
    {
        private JasperGreenContext Context { get; set; }
        public EmployeeController(JasperGreenContext ctx) => Context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        [Route("[controller]s")]
        public IActionResult List()
        {
            var employees = Context.Employees.OrderBy(c => c.EmployeeLastName).ToList();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            var employee = new Employee();
            employee.HireDate = DateTime.Today;
            return View("AddEdit", employee);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var employee = Context.Employees.Find(id);
            return View("AddEdit", employee);
        }

        [HttpPost]
        public IActionResult Save(Employee employee)
        {
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                if (employee.EmployeeID == 0)
                {
                    Context.Employees.Add(employee);
                    message = employee.FullName + " was added.";
                }
                else
                {
                    Context.Employees.Update(employee);
                    message = employee.FullName + " was updated.";
                }
                Context.SaveChanges();
                TempData["message"] = message;
                return RedirectToAction("List");
            }
            else
            {
                if (employee.EmployeeID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }
                //ViewBag.Countries = context.Countries.OrderBy(c => c.Name).ToList();
                return View("AddEdit", employee);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employee = Context.Employees.Find(id);
            return View(employee);
        }

        [HttpPost]
        public IActionResult Delete(Employee employee)
        {
            var e = Context.Employees.Find(employee.EmployeeID);
            try
            {
                Context.Employees.Remove(e);
                Context.SaveChanges();
                TempData["message"] = employee.FullName + " was deleted.";
            }
            catch (DbUpdateException)
            {
                // Provide user-friendly feedback
                TempData["message"] = $"{e.FullName} cannot be deleted because there are related records in the system.";
            }
            return RedirectToAction("List");
        }
    }
}