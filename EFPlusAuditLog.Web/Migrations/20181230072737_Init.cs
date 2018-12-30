using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFPlusAuditLog.Web.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuditEntries",
                columns: table => new
                {
                    AuditEntryID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    EntitySetName = table.Column<string>(maxLength: 255, nullable: true),
                    EntityTypeName = table.Column<string>(maxLength: 255, nullable: true),
                    State = table.Column<int>(nullable: false),
                    StateName = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEntries", x => x.AuditEntryID);
                });

            migrationBuilder.CreateTable(
                name: "Terminal",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminal", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuditEntryProperties",
                columns: table => new
                {
                    AuditEntryPropertyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AuditEntryID = table.Column<int>(nullable: false),
                    PropertyName = table.Column<string>(maxLength: 255, nullable: true),
                    RelationName = table.Column<string>(maxLength: 255, nullable: true),
                    NewValue = table.Column<string>(nullable: true),
                    OldValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditEntryProperties", x => x.AuditEntryPropertyID);
                    table.ForeignKey(
                        name: "FK_AuditEntryProperties_AuditEntries_AuditEntryID",
                        column: x => x.AuditEntryID,
                        principalTable: "AuditEntries",
                        principalColumn: "AuditEntryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuditEntryProperties_AuditEntryID",
                table: "AuditEntryProperties",
                column: "AuditEntryID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditEntryProperties");

            migrationBuilder.DropTable(
                name: "Terminal");

            migrationBuilder.DropTable(
                name: "AuditEntries");
        }
    }
}
