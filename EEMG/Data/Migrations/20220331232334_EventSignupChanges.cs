using Microsoft.EntityFrameworkCore.Migrations;

namespace EEMG.Data.Migrations
{
    public partial class EventSignupChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventUserSignUps_AspNetUsers_UserId1",
                table: "EventUserSignUps");

            migrationBuilder.DropIndex(
                name: "IX_EventUserSignUps_UserId1",
                table: "EventUserSignUps");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EventUserSignUps");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "EventUserSignUps");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "EventUserSignUps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "EventUserSignUps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "EventUserSignUps",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "EventUserSignUps",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PaidForEvent",
                table: "EventUserSignUps",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "EventUserSignUps");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "EventUserSignUps");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "EventUserSignUps");

            migrationBuilder.DropColumn(
                name: "Organization",
                table: "EventUserSignUps");

            migrationBuilder.DropColumn(
                name: "PaidForEvent",
                table: "EventUserSignUps");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "EventUserSignUps",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "EventUserSignUps",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventUserSignUps_UserId1",
                table: "EventUserSignUps",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_EventUserSignUps_AspNetUsers_UserId1",
                table: "EventUserSignUps",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
