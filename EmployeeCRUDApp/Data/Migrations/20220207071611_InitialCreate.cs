using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeCRUDApp.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EMPCODE = table.Column<int>(type: "int", nullable: false),
                    EMPNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DESIGNATION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SALARY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
