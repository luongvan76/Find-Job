using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindJobAPI.Data.FindJobAPI_DB
{
    public partial class initialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    UID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.UID);
                });

            migrationBuilder.CreateTable(
                name: "Industry",
                columns: table => new
                {
                    industry_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    industry_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Industry", x => x.industry_id);
                });

            migrationBuilder.CreateTable(
                name: "Type",
                columns: table => new
                {
                    type_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type", x => x.type_id);
                });

            migrationBuilder.CreateTable(
                name: "Employer",
                columns: table => new
                {
                    UID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    employer_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employer_website = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employer_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employer_about = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employer_image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    employer_image_cover = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employer", x => x.UID);
                    table.ForeignKey(
                        name: "FK_Employer_Account_UID",
                        column: x => x.UID,
                        principalTable: "Account",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seeker",
                columns: table => new
                {
                    UID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    skills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    education = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    major = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seeker", x => x.UID);
                    table.ForeignKey(
                        name: "FK_Seeker_Account_UID",
                        column: x => x.UID,
                        principalTable: "Account",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Job",
                columns: table => new
                {
                    job_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    job_title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    job_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    minimum_salary = table.Column<float>(type: "real", nullable: false),
                    maximum_salary = table.Column<float>(type: "real", nullable: false),
                    requirement = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false),
                    industry_id = table.Column<int>(type: "int", nullable: false),
                    type_id = table.Column<int>(type: "int", nullable: false),
                    posted_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job", x => x.job_id);
                    table.ForeignKey(
                        name: "FK_Job_Employer_UID",
                        column: x => x.UID,
                        principalTable: "Employer",
                        principalColumn: "UID");
                    table.ForeignKey(
                        name: "FK_Job_Industry_industry_id",
                        column: x => x.industry_id,
                        principalTable: "Industry",
                        principalColumn: "industry_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Job_Type_type_id",
                        column: x => x.type_id,
                        principalTable: "Type",
                        principalColumn: "type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recruitment",
                columns: table => new
                {
                    UID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    job_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: true),
                    registation_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruitment", x => new { x.UID, x.job_id });
                    table.ForeignKey(
                        name: "FK_Recruitment_Job_job_id",
                        column: x => x.job_id,
                        principalTable: "Job",
                        principalColumn: "job_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recruitment_Seeker_UID",
                        column: x => x.UID,
                        principalTable: "Seeker",
                        principalColumn: "UID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recruitment_No_Accounts",
                columns: table => new
                {
                    recruitment_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    job_id = table.Column<int>(type: "int", nullable: false),
                    fullname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    experience = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    skills = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    education = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    major = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<bool>(type: "bit", nullable: true),
                    registration_date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recruitment_No_Accounts", x => x.recruitment_ID);
                    table.ForeignKey(
                        name: "FK_Recruitment_No_Accounts_Job_job_id",
                        column: x => x.job_id,
                        principalTable: "Job",
                        principalColumn: "job_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Job_industry_id",
                table: "Job",
                column: "industry_id");

            migrationBuilder.CreateIndex(
                name: "IX_Job_type_id",
                table: "Job",
                column: "type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Job_UID",
                table: "Job",
                column: "UID");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_job_id",
                table: "Recruitment",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "IX_Recruitment_No_Accounts_job_id",
                table: "Recruitment_No_Accounts",
                column: "job_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recruitment");

            migrationBuilder.DropTable(
                name: "Recruitment_No_Accounts");

            migrationBuilder.DropTable(
                name: "Seeker");

            migrationBuilder.DropTable(
                name: "Job");

            migrationBuilder.DropTable(
                name: "Employer");

            migrationBuilder.DropTable(
                name: "Industry");

            migrationBuilder.DropTable(
                name: "Type");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
