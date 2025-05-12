//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The Property Controller represents the bridge between the Property model and all of the Views under Property. 
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JasperGreen.Models;

namespace JasperGreen.Controllers
{
    public class PropertyController : Controller
    {
        private JasperGreenContext Context { get; set; }
        public PropertyController(JasperGreenContext ctx) => Context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        [Route("properties")]
        public IActionResult List()
        {
            var model = new PropertyListViewModel
            {
                Properties = Context.Properties
                    .Include(p => p.Customer)
                    .OrderBy(p => p.Customer.CustomerLastName)
            };
            
            return View(model);
        }

        private PropertyViewModel GetViewModel(string action = "")
        {
            PropertyViewModel model = new PropertyViewModel
            {
                Customers = Context.Customers
                    .OrderBy(c => c.CustomerLastName)
                    .ToList()
            };
            
            if (!String.IsNullOrEmpty(action))
                model.Action = action;

            return model;
        }

        [HttpGet]
        public IActionResult Add()
        {
            PropertyViewModel model = GetViewModel("Add");
            model.Property = new Property();

            return View("AddEdit", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            PropertyViewModel model = GetViewModel("Edit");
            model.Property = Context.Properties.Find(id)!;

            return View("AddEdit", model);
        }

        [HttpPost]
        public IActionResult Save(Property property)
        {
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                if (property.PropertyID == 0)
                {
                    Context.Properties.Add(property);
                    message = property.PropertyAddress + " was added.";
                }
                else
                {
                    Context.Properties.Update(property);
                    message = property.PropertyAddress + " was updated.";

                }
                Context.SaveChanges();
                TempData["message"] = message;
                return RedirectToAction("List");
            }
            else
            {
                var model = GetViewModel();
                model.Property = property;

                if (property.PropertyID == 0)
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
            var product = Context.Properties.Find(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(Property property)
        {
            var p = Context.Properties.Find(property.PropertyID);
            try
            {
                Context.Properties.Remove(p);
                Context.SaveChanges();
                TempData["message"] = property.PropertyAddress + " was deleted.";
            }
            catch (DbUpdateException)
            {
                // Provide user-friendly feedback
                TempData["message"] = $"{p.PropertyAddress} cannot be deleted because there are related records in the system.";
            }
            return RedirectToAction("List");
        }
    }
}