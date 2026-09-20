using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class IndicatorRedefinition2SchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "actionindicatormontlyplans",
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
                    table.PrimaryKey("pk_actionindicatormontlyplans", x => x.id);
                    table.ForeignKey(
                        name: "fk_actionindicatormontlyplans_actionindicators_indicatorid",
                        column: x => x.indicatorid,
                        principalTable: "actionindicators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_actionindicatormontlyplans_indicatorid",
                table: "actionindicatormontlyplans",
                column: "indicatorid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "actionindicatormontlyplans");
        }
    }
}
