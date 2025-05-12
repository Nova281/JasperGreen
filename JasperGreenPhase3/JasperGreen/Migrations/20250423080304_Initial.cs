using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JasperGreen.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillingZip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerPhone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SSN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeID);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaymentAmount = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_Payments_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    PropertyAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertyZip = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceFee = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyID);
                    table.ForeignKey(
                        name: "FK_Properties_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Crews",
                columns: table => new
                {
                    CrewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrewName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CrewForemanID = table.Column<int>(type: "int", nullable: false),
                    CrewMember1ID = table.Column<int>(type: "int", nullable: false),
                    CrewMember2ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crews", x => x.CrewID);
                    table.ForeignKey(
                        name: "FK_Crews_Employees_CrewForemanID",
                        column: x => x.CrewForemanID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Crews_Employees_CrewMember1ID",
                        column: x => x.CrewMember1ID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Crews_Employees_CrewMember2ID",
                        column: x => x.CrewMember2ID,
                        principalTable: "Employees",
                        principalColumn: "EmployeeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProvideServices",
                columns: table => new
                {
                    ServiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CrewID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    ServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ServiceFee = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    PaymentID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProvideServices", x => x.ServiceID);
                    table.ForeignKey(
                        name: "FK_ProvideServices_Crews_CrewID",
                        column: x => x.CrewID,
                        principalTable: "Crews",
                        principalColumn: "CrewID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProvideServices_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProvideServices_Payments_PaymentID",
                        column: x => x.PaymentID,
                        principalTable: "Payments",
                        principalColumn: "PaymentID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProvideServices_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "BillingAddress", "BillingCity", "BillingState", "BillingZip", "CustomerFirstName", "CustomerLastName", "CustomerPhone" },
                values: new object[,]
                {
                    { 1, "2200 Cottage Ln", "College Station", "TX", "77845", "Aarya", "Mummadavarapu", "(512) 588-9150" },
                    { 2, "85 North Campfire Street", "College Station", "TX", "77843", "Adeline ", "Wardell", "(732) 308-7385" },
                    { 3, "22 Glen Eagles Avenue", "College Station", "TX", "77845", "Dexter", "Jasmine", "(914) 258-9597" },
                    { 4, "7433 Jockey Hollow Drive", "College Station", "TX", "77842", "Gorden", "Roderick", "(210) 485-9946" },
                    { 5, "141 Silver Spear Lane", "College Station", "TX", "77845", "Kevin", "Nguyen", "(713) 555-4321" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeID", "EmployeeFirstName", "EmployeeLastName", "HireDate", "HourlyRate", "JobTitle", "SSN" },
                values: new object[,]
                {
                    { 1, "Bjork", "Sonja", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.30m, "Crewman Associate 1", "123456789" },
                    { 2, "Amina", "Sara", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.30m, "Crewman Intern", "223456789" },
                    { 3, "Pakpao", "Amporn", new DateTime(2025, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.30m, "Junior Janatorial Consultant 1", "323456789" },
                    { 4, "Mihangel", "Dai", new DateTime(2000, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.30m, "Senior Leaf Blowing Partner", "423456789" },
                    { 5, "Willihard", "Epiphanes", new DateTime(2020, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 13.35m, "Crewman Intern", "523456789" },
                    { 6, "Nikandros", "Leutgar", new DateTime(2005, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 34.34m, "Leaf Blowing Analyst 2", "523456789" },
                    { 7, "Gervasius", "Lydos", new DateTime(2004, 6, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.30m, "Crewman Associate 1", "523456789" },
                    { 8, "Attikos", "Manuel", new DateTime(2004, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.30m, "Senior Janitorial Consultant", "523456789" },
                    { 9, "Acacius", "Ardalion", new DateTime(2005, 7, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.30m, "Crewman Associate 1", "523456789" },
                    { 10, "Facundus", "Nazarius", new DateTime(2009, 5, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.30m, "Crewman Associate 1", "523456789" },
                    { 11, "Rocco", "Sophus", new DateTime(1997, 10, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.30m, "Crewman Associate 1", "523456789" },
                    { 12, "Balderich", "Raginhard", new DateTime(2016, 10, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.30m, "Crewman Associate 1", "523456789" },
                    { 13, "Artabazos", "Rufinus", new DateTime(2019, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.30m, "Crewman Associate 1", "523456789" },
                    { 14, "Vibius", "Alfarr", new DateTime(2007, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.30m, "Crewman Associate 1", "523456789" },
                    { 15, "Deusdedit", "Pygmalion", new DateTime(2017, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 30.30m, "Crewman Associate 1", "523456789" }
                });

            migrationBuilder.InsertData(
                table: "Crews",
                columns: new[] { "CrewID", "CrewForemanID", "CrewMember1ID", "CrewMember2ID", "CrewName" },
                values: new object[,]
                {
                    { 1, 1, 5, 7, "Mowing Crew 1" },
                    { 2, 2, 10, 9, "Mowing Crew 2" },
                    { 3, 11, 12, 8, "Mowing Crew 3" },
                    { 4, 6, 4, 3, "Mowing Crew 4" },
                    { 5, 13, 14, 15, "Mowing Crew 5" }
                });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentID", "CustomerID", "PaymentAmount", "PaymentDate" },
                values: new object[,]
                {
                    { 1, 1, 200.00m, new DateTime(2025, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 400.00m, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 215.00m, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 4, 210.00m, new DateTime(2025, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 5, 180.00m, new DateTime(2025, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyID", "CustomerID", "PropertyAddress", "PropertyCity", "PropertyState", "PropertyZip", "ServiceFee" },
                values: new object[,]
                {
                    { 1, 1, "2200 Cottage Lane", "College Station", "Texas", "77840", 100.00m },
                    { 2, 1, "101 University Drive", "College Station", "Texas", "77840", 100.00m },
                    { 3, 2, "500 George Bush Drive", "College Station", "Texas", "77840", 300.00m },
                    { 4, 2, "202 Southwest Parkway", "College Station", "Texas", "77840", 100.00m },
                    { 5, 3, "1501 Harvey Road", "College Station", "Texas", "77840", 100.00m },
                    { 6, 3, "909 Texas Avenue South", "College Station", "Texas", "77840", 100.00m },
                    { 7, 4, "3200 Longmire Drive", "College Station", "Texas", "77845", 110.00m },
                    { 8, 4, "1801 Holleman Dr", "College Station", "Texas", "77840", 100.00m },
                    { 9, 5, "2301 Earl Rudder Fwy South", "College Station", "Texas", "77845", 90.00m },
                    { 10, 5, "4101 Texas Ave", "College Station", "Texas", "77840", 90.00m }
                });

            migrationBuilder.InsertData(
                table: "ProvideServices",
                columns: new[] { "ServiceID", "CrewID", "CustomerID", "PaymentID", "PropertyID", "ServiceDate", "ServiceFee" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1, new DateTime(2025, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 100m },
                    { 2, 1, 1, 1, 2, new DateTime(2025, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 100m },
                    { 3, 2, 2, 2, 3, new DateTime(2024, 6, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 300m },
                    { 4, 2, 2, 2, 4, new DateTime(2024, 6, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 100m },
                    { 5, 3, 3, 3, 5, new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 100m },
                    { 6, 3, 3, 3, 6, new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 100m },
                    { 7, 4, 4, 4, 7, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 110m },
                    { 8, 4, 4, 4, 8, new DateTime(2025, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 100m },
                    { 9, 5, 5, 5, 9, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 90m },
                    { 10, 5, 5, 5, 10, new DateTime(2025, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 90m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crews_CrewForemanID",
                table: "Crews",
                column: "CrewForemanID");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_CrewMember1ID",
                table: "Crews",
                column: "CrewMember1ID");

            migrationBuilder.CreateIndex(
                name: "IX_Crews_CrewMember2ID",
                table: "Crews",
                column: "CrewMember2ID");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CustomerID",
                table: "Payments",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CustomerID",
                table: "Properties",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ProvideServices_CrewID",
                table: "ProvideServices",
                column: "CrewID");

            migrationBuilder.CreateIndex(
                name: "IX_ProvideServices_CustomerID",
                table: "ProvideServices",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_ProvideServices_PaymentID",
                table: "ProvideServices",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_ProvideServices_PropertyID",
                table: "ProvideServices",
                column: "PropertyID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProvideServices");

            migrationBuilder.DropTable(
                name: "Crews");

            migrationBuilder.DropTable(
                name: "Payments");

            migrationBuilder.DropTable(
                name: "Properties");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
