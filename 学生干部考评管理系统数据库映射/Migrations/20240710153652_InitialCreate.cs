using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace 学生干部考评管理系统数据库映射.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    Role = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OperationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Operation = table.Column<string>(type: "TEXT", nullable: false),
                    OperationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OperationLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StudentCadreInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    BasicInfo = table.Column<string>(type: "TEXT", nullable: false),
                    Position = table.Column<string>(type: "TEXT", nullable: false),
                    Organization = table.Column<string>(type: "TEXT", nullable: false),
                    SelfEvaluation = table.Column<string>(type: "TEXT", nullable: false),
                    PeerEvaluation = table.Column<string>(type: "TEXT", nullable: false),
                    TeacherEvaluation = table.Column<string>(type: "TEXT", nullable: false),
                    TotalScore = table.Column<float>(type: "REAL", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateOn = table.Column<DateTime>(type: "TEXT", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Evaluations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentCadreId = table.Column<int>(type: "INTEGER", nullable: false),
                    EvaluatorId = table.Column<int>(type: "INTEGER", nullable: false),
                    EvaluationType = table.Column<int>(type: "INTEGER", nullable: false),
                    Score = table.Column<float>(type: "REAL", nullable: false),
                    Comments = table.Column<string>(type: "TEXT", nullable: false),
                    EvaluationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    CreateOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdateOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluations_StudentCadreInfos_StudentCadreId",
                        column: x => x.StudentCadreId,
                        principalTable: "StudentCadreInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Evaluations_Users_EvaluatorId",
                        column: x => x.EvaluatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_EvaluatorId",
                table: "Evaluations",
                column: "EvaluatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluations_StudentCadreId",
                table: "Evaluations",
                column: "StudentCadreId");

            migrationBuilder.CreateIndex(
                name: "IX_OperationLogs_UserId",
                table: "OperationLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentCadreInfos_UserId",
                table: "StudentCadreInfos",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "Evaluations");

            migrationBuilder.DropTable(
                name: "OperationLogs");

            migrationBuilder.DropTable(
                name: "StudentCadreInfos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
