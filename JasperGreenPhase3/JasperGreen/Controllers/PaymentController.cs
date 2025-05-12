//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The PAyment Controller represents the bridge between the Payment model and all of the Views under Payment. 
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JasperGreen.Models;

namespace JasperGreen.Controllers
{
    public class PaymentController : Controller
    {
        private JasperGreenContext Context { get; set; }
        public PaymentController(JasperGreenContext ctx) => Context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        [Route("[controller]s")]
        public IActionResult List() //populates the list view of the Property List View using the PropertyListViewModel
        {
            var model = new PaymentListViewModel
            {
                Payments = Context.Payments.Include(p => p.Customer).ToList()
            };

            return View(model);
        }

        private PaymentViewModel GetViewModel(string action = "")
        {
            PaymentViewModel model = new PaymentViewModel
            {
                Customers = Context.Customers
                    .OrderBy(c => c.CustomerFirstName)
                    .ToList()
            };
            if (!String.IsNullOrEmpty(action))
                model.Action = action;

            return model;
        }

        [HttpGet]
        public IActionResult Add()
        {
            PaymentViewModel model = GetViewModel("Add");
            model.Payment = new Payment
            {
                PaymentDate = DateTime.Today // Set PaymentDate to the current date
            };

            return View("AddEdit", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            PaymentViewModel model = GetViewModel("Edit");
            model.Payment = Context.Payments.Find(id)!;

            return View("AddEdit", model);
        }

        [HttpPost]
        public IActionResult Save(Payment payment)
        {
            string message = string.Empty;
            if (ModelState.IsValid)
            {
                var customer = Context.Customers.FirstOrDefault(c => c.CustomerID == payment.CustomerID);

                if (payment.PaymentID == 0)
                {
                    Context.Payments.Add(payment);
                    message = customer?.FullName + "'s payment was added.";
                }
                else
                {
                    Context.Payments.Update(payment);
                    message = customer?.FullName + "'s payment was updated.";
                }
                Context.SaveChanges();
                TempData["message"] = message;
                return RedirectToAction("List");
            }
            else
            {
                if (payment.PaymentID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }

                return View(payment);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var payment = Context.Payments
                                .Include(p => p.Customer) // Include the related customer
                                .FirstOrDefault(p => p.PaymentID == id);
            return View(payment);
        }


        [HttpPost]
        public IActionResult Delete(Payment payment)
        {
            var payment1 = Context.Payments.Include(p => p.Customer).FirstOrDefault(p => p.PaymentID == payment.PaymentID);

            if (payment1 != null && payment1.Customer != null) // Check if payment1 and its Customer are not null
            {
                try
                {
                    Context.Payments.Remove(payment1);
                    Context.SaveChanges();
                    TempData["message"] = payment1.Customer.FullName + "'s payment was deleted.";
                }
                catch (DbUpdateException)
                {
                    TempData["message"] = payment1.Customer.FullName + "'s payment cannot be deleted because there are related records in the system.";
                }
            }
            else
            {
                TempData["message"] = "Payment or related customer not found.";
            }

            return RedirectToAction("List");
        }

    }
}