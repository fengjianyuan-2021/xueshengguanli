using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace 学生干部考评管理系统数据库映射.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableUserInmap : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Users_EvaluatorId",
                table: "Evaluations");

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
                name: "StudentCadreInfoId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Evaluations");

            migrationBuilder.AlterColumn<float>(
                name: "Score",
                table: "Evaluations",
                type: "REAL",
                nullable: true,
                oldClrType: typeof(float),
                oldType: "REAL");

            migrationBuilder.AlterColumn<DateTime>(
                name: "EvaluationDate",
                table: "Evaluations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "Evaluations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Users_EvaluatorId",
                table: "Evaluations",
                column: "EvaluatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Evaluations_Users_StudentCadreId",
                table: "Evaluations",
                column: "StudentCadreId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Users_EvaluatorId",
                table: "Evaluations");

            migrationBuilder.DropForeignKey(
                name: "FK_Evaluations_Users_StudentCadreId",
                table: "Evaluations");

            migrationBuilder.AlterColumn<float>(
                name: "Score",
                table: "Evaluations",
                type: "REAL",
                nullable: false,
                defaultValue: 0f,
                oldClrType: typeof(float),
                oldType: "REAL",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "EvaluationDate",
                table: "Evaluations",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Comments",
                table: "Evaluations",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

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
                name: "FK_Evaluations_Users_EvaluatorId",
                table: "Evaluations",
                column: "EvaluatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
