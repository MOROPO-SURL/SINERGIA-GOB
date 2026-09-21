using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class Extend1SchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "elementsourceid",
                table: "proyections",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "expectedresult",
                table: "proyections",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "elementsourceid",
                table: "actions",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "expectedresult",
                table: "actions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "elementsources",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    alias = table.Column<string>(type: "text", nullable: true),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    archived = table.Column<bool>(type: "boolean", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_elementsources", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_proyections_elementsourceid",
                table: "proyections",
                column: "elementsourceid");

            migrationBuilder.CreateIndex(
                name: "ix_actions_elementsourceid",
                table: "actions",
                column: "elementsourceid");

            migrationBuilder.AddForeignKey(
                name: "fk_actions_elementsources_elementsourceid",
                table: "actions",
                column: "elementsourceid",
                principalTable: "elementsources",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_proyections_elementsources_elementsourceid",
                table: "proyections",
                column: "elementsourceid",
                principalTable: "elementsources",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_actions_elementsources_elementsourceid",
                table: "actions");

            migrationBuilder.DropForeignKey(
                name: "fk_proyections_elementsources_elementsourceid",
                table: "proyections");

            migrationBuilder.DropTable(
                name: "elementsources");

            migrationBuilder.DropIndex(
                name: "ix_proyections_elementsourceid",
                table: "proyections");

            migrationBuilder.DropIndex(
                name: "ix_actions_elementsourceid",
                table: "actions");

            migrationBuilder.DropColumn(
                name: "elementsourceid",
                table: "proyections");

            migrationBuilder.DropColumn(
                name: "expectedresult",
                table: "proyections");

            migrationBuilder.DropColumn(
                name: "elementsourceid",
                table: "actions");

            migrationBuilder.DropColumn(
                name: "expectedresult",
                table: "actions");
        }
    }
}
