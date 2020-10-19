using Microsoft.EntityFrameworkCore.Migrations;

namespace MobileStore.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MobilePhones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Manufacturer = table.Column<string>(nullable: true, defaultValue: "Unknown"),
                    Size = table.Column<string>(nullable: true, defaultValue: "Unknown"),
                    Weight = table.Column<double>(nullable: false, defaultValue: 0.0),
                    ScreenSize = table.Column<string>(nullable: true, defaultValue: "Unknown"),
                    Intelligibility = table.Column<string>(nullable: true, defaultValue: "Unknown"),
                    CPU = table.Column<string>(nullable: true, defaultValue: "Unknown"),
                    Memory = table.Column<int>(nullable: false, defaultValue: 0),
                    OperatingSystem = table.Column<string>(nullable: true, defaultValue: "Unknown"),
                    Price = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MobilePhones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mediae",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MobilePhoneId = table.Column<int>(nullable: true),
                    FilePath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mediae", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mediae_MobilePhones_MobilePhoneId",
                        column: x => x.MobilePhoneId,
                        principalTable: "MobilePhones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mediae_MobilePhoneId",
                table: "Mediae",
                column: "MobilePhoneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mediae");

            migrationBuilder.DropTable(
                name: "MobilePhones");
        }
    }
}
