using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace 学生干部考评管理系统数据库映射.Migrations
{
    /// <inheritdoc />
    public partial class DropTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_StudentCadreInfos_StudentCadreId",
                table: "Evaluations");

            migrationBuilder.DropTable(
                name: "StudentCadreInfos");

            migrationBuilder.AddColumn<string>(
                name: "Organization",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<float>(
                name: "TotalScore",
                table: "Users",
                type: "REAL",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<int>(
                name: "StudentCadreInfoId",
                table: "Evaluations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Evaluations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_StudentCadreInfoId",
                table: "Evaluations",
                column: "StudentCadreInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_UserId",
                table: "Evaluations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Users_StudentCadreId",
                table: "Evaluations",
                column: "StudentCadreId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Users_StudentCadreInfoId",
                table: "Evaluations",
                column: "StudentCadreInfoId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Users_UserId",
                table: "Evaluations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Users_StudentCadreId",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Users_StudentCadreInfoId",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Users_UserId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_StudentCadreInfoId",
                table: "Evaluations");

            migrationBuilder.DropIndex(
                name: "IX_Evaluations_UserId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "Organization",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "TotalScore",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StudentCadreInfoId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Evaluations");

            migrationBuilder.CreateTable(
                name: "StudentCadreInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreateOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    Organization = table.Column<string>(type: "TEXT", nullable: false),
                    Position = table.Column<string>(type: "TEXT", nullable: false),
                    TotalScore = table.Column<float>(type: "REAL", nullable: false),
                    UpdateOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCadreInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentCadreInfos_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentCadreInfos_UserId",
                table: "StudentCadreInfos",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_StudentCadreInfos_StudentCadreId",
                table: "Evaluations",
                column: "StudentCadreId",
                principalTable: "StudentCadreInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
