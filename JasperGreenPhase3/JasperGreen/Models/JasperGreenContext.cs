//  AUTHOR:     Benz Le and Harrison Lee
//  COURSE:     ISTM 415
//  PROGRAM:    Jasper Green Web App
//  PURPOSE:    The JasperGreenContext model represents database seeded data with fluid API
//              for establishing database relationships.
//  HONOR CODE: On my honor, as an Aggie, I have neither given 
//              nor received unauthorized aid on this academic work.

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection.Emit;

namespace JasperGreen.Models
{
    public class JasperGreenContext : DbContext
    {
        public JasperGreenContext(DbContextOptions<JasperGreenContext> options) : base(options) { }

        public DbSet<Crew> Crews { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;
        public DbSet<Customer> Customers { get; set; } = null!;
        public DbSet<Property> Properties { get; set; } = null!;
        public DbSet<ProvideService> ProvideServices { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Prevent cascading deletes between Customer and Property
            modelBuilder.Entity<Crew>()
                .HasOne(c => c.CrewForeman)
                .WithMany(e => e.Crews)
                .HasForeignKey(cf => cf.CrewForemanID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Crew>()
                 .HasOne(c => c.CrewMember1)
                 .WithMany(e => e.Member1)
                 .HasForeignKey(cf => cf.CrewMember1ID)
                 .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Crew>()
                 .HasOne(c => c.CrewMember2)
                 .WithMany(e => e.Member2)
                 .HasForeignKey(cf => cf.CrewMember2ID)
                 .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Property>()
                 .HasOne(p => p.Customer)
                 .WithMany(c => c.Properties)
                 .HasForeignKey(p => p.CustomerID)
                 .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Payment>()
                 .HasOne(p => p.Customer)
                 .WithMany(c => c.Payments)
                 .HasForeignKey(p => p.CustomerID)
                 .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<ProvideService>()
                .HasOne(pr => pr.Payment)
                .WithMany(p => p.ProvideServices)
                .HasForeignKey(pr => pr.PaymentID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProvideService>()
                .HasOne(pr => pr.Property)
                .WithMany(p => p.ProvideServices)
                .HasForeignKey(pr => pr.PropertyID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProvideService>()
                .HasOne(pr => pr.Customer)
                .WithMany(c => c.ProvideServices)
                .HasForeignKey(c => c.CustomerID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ProvideService>()
                .HasOne(pr => pr.Crew)
                .WithMany(cr => cr.ProvideServices)
                .HasForeignKey(pr => pr.CrewID)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Customer>().HasData(
               new Customer
               {
                   CustomerID = 1,
                   CustomerFirstName = "Aarya",
                   CustomerLastName = "Mummadavarapu",
                   BillingAddress = "2200 Cottage Ln",
                   BillingCity = "College Station",
                   BillingState = "TX",
                   BillingZip = "77845",
                   CustomerPhone = "(512) 588-9150"

               },
               new Customer
               {
                   CustomerID = 2,
                   CustomerFirstName = "Adeline ",
                   CustomerLastName = "Wardell",
                   BillingAddress = "85 North Campfire Street",
                   BillingCity = "College Station",
                   BillingState = "TX",
                   BillingZip = "77843",
                   CustomerPhone = "(732) 308-7385"
               },
               new Customer
               {
                   CustomerID = 3,
                   CustomerFirstName = "Dexter",
                   CustomerLastName = "Jasmine",
                   BillingAddress = "22 Glen Eagles Avenue",
                   BillingCity = "College Station",
                   BillingState = "TX",
                   BillingZip = "77845",
                   CustomerPhone = "(914) 258-9597"
               },
               new Customer
               {
                   CustomerID = 4,
                   CustomerFirstName = "Gorden",
                   CustomerLastName = "Roderick",
                   BillingAddress = "7433 Jockey Hollow Drive",
                   BillingCity = "College Station",
                   BillingState = "TX",
                   BillingZip = "77842",
                   CustomerPhone = "(210) 485-9946"
               },
               new Customer
               {
                   CustomerID = 5,
                   CustomerFirstName = "Kevin",
                   CustomerLastName = "Nguyen",
                   BillingAddress = "141 Silver Spear Lane",
                   BillingCity = "College Station",
                   BillingState = "TX",
                   BillingZip = "77845",
                   CustomerPhone = "(713) 555-4321"
               }
            );

            modelBuilder.Entity<Property>().HasData(
               new Property
               {
                   PropertyID = 1,
                   CustomerID = 1,
                   PropertyAddress = "2200 Cottage Lane",
                   PropertyCity = "College Station",
                   PropertyState = "Texas",
                   PropertyZip = "77840",
                   ServiceFee = 100.00m
               },
               new Property
               {
                   PropertyID = 2,
                   CustomerID = 1,
                   PropertyAddress = "101 University Drive",
                   PropertyCity = "College Station",
                   PropertyState = "Texas",
                   PropertyZip = "77840",
                   ServiceFee = 100.00m
               },
               new Property
               {
                   PropertyID = 3,
                   CustomerID = 2,
                   PropertyAddress = "500 George Bush Drive",
                   PropertyCity = "College Station",
                   PropertyState = "Texas",
                   PropertyZip = "77840",
                   ServiceFee = 300.00m
               },
               new Property
               {
                   PropertyID = 4,
                   CustomerID = 2,
                   PropertyAddress = "202 Southwest Parkway",
                   PropertyCity = "College Station",
                   PropertyState = "Texas",
                   PropertyZip = "77840",
                   ServiceFee = 100.00m
               },
               new Property
               {
                   PropertyID = 5,
                   CustomerID = 3,
                   PropertyAddress = "1501 Harvey Road",
                   PropertyCity = "College Station",
                   PropertyState = "Texas",
                   PropertyZip = "77840",
                   ServiceFee = 100.00m
               },
               new Property
               {
                   PropertyID = 6,
                   CustomerID = 3,
                   PropertyAddress = "909 Texas Avenue South",
                   PropertyCity = "College Station",
                   PropertyState = "Texas",
                   PropertyZip = "77840",
                   ServiceFee = 100.00m
               },
               new Property
               {
                   PropertyID = 7,
                   CustomerID = 4,
                   PropertyAddress = "3200 Longmire Drive",
                   PropertyCity = "College Station",
                   PropertyState = "Texas",
                   PropertyZip = "77845",
                   ServiceFee = 110.00m
               },
               new Property
               {
                   PropertyID = 8,
                   CustomerID = 4,
                   PropertyAddress = "1801 Holleman Dr",
                   PropertyCity = "College Station",
                   PropertyState = "Texas",
                   PropertyZip = "77840",
                   ServiceFee = 100.00m
               },
               new Property
               {
                   PropertyID = 9,
                   CustomerID = 5,
                   PropertyAddress = "2301 Earl Rudder Fwy South",
                   PropertyCity = "College Station",
                   PropertyState = "Texas",
                   PropertyZip = "77845",
                   ServiceFee = 90.00m
               },
               new Property
               {
                   PropertyID = 10,
                   CustomerID = 5,
                   PropertyAddress = "4101 Texas Ave",
                   PropertyCity = "College Station",
                   PropertyState = "Texas",
                   PropertyZip = "77840",
                   ServiceFee = 90.00m
               }
            );

            modelBuilder.Entity<Employee>().HasData(
               new Employee
               {
                   EmployeeID = 1,
                   EmployeeFirstName = "Bjork",
                   EmployeeLastName = "Sonja",
                   SSN = "123456789",
                   JobTitle = "Crewman Associate 1",
                   HireDate = DateTime.Parse("04/03/2025"),
                   HourlyRate = 30.30m
               },
               new Employee
               {
                   EmployeeID = 2,
                   EmployeeFirstName = "Amina",
                   EmployeeLastName = "Sara",
                   SSN = "223456789",
                   JobTitle = "Crewman Intern",
                   HireDate = DateTime.Parse("04/03/2025"),
                   HourlyRate = 30.30m
               },
               new Employee
               {
                   EmployeeID = 3,
                   EmployeeFirstName = "Pakpao",
                   EmployeeLastName = "Amporn",
                   SSN = "323456789",
                   JobTitle = "Junior Janatorial Consultant 1",
                    HireDate = DateTime.Parse("04/03/2025"),
                   HourlyRate = 30.30m
               },
               new Employee
               {
                   EmployeeID = 4,
                   EmployeeFirstName = "Mihangel",
                   EmployeeLastName = "Dai",
                   SSN = "423456789",
                   JobTitle = "Senior Leaf Blowing Partner",
                   HireDate = DateTime.Parse("12/03/2000"),
                   HourlyRate = 30.30m
               },
               new Employee
               {
                   EmployeeID = 5,
                   EmployeeFirstName = "Willihard",
                   EmployeeLastName = "Epiphanes",
                   SSN = "523456789",
                   JobTitle = "Crewman Intern",
                   HireDate = DateTime.Parse("04/13/2020"),
                   HourlyRate = 13.35m
               },
               new Employee
               {
                   EmployeeID = 6,
                   EmployeeFirstName = "Nikandros",
                   EmployeeLastName = "Leutgar",
                   SSN = "523456789",
                   JobTitle = "Leaf Blowing Analyst 2",
                   HireDate = DateTime.Parse("11/01/2005"),
                   HourlyRate = 34.34m
               },
               new Employee
               {
                   EmployeeID = 7,
                   EmployeeFirstName = "Gervasius",
                   EmployeeLastName = "Lydos",
                   SSN = "523456789",
                   JobTitle = "Crewman Associate 1",
                   HireDate = DateTime.Parse("06/23/2004"),
                   HourlyRate = 30.30m
               },
               new Employee
               {
                   EmployeeID = 8,
                   EmployeeFirstName = "Attikos",
                   EmployeeLastName = "Manuel",
                   SSN = "523456789",
                   JobTitle = "Senior Janitorial Consultant",
                   HireDate = DateTime.Parse("04/09/2004"),
                   HourlyRate = 50.30m
               },
               new Employee
               {
                   EmployeeID = 9,
                   EmployeeFirstName = "Acacius",
                   EmployeeLastName = "Ardalion",
                   SSN = "523456789",
                   JobTitle = "Crewman Associate 1",
                   HireDate = DateTime.Parse("07/11/2005"),
                   HourlyRate = 30.30m
               },
               new Employee
               {
                   EmployeeID = 10,
                   EmployeeFirstName = "Facundus",
                   EmployeeLastName = "Nazarius",
                   SSN = "523456789",
                   JobTitle = "Crewman Associate 1",
                   HireDate = DateTime.Parse("05/23/2009"),
                   HourlyRate = 30.30m
               },
               new Employee
               {
                   EmployeeID = 11,
                   EmployeeFirstName = "Rocco",
                   EmployeeLastName = "Sophus",
                   SSN = "523456789",
                   JobTitle = "Crewman Associate 1",
                   HireDate = DateTime.Parse("10/23/1997"),
                   HourlyRate = 30.30m
               },
               new Employee
               {
                   EmployeeID = 12,
                   EmployeeFirstName = "Balderich",
                   EmployeeLastName = "Raginhard",
                   SSN = "523456789",
                   JobTitle = "Crewman Associate 1",
                   HireDate = DateTime.Parse("10/08/2016"),
                   HourlyRate = 30.30m
               },
               new Employee
               {
                   EmployeeID = 13,
                   EmployeeFirstName = "Artabazos",
                   EmployeeLastName = "Rufinus",
                   SSN = "523456789",
                   JobTitle = "Crewman Associate 1",
                   HireDate = DateTime.Parse("02/02/2019"),
                   HourlyRate = 30.30m
               },
               new Employee
               {
                   EmployeeID = 14,
                   EmployeeFirstName = "Vibius",
                   EmployeeLastName = "Alfarr",
                   SSN = "523456789",
                   JobTitle = "Crewman Associate 1",
                   HireDate = DateTime.Parse("04/26/2007"),
                   HourlyRate = 30.30m
               },
               new Employee
               {
                   EmployeeID = 15,
                   EmployeeFirstName = "Deusdedit",
                   EmployeeLastName = "Pygmalion",
                   SSN = "523456789",
                   JobTitle = "Crewman Associate 1",
                   HireDate = DateTime.Parse("03/15/2017"),
                   HourlyRate = 30.30m
               }
            );
            modelBuilder.Entity<Crew>().HasData(
                new Crew
                {
                    CrewID = 1,
                    CrewName = "Mowing Crew 1",
                    CrewForemanID = 1,
                    CrewMember1ID = 5,
                    CrewMember2ID = 7,
                },
                new Crew
                {
                    CrewID = 2,
                    CrewName = "Mowing Crew 2",
                    CrewForemanID = 2,
                    CrewMember1ID = 10,
                    CrewMember2ID = 9,
                },
                new Crew
                {
                    CrewID = 3,
                    CrewName = "Mowing Crew 3",
                    CrewForemanID = 11,
                    CrewMember1ID = 12,
                    CrewMember2ID = 8,
                },
                new Crew
                {
                    CrewID = 4,
                    CrewName = "Mowing Crew 4",
                    CrewForemanID = 6,
                    CrewMember1ID = 4,
                    CrewMember2ID = 3,
                },
                new Crew
                {
                    CrewID = 5,
                    CrewName = "Mowing Crew 5",
                    CrewForemanID = 13,
                    CrewMember1ID = 14,
                    CrewMember2ID = 15,
                }
                );
            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    PaymentID = 1,
                    CustomerID = 1,
                    PaymentDate = DateTime.Parse("2025-04-20"),
                    PaymentAmount = 200.00M,
                },
                new Payment
                {
                    PaymentID = 2,
                    CustomerID = 2,
                    PaymentDate = DateTime.Parse("2024-07-01"),
                    PaymentAmount = 400.00M,

                },
                new Payment
                {
                    PaymentID = 3,
                    CustomerID = 3,
                    PaymentDate = DateTime.Parse("2023-01-10"),
                    PaymentAmount = 215.00M,

                },
                new Payment
                {
                    PaymentID = 4,
                    CustomerID = 4,
                    PaymentDate = DateTime.Parse("2025-02-09"),
                    PaymentAmount = 210.00M,

                },
                new Payment
                {
                    PaymentID = 5,
                    CustomerID = 5,
                    PaymentDate = DateTime.Parse("2025-01-19"),
                    PaymentAmount = 180.00M,

                }
            );
            modelBuilder.Entity<ProvideService>().HasData(
                new ProvideService
                {
                    ServiceID = 1,
                    CrewID = 1,
                    CustomerID = 1,
                    PropertyID = 1,
                    ServiceDate = DateTime.Parse("2025-04-13"),
                    ServiceFee = 100M,
                    PaymentID = 1,
                },
                new ProvideService
                {
                    ServiceID = 2,
                    CrewID = 1,
                    CustomerID = 1,
                    PropertyID = 2,
                    ServiceDate = DateTime.Parse("2025-04-12"),
                    ServiceFee = 100M,
                    PaymentID = 1,
                },
                new ProvideService
                {
                    ServiceID = 3,
                    CrewID = 2,
                    CustomerID = 2,
                    PropertyID = 3,
                    ServiceDate = DateTime.Parse("2024-06-21"),
                    ServiceFee = 300M,
                    PaymentID = 2,
                },
                new ProvideService
                {
                    ServiceID = 4,
                    CrewID = 2,
                    CustomerID = 2,
                    PropertyID = 4,
                    ServiceDate = DateTime.Parse("2024-06-22"),
                    ServiceFee = 100M,
                    PaymentID = 2,
                },
                new ProvideService
                {
                    ServiceID = 5,
                    CrewID = 3,
                    CustomerID = 3,
                    PropertyID = 5,
                    ServiceDate = DateTime.Parse("2023-01-03"),
                    ServiceFee = 100M,
                    PaymentID = 3,
                },
                new ProvideService
                {
                    ServiceID = 6,
                    CrewID = 3,
                    CustomerID = 3,
                    PropertyID = 6,
                    ServiceDate = DateTime.Parse("2023-01-02"),
                    ServiceFee = 100M,
                    PaymentID = 3,
                },
                new ProvideService
                {
                    ServiceID = 7,
                    CrewID = 4,
                    CustomerID = 4,
                    PropertyID = 7,
                    ServiceDate = DateTime.Parse("2025-02-02"),
                    ServiceFee = 110M,
                    PaymentID = 4,
                },
                new ProvideService
                {
                    ServiceID = 8,
                    CrewID = 4,
                    CustomerID = 4,
                    PropertyID = 8,
                    ServiceDate = DateTime.Parse("2025-02-02"),
                    ServiceFee = 100M,
                    PaymentID = 4,
                },
                new ProvideService
                {
                    ServiceID = 9,
                    CrewID = 5,
                    CustomerID = 5,
                    PropertyID = 9,
                    ServiceDate = DateTime.Parse("2025-01-02"),
                    ServiceFee = 90M,
                    PaymentID = 5,
                },
                new ProvideService
                {
                    ServiceID = 10,
                    CrewID = 5,
                    CustomerID = 5,
                    PropertyID = 10,
                    ServiceDate = DateTime.Parse("2025-01-09"),
                    ServiceFee = 90M,
                    PaymentID = 5,
                }
                );
        }
    }
}
