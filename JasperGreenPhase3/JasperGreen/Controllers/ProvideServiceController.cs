//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The Customer Controller represents the bridge between the Customer model and all of the Views under Customer. 
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

using Microsoft.AspNetCore.Mvc;
using JasperGreen.Models;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JasperGreen.Controllers
{
    public class ProvideServiceController : Controller
    {
        private const string CUSTKEY = "custID";
        private const string PROPKEY = "propID";
        private const string CREWKEY = "crewID";
        private JasperGreenContext Context { get; set; }
        public ProvideServiceController(JasperGreenContext ctx) => Context = ctx;

        public IActionResult Index() => RedirectToAction("List");

        [Route("[controller]s/{filter?}")]
        public IActionResult List(int CustID = -1, int PropID = -1, int CrewID = -1)
        {
            IQueryable<ProvideService> servicesQuery = Context.ProvideServices
                .Include(i => i.Crew)
                .Include(i => i.Customer)
                .Include(i => i.Property)
                .Include(i => i.Payment)
                .OrderBy(i => i.ServiceDate);
            string filter = "all";

            if (filter == "unassigned")
            {
                servicesQuery = servicesQuery.Where(i => i.CrewID == -1);
            }

            if (CustID != -1)
            {
                servicesQuery = servicesQuery.Where(i => i.CustomerID == CustID);
            }

            if (PropID != -1)
            {
                servicesQuery = servicesQuery.Where(i => i.PropertyID == PropID);
            }

            if (CrewID != -1)
            {
                servicesQuery = servicesQuery.Where(i => i.CrewID == CrewID);
            }

            var model = new ProvideServiceListViewModel
            {
                Filter = filter,
                ProvideServices = servicesQuery.ToList()
            };

            return View(model);
        }

        private ProvideServiceViewModel GetViewModel(string action = "")
        {
            ProvideServiceViewModel model = new ProvideServiceViewModel
            {
                Customers = Context.Customers
                    .OrderBy(c => c.CustomerFirstName)
                    .ToList(),
                Crews = Context.Crews
                    .OrderBy(c => c.CrewName)
                    .ToList(),
                Properties = Context.Properties
                    .OrderBy(p => p.PropertyAddress)
                    .ToList(),
                Payments = Context.Payments
                    .OrderBy(p => p.PaymentAmount)
                    .ToList(),
            };

            if (!String.IsNullOrEmpty(action))
                model.Action = action;

            return model;
        }

        [HttpGet]
        public IActionResult Add()
        {
            ProvideServiceViewModel model = GetViewModel("Add");
            model.Service = new ProvideService();
            model.Service.ServiceDate = DateTime.Today;

            return View("AddEdit", model);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ProvideServiceViewModel model = GetViewModel("Edit");
            model.Service = Context.ProvideServices.Find(id)!;

            return View("AddEdit", model);
        }
        [HttpGet]
        public IActionResult Pay(int id)
        {
            var customers = Context.Customers
                .OrderBy(c => c.CustomerFirstName)
                .ToList();

            var service = Context.ProvideServices.Find(id);

            PaymentViewModel model = new PaymentViewModel
            {
                Payment = new Payment
                {
                    PaymentDate = DateTime.Today,
                    CustomerID = service.CustomerID
                },
                Customers = customers,
                Service = service,
                Action = "Add"
            };

            return View("~/Views/Payment/AddEdit.cshtml", model);
        }

        [HttpPost]
        public IActionResult Save(ProvideServiceViewModel viewModel)
        {
            var property = Context.Properties.Find(viewModel.Service.PropertyID);
            if (property != null && viewModel.Service.ServiceFee < property.ServiceFee)
            {
                ModelState.AddModelError("Service.ServiceFee", "Service fee cannot be less than the property's base service fee.");
            }

            string message = string.Empty;
            if (ModelState.IsValid)
            {
                var service = viewModel.Service;
                var customer = Context.Customers.FirstOrDefault(c => c.CustomerID == service.CustomerID);

                if (Context.Crews.Any(c => c.CrewID == service.CrewID))
                {
                    if (service.ServiceID == 0)
                    {
                        Context.ProvideServices.Add(service);
                        message = "Service for " + customer.FullName + " was added.";
                    }
                    else
                    {
                        Context.ProvideServices.Update(service);
                        message = "Service for " + customer.FullName + " was updated.";
                    }
                    Context.SaveChanges();
                    TempData["message"] = message;
                    return RedirectToAction("List");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Crew Selection.");
                }

            }
            else
            {
                if (viewModel.Service.ServiceID == 0)
                {
                    ViewBag.Action = "Add";
                }
                else
                {
                    ViewBag.Action = "Edit";
                }
                viewModel.Crews = Context.Crews.OrderBy(c => c.CrewName).ToList();
                viewModel.Customers = Context.Customers.OrderBy(c => c.CustomerFirstName).ToList();
                viewModel.Properties = Context.Properties.OrderBy(p => p.PropertyAddress).ToList();
                viewModel.Payments = Context.Payments.OrderBy(p => p.PaymentAmount).ToList();
                return View("AddEdit", viewModel);
            }
            viewModel.Crews = Context.Crews.OrderBy(c => c.CrewName).ToList();
            viewModel.Customers = Context.Customers.OrderBy(c => c.CustomerFirstName).ToList();
            viewModel.Properties = Context.Properties.OrderBy(p => p.PropertyAddress).ToList();
            viewModel.Payments = Context.Payments.OrderBy(p => p.PaymentAmount).ToList();
            return View("AddEdit", viewModel);
        }
        [HttpPost]
        public IActionResult SaveWithoutProcessing(ProvideServiceViewModel viewModel)
        {
            TempData["message"] = "Payment successfully issued";
            // Redirect to the List action which renders the ProvideService list view
            return RedirectToAction("List");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var service = Context.ProvideServices
                        .Include(ps => ps.Customer) // Include the related customer
                        .FirstOrDefault(ps => ps.ServiceID == id);
            return View(service);
        }

        [HttpPost]
        public IActionResult Delete(ProvideService service)
        {
            var s = Context.ProvideServices.Include(ps => ps.Customer).FirstOrDefault(ps => ps.ServiceID == service.ServiceID);
            if (s != null && s.Customer != null)
            {
                try
                {
                    Context.ProvideServices.Remove(s);
                    Context.SaveChanges();
                    TempData["message"] = "Service for " + s.Customer.FullName + " was deleted.";
                }
                catch (DbUpdateException)
                {
                    // Provide user-friendly feedback
                    TempData["message"] = $"Service for {s.Customer.FullName} cannot be deleted because there are related records in the system.";
                }
            }
            else
            {
                TempData["message"] = "Service or related customer not found.";
            }

            return RedirectToAction("List");
        }

        //used to display customer dropdown. equivalent to index
        [HttpGet]
        public IActionResult GetCustomer()
        {
            ViewBag.Customers = Context.Customers
                .Where(c => c.CustomerID > -1)  // skip default unassigned value
                .OrderBy(c => c.CustomerFirstName)
                .ToList();

            var customer = new Models.Customer();

            int? custID = HttpContext.Session.GetInt32(CUSTKEY);
            if (custID.HasValue)
            {
                customer = Context.Customers.Find(custID);
            }

            return View(customer);
        }
        //equivalent to list
        [HttpPost]
        public IActionResult Customer(Customer customer)
        {
            if (customer.CustomerID == 0)
            {
                TempData["message"] = "You must select a customer.";
                return RedirectToAction("GetCustomer");
            }
            else
            {
                HttpContext.Session.SetInt32(CUSTKEY, customer.CustomerID);
                return RedirectToAction("List", new { CustID = customer.CustomerID, PropID = -1, CrewID = -1 });
            }
        }
        
        //used to display property dropdown. equivalent to index
        [HttpGet]
        public IActionResult GetProperty()
        {
            ViewBag.Properties = Context.Properties
                .Where(p => p.PropertyID > -1)  // skip default unassigned value
                .OrderBy(p => p.PropertyCity)
                .ToList();

            var property = new Property();

            int? propID = HttpContext.Session.GetInt32(PROPKEY);
            if (propID.HasValue)
            {
                property = Context.Properties.Find(propID);
            }

            return View(property);
        }
        //equivalent to list
        [HttpPost]
        public IActionResult Property(Property property)
        {
            if (property.PropertyID == 0)
            {
                TempData["message"] = "You must select a property.";
                return RedirectToAction("GetProperty");
            }
            else
            {
                HttpContext.Session.SetInt32(PROPKEY, property.PropertyID);
                return RedirectToAction("List", new { CustID = -1, PropID = property.PropertyID, CrewID = -1 });
            }
        }

        //used to display crew dropdown. equivalent to index
        [HttpGet]
        public IActionResult GetCrew()
        {
            ViewBag.Crews = Context.Crews
                .Where(c => c.CrewID > -1)  // skip default unassigned value
                .OrderBy(c => c.CrewName)
                .ToList();

            var crew = new Crew();

            int? crewID = HttpContext.Session.GetInt32(CREWKEY);
            if (crewID.HasValue)
            {
                crew = Context.Crews.Find(crewID);
            }

            return View(crewID);
        }
        //equivalent to list
        [HttpPost]
        public IActionResult Crew(Crew crew)
        {
            if (crew.CrewID == 0)
            {
                TempData["message"] = "You must select a crew.";
                return RedirectToAction("GetCrew");
            }
            else
            {
                HttpContext.Session.SetInt32(CREWKEY, crew.CrewID);
                return RedirectToAction("List", new { CustID = -1, PropID = -1, CrewID = crew.CrewID });
            }
        }
    }
}