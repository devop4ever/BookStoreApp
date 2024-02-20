using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BookStoreApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsersAndRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a3c2ce1-c765-42c0-856b-03645d4f3f31", null, "Admin", "ADMIN" },
                    { "eadfecc0-7a4d-466f-9ab9-bb20a9e42a8e", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "17360169-1c03-4089-95b3-0294ad07aec6", 0, "c60c36a5-aec4-4b94-a6f2-5e887effc651", "user@bookstore.com", false, "System", "User", false, null, "USER@BOOKSTORE.COM", "USER", "AQAAAAIAAYagAAAAEH1vy2iy1iIAB1zV2l99V/fIK45r3KwUKWiA9DtaIodka+J54eD3f7uclYLfS6TQVw==", null, false, "02651e3e-f51d-483b-9dde-2d7e0bae72d3", false, "User" },
                    { "c6835835-8c82-4eef-8839-6e3abeb62103", 0, "41467d1d-78d7-4e52-8f80-5313c129a09f", "admin@bookstore.com", false, "System", "Admin", false, null, "ADMIN@BOOKSTORE.COM", "ADMIN", "AQAAAAIAAYagAAAAEGJ/gSqInde3/D/kpFD8VBShoQmjzO37QXK4Q8ye6pWD0rEDmxl7kpOBTiUKOk8dWw==", null, false, "f9314684-3eab-4a0c-b6d9-a2035959ff85", false, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "eadfecc0-7a4d-466f-9ab9-bb20a9e42a8e", "17360169-1c03-4089-95b3-0294ad07aec6" },
                    { "2a3c2ce1-c765-42c0-856b-03645d4f3f31", "c6835835-8c82-4eef-8839-6e3abeb62103" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "eadfecc0-7a4d-466f-9ab9-bb20a9e42a8e", "17360169-1c03-4089-95b3-0294ad07aec6" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2a3c2ce1-c765-42c0-856b-03645d4f3f31", "c6835835-8c82-4eef-8839-6e3abeb62103" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a3c2ce1-c765-42c0-856b-03645d4f3f31");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eadfecc0-7a4d-466f-9ab9-bb20a9e42a8e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "17360169-1c03-4089-95b3-0294ad07aec6");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c6835835-8c82-4eef-8839-6e3abeb62103");
        }
    }
}
