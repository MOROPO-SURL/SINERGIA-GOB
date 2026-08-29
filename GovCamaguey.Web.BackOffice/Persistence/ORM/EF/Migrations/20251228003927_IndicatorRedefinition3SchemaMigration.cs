using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class IndicatorRedefinition3SchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "globalindicators",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("pk_globalindicators", x => x.id);
                    table.ForeignKey(
                        name: "fk_globalindicators_unitmeasurement_unitmeasurementid",
                        column: x => x.unitmeasurementid,
                        principalTable: "unitmeasurement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "objectiveindicators",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    objectiveid = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("pk_objectiveindicators", x => x.id);
                    table.ForeignKey(
                        name: "fk_objectiveindicators_objectives_objectiveid",
                        column: x => x.objectiveid,
                        principalTable: "objectives",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_objectiveindicators_unitmeasurement_unitmeasurementid",
                        column: x => x.unitmeasurementid,
                        principalTable: "unitmeasurement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "proyectionindicators",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    proyectionid = table.Column<Guid>(type: "uuid", nullable: false),
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
                    table.PrimaryKey("pk_proyectionindicators", x => x.id);
                    table.ForeignKey(
                        name: "fk_proyectionindicators_proyections_proyectionid",
                        column: x => x.proyectionid,
                        principalTable: "proyections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_proyectionindicators_unitmeasurement_unitmeasurementid",
                        column: x => x.unitmeasurementid,
                        principalTable: "unitmeasurement",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "globalindicatormontlyplans",
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
                    table.PrimaryKey("pk_globalindicatormontlyplans", x => x.id);
                    table.ForeignKey(
                        name: "fk_globalindicatormontlyplans_globalindicators_indicatorid",
                        column: x => x.indicatorid,
                        principalTable: "globalindicators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "objectiveindicatormontlyplans",
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
                    table.PrimaryKey("pk_objectiveindicatormontlyplans", x => x.id);
                    table.ForeignKey(
                        name: "fk_objectiveindicatormontlyplans_objectiveindicators_indicator~",
                        column: x => x.indicatorid,
                        principalTable: "objectiveindicators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "proyectionindicatormontlyplans",
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
                    table.PrimaryKey("pk_proyectionindicatormontlyplans", x => x.id);
                    table.ForeignKey(
                        name: "fk_proyectionindicatormontlyplans_proyectionindicators_indicat~",
                        column: x => x.indicatorid,
                        principalTable: "proyectionindicators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_globalindicatormontlyplans_indicatorid",
                table: "globalindicatormontlyplans",
                column: "indicatorid");

            migrationBuilder.CreateIndex(
                name: "ix_globalindicators_unitmeasurementid",
                table: "globalindicators",
                column: "unitmeasurementid");

            migrationBuilder.CreateIndex(
                name: "ix_objectiveindicatormontlyplans_indicatorid",
                table: "objectiveindicatormontlyplans",
                column: "indicatorid");

            migrationBuilder.CreateIndex(
                name: "ix_objectiveindicators_objectiveid",
                table: "objectiveindicators",
                column: "objectiveid");

            migrationBuilder.CreateIndex(
                name: "ix_objectiveindicators_unitmeasurementid",
                table: "objectiveindicators",
                column: "unitmeasurementid");

            migrationBuilder.CreateIndex(
                name: "ix_proyectionindicatormontlyplans_indicatorid",
                table: "proyectionindicatormontlyplans",
                column: "indicatorid");

            migrationBuilder.CreateIndex(
                name: "ix_proyectionindicators_proyectionid",
                table: "proyectionindicators",
                column: "proyectionid");

            migrationBuilder.CreateIndex(
                name: "ix_proyectionindicators_unitmeasurementid",
                table: "proyectionindicators",
                column: "unitmeasurementid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "globalindicatormontlyplans");

            migrationBuilder.DropTable(
                name: "objectiveindicatormontlyplans");

            migrationBuilder.DropTable(
                name: "proyectionindicatormontlyplans");

            migrationBuilder.DropTable(
                name: "globalindicators");

            migrationBuilder.DropTable(
                name: "objectiveindicators");

            migrationBuilder.DropTable(
                name: "proyectionindicators");
        }
    }
}
