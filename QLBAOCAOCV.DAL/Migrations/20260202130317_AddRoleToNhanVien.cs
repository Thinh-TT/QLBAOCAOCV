using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QLBAOCAOCV.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleToNhanVien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "NhanVien",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "NhanVien");
        }
    }
}
