using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class GlobalIndicatorResultCompleteSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "indicatorid",
                table: "globalindicatorresults",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_globalindicatorresults_indicatorid",
                table: "globalindicatorresults",
                column: "indicatorid");

            migrationBuilder.AddForeignKey(
                name: "fk_globalindicatorresults_globalindicators_indicatorid",
                table: "globalindicatorresults",
                column: "indicatorid",
                principalTable: "globalindicators",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_globalindicatorresults_globalindicators_indicatorid",
                table: "globalindicatorresults");

            migrationBuilder.DropIndex(
                name: "ix_globalindicatorresults_indicatorid",
                table: "globalindicatorresults");

            migrationBuilder.DropColumn(
                name: "indicatorid",
                table: "globalindicatorresults");
        }
    }
}
