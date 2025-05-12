//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The Customer Controller represents the bridge between the Customer model and all of the Views under Customer. 
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

using Microsoft.AspNetCore.Mvc;
using JasperGreen.Models;
using Microsoft.EntityFrameworkCore;

namespace JasperGreen.Controllers
{
    public class CustomerController : Controller
    {
        private JasperGreenContext Context { get; set; }
        public CustomerController(JasperGreenContext ctx) => Context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        [Route("[controller]s")]
        public IActionResult List()
        {
            var customers = Context.Customers.OrderBy(c => c.CustomerLastName).ToList();
            return View(customers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("AddEdit", new Customer());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var customer = Context.Customers.Find(id);
            return View("AddEdit", customer);
        }

        [HttpPost]
        public IActionResult Save(Customer customer)
        {
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                if (customer.CustomerID == 0)
                {
                    Context.Customers.Add(customer);
                    message = customer.FullName + " was added.";
                }
                else
                {
                    Context.Customers.Update(customer);
                    message = customer.FullName + " was updated.";

                }
                Context.SaveChanges();
                TempData["message"] = message;
                return RedirectToAction("List");
            }
            else
            {
                if (customer.CustomerID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }
                return View("AddEdit", customer);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var customer = Context.Customers.Find(id);
            return View(customer);
        }

        [HttpPost]
        public IActionResult Delete(Customer customer)
        {
            var c = Context.Customers.Find(customer.CustomerID);
            try
            {
                Context.Customers.Remove(c);
                Context.SaveChanges();
                TempData["message"] = customer.FullName + " was deleted.";
            }
            catch (DbUpdateException)
            {
                // Provide user-friendly feedback
                TempData["message"] = $"{c.FullName} cannot be deleted because there are related records in the system.";
            }
            return RedirectToAction("List");
        }
    }
}