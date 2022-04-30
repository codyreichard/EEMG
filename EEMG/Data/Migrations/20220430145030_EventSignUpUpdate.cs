using Microsoft.EntityFrameworkCore.Migrations;

namespace EEMG.Data.Migrations
{
    public partial class EventSignUpUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Speaker",
                table: "Events",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SpeakerBio",
                table: "Events",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Speaker",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "SpeakerBio",
                table: "Events");
        }
    }
}
