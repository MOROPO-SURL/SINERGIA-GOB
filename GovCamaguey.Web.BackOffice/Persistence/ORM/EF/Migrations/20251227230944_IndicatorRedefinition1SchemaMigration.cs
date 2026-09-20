using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class IndicatorRedefinition1SchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_results_indicators_indicatorid",
                table: "results");

            migrationBuilder.DropTable(
                name: "indicatormontlyplans");

            migrationBuilder.DropTable(
                name: "indicators");

            migrationBuilder.CreateTable(
                name: "actionindicators",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    actionid = table.Column<Guid>(type: "uuid", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    archived = table.Column<bool>(type: "boolean", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    unitmeasurementid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_actionindicators", x => x.id);
                    table.ForeignKey(
                        name: "fk_actionindicators_actions_actionid",
                        column: x => x.actionid,
                        principalTable: "actions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_actionindicators_unitmeasurement_unitmeasurementid",
                        column: x => x.unitmeasurementid,
                        principalTable: "unitmeasurement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_actionindicators_actionid",
                table: "actionindicators",
                column: "actionid");

            migrationBuilder.CreateIndex(
                name: "ix_actionindicators_unitmeasurementid",
                table: "actionindicators",
                column: "unitmeasurementid");

            migrationBuilder.AddForeignKey(
                name: "fk_results_actionindicators_indicatorid",
                table: "results",
                column: "indicatorid",
                principalTable: "actionindicators",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_results_actionindicators_indicatorid",
                table: "results");

            migrationBuilder.DropTable(
                name: "actionindicators");

            migrationBuilder.CreateTable(
                name: "indicators",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    actionid = table.Column<Guid>(type: "uuid", nullable: false),
                    unitmeasurementid = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    archived = table.Column<bool>(type: "boolean", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_indicators", x => x.id);
                    table.ForeignKey(
                        name: "fk_indicators_actions_actionid",
                        column: x => x.actionid,
                        principalTable: "actions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_indicators_unitmeasurement_unitmeasurementid",
                        column: x => x.unitmeasurementid,
                        principalTable: "unitmeasurement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "indicatormontlyplans",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    indicatorid = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    month = table.Column<int>(type: "integer", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "ix_indicators_actionid",
                table: "indicators",
                column: "actionid");

            migrationBuilder.CreateIndex(
                name: "ix_indicators_unitmeasurementid",
                table: "indicators",
                column: "unitmeasurementid");

            migrationBuilder.AddForeignKey(
                name: "fk_results_indicators_indicatorid",
                table: "results",
                column: "indicatorid",
                principalTable: "indicators",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
