using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class IndicatorPlanificationInclusionSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "indicatormontlyplans",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    indicatorid = table.Column<Guid>(type: "uuid", nullable: false),
                    month = table.Column<int>(type: "integer", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_indicatormontlyplans", x => x.id);
                    table.ForeignKey(
                        name: "fk_indicatormontlyplans_indicators_indicatorid",
                        column: x => x.indicatorid,
                        principalTable: "indicators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_indicatormontlyplans_indicatorid",
                table: "indicatormontlyplans",
                column: "indicatorid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "indicatormontlyplans");
        }
    }
}
