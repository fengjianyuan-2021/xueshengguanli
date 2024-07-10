using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace 学生干部考评管理系统数据库映射.Migrations
{
    /// <inheritdoc />
    public partial class AlterCloumnAvatar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Avatar",
                table: "Users",
                type: "BLOB",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "Users");
        }
    }
}
