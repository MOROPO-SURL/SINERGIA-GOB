using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class PendingLinkInclusionSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "objectiveid",
                table: "proyections",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "proyectionid",
                table: "actions",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_proyections_objectiveid",
                table: "proyections",
                column: "objectiveid");

            migrationBuilder.CreateIndex(
                name: "ix_actions_proyectionid",
                table: "actions",
                column: "proyectionid");

            migrationBuilder.AddForeignKey(
                name: "fk_actions_proyections_proyectionid",
                table: "actions",
                column: "proyectionid",
                principalTable: "proyections",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_proyections_objectives_objectiveid",
                table: "proyections",
                column: "objectiveid",
                principalTable: "objectives",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_actions_proyections_proyectionid",
                table: "actions");

            migrationBuilder.DropForeignKey(
                name: "fk_proyections_objectives_objectiveid",
                table: "proyections");

            migrationBuilder.DropIndex(
                name: "ix_proyections_objectiveid",
                table: "proyections");

            migrationBuilder.DropIndex(
                name: "ix_actions_proyectionid",
                table: "actions");

            migrationBuilder.DropColumn(
                name: "objectiveid",
                table: "proyections");

            migrationBuilder.DropColumn(
                name: "proyectionid",
                table: "actions");
        }
    }
}
