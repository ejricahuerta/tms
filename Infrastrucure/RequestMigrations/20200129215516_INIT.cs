using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Infrastrucure.RequestMigrations
{
    public partial class INIT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateAdded = table.Column<DateTime>(nullable: false),
                    RequestedBy = table.Column<string>(nullable: false),
                    Department = table.Column<string>(nullable: true),
                    DepartmentManager = table.Column<string>(nullable: false),
                    Director = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    DirectorSigned = table.Column<bool>(nullable: true),
                    DirectorSignedDate = table.Column<DateTime>(nullable: true),
                    ManagerSigned = table.Column<bool>(nullable: false),
                    ManagerSignedDate = table.Column<DateTime>(nullable: true),
                    Signed = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    NeedsInstall = table.Column<bool>(nullable: true),
                    EmployeeRequestType = table.Column<int>(nullable: true),
                    Office = table.Column<string>(nullable: true),
                    DateRequestCompletion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AXRequestAccesses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AXRequest = table.Column<int>(nullable: false),
                    MenuItem = table.Column<string>(nullable: false),
                    Company = table.Column<string>(nullable: false),
                    FullControl = table.Column<bool>(nullable: false),
                    NoAccess = table.Column<bool>(nullable: false),
                    View = table.Column<bool>(nullable: false),
                    Write = table.Column<bool>(nullable: false),
                    Create = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AXRequestAccesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AXRequestAccesses_Requests_AXRequest",
                        column: x => x.AXRequest,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequestItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RequestItemType = table.Column<int>(nullable: false),
                    EmployeeRequestId = table.Column<int>(nullable: true),
                    ITRequestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestItems_Requests_EmployeeRequestId",
                        column: x => x.EmployeeRequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestItems_Requests_ITRequestId",
                        column: x => x.ITRequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AXRequestAccesses_AXRequest",
                table: "AXRequestAccesses",
                column: "AXRequest");

            migrationBuilder.CreateIndex(
                name: "IX_RequestItems_EmployeeRequestId",
                table: "RequestItems",
                column: "EmployeeRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestItems_ITRequestId",
                table: "RequestItems",
                column: "ITRequestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AXRequestAccesses");

            migrationBuilder.DropTable(
                name: "RequestItems");

            migrationBuilder.DropTable(
                name: "Requests");
        }
    }
}
