using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace 学生干部考评管理系统数据库映射.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableinUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalScore",
                table: "Users",
                newName: "ClassHour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClassHour",
                table: "Users",
                newName: "TotalScore");
        }
    }
}
