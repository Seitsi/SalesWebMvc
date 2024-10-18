using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesWebMvc.Migrations
{
    /// <inheritdoc />
    public partial class PasswordHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Login",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "ConfirmPassword",
                table: "Login",
                newName: "ConfirmPasswordHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Login",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "ConfirmPasswordHash",
                table: "Login",
                newName: "ConfirmPassword");
        }
    }
}
