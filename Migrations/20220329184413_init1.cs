using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectItiTeam.Migrations
{
    public partial class init1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "pathViedoes",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "Videos",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pathViedoes",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "username",
                table: "Videos");
        }
    }
}
