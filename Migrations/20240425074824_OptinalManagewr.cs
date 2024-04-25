using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApi.Migrations
{
    /// <inheritdoc />
    public partial class OptinalManagewr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BranchManager",
                table: "BankBranches",
                type: "TEXT",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "BankBranches",
                keyColumn: "Id",
                keyValue: 1,
                column: "BranchManager",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BranchManager",
                table: "BankBranches");
        }
    }
}
