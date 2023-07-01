using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalBB.Migrations
{
    /// <inheritdoc />
    public partial class vola : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1001, 1"),
                    AdminName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdminEmail = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AdminPassword = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DocId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Speciality = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocAge = table.Column<int>(type: "int", nullable: false),
                    DocGender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    DocPhoneNumber = table.Column<int>(type: "int", nullable: false),
                    DocImg = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DocId);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "101, 1"),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DocId = table.Column<int>(type: "int", nullable: false),
                    Issue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    DoctorDocId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppId);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorDocId",
                        column: x => x.DoctorDocId,
                        principalTable: "Doctors",
                        principalColumn: "DocId");
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    PatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "201, 1"),
                    DocId = table.Column<int>(type: "int", nullable: false),
                    PatName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatPassword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PatPhoneNumber = table.Column<int>(type: "int", nullable: false),
                    PatGender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    PatIssue = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DoctorsDocId = table.Column<int>(type: "int", nullable: true),
                    AppointmentAppId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.PatId);
                    table.ForeignKey(
                        name: "FK_Patients_Appointments_AppointmentAppId",
                        column: x => x.AppointmentAppId,
                        principalTable: "Appointments",
                        principalColumn: "AppId");
                    table.ForeignKey(
                        name: "FK_Patients_Doctors_DoctorsDocId",
                        column: x => x.DoctorsDocId,
                        principalTable: "Doctors",
                        principalColumn: "DocId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_AdminEmail",
                table: "Admins",
                column: "AdminEmail",
                unique: true,
                filter: "[AdminEmail] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorDocId",
                table: "Appointments",
                column: "DoctorDocId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_AppointmentAppId",
                table: "Patients",
                column: "AppointmentAppId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorsDocId",
                table: "Patients",
                column: "DoctorsDocId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "PatId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Doctors_DoctorDocId",
                table: "Appointments");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Doctors_DoctorsDocId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Appointments");
        }
    }
}
