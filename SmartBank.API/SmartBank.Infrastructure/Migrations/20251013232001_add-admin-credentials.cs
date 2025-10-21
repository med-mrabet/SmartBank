using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addadmincredentials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserRoles",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("d2308bf6-87f0-463e-bbdb-e9e7fa1dd85e"), 0, "address", "d30fb918-1c37-47bb-a4b9-4019e08dceb8", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@gmail.com", false, "Admin", "Admin", false, null, null, null, "AQAAAAIAAYagAAAAELCmPIPIh4NG+vZbV28RyQWnU6QBPsZMZjRJcJPzptdLRr4iY08tGuAcrpDq7frsxQ==", "23334444", false, null, false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[] { new Guid("3537c890-a749-4b75-8385-0628cf54d029"), new Guid("d2308bf6-87f0-463e-bbdb-e9e7fa1dd85e"), "ApplicationUserRole" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("3537c890-a749-4b75-8385-0628cf54d029"), new Guid("d2308bf6-87f0-463e-bbdb-e9e7fa1dd85e") });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("d2308bf6-87f0-463e-bbdb-e9e7fa1dd85e"));

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserRoles");
        }
    }
}
