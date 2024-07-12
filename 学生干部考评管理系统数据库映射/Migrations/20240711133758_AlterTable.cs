using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace 学生干部考评管理系统数据库映射.Migrations
{
    /// <inheritdoc />
    public partial class AlterTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasicInfo",
                table: "StudentCadreInfos");

            migrationBuilder.DropColumn(
                name: "PeerEvaluation",
                table: "StudentCadreInfos");

            migrationBuilder.DropColumn(
                name: "SelfEvaluation",
                table: "StudentCadreInfos");

            migrationBuilder.DropColumn(
                name: "TeacherEvaluation",
                table: "StudentCadreInfos");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Users",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "BasicInfo",
                table: "StudentCadreInfos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PeerEvaluation",
                table: "StudentCadreInfos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SelfEvaluation",
                table: "StudentCadreInfos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeacherEvaluation",
                table: "StudentCadreInfos",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
