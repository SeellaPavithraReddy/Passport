using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PVMS_Project.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cost_6",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Months = table.Column<int>(type: "int", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cost_6", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Register_6",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Firstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contactno = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Qualification = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplyType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HintQuestion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HintAnswer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CitizenType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Register_6", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "State_6",
                columns: table => new
                {
                    State_Id = table.Column<int>(type: "int", nullable: false),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State_6", x => x.State_Id);
                });

            migrationBuilder.CreateTable(
                name: "VisaApplicationCost_6",
                columns: table => new
                {
                    Occupation = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisaApplicationCost_6", x => x.Occupation);
                });

            migrationBuilder.CreateTable(
                name: "Passport_6",
                columns: table => new
                {
                    Passport_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    User_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Booklet_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Issue_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expiry_date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Pincode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type_of_services = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationCost = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passport_6", x => x.Passport_id);
                    table.ForeignKey(
                        name: "FK_Passport_6_Register_6_User_id",
                        column: x => x.User_id,
                        principalTable: "Register_6",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City_6",
                columns: table => new
                {
                    Pincode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    City_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City_6", x => x.Pincode);
                    table.ForeignKey(
                        name: "FK_City_6_State_6_State_Id",
                        column: x => x.State_Id,
                        principalTable: "State_6",
                        principalColumn: "State_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Visa_6",
                columns: table => new
                {
                    VisaId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfApplication = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Occupation = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RegistrationCost = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visa_6", x => x.VisaId);
                    table.ForeignKey(
                        name: "FK_Visa_6_Passport_6_PassportId",
                        column: x => x.PassportId,
                        principalTable: "Passport_6",
                        principalColumn: "Passport_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Visa_6_VisaApplicationCost_6_Occupation",
                        column: x => x.Occupation,
                        principalTable: "VisaApplicationCost_6",
                        principalColumn: "Occupation",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_City_6_State_Id",
                table: "City_6",
                column: "State_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Passport_6_User_id",
                table: "Passport_6",
                column: "User_id");

            migrationBuilder.CreateIndex(
                name: "IX_Register_6_Contactno",
                table: "Register_6",
                column: "Contactno",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Register_6_Email",
                table: "Register_6",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Visa_6_Occupation",
                table: "Visa_6",
                column: "Occupation");

            migrationBuilder.CreateIndex(
                name: "IX_Visa_6_PassportId",
                table: "Visa_6",
                column: "PassportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "City_6");

            migrationBuilder.DropTable(
                name: "Cost_6");

            migrationBuilder.DropTable(
                name: "Visa_6");

            migrationBuilder.DropTable(
                name: "State_6");

            migrationBuilder.DropTable(
                name: "Passport_6");

            migrationBuilder.DropTable(
                name: "VisaApplicationCost_6");

            migrationBuilder.DropTable(
                name: "Register_6");
        }
    }
}
