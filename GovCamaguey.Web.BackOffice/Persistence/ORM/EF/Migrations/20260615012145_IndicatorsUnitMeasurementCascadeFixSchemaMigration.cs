using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class IndicatorsUnitMeasurementCascadeFixSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_actionindicators_unitmeasurement_unitmeasurementid",
                table: "actionindicators");

            migrationBuilder.DropForeignKey(
                name: "fk_globalindicators_unitmeasurement_unitmeasurementid",
                table: "globalindicators");

            migrationBuilder.DropForeignKey(
                name: "fk_objectiveindicators_unitmeasurement_unitmeasurementid",
                table: "objectiveindicators");

            migrationBuilder.DropForeignKey(
                name: "fk_proyectionindicators_unitmeasurement_unitmeasurementid",
                table: "proyectionindicators");

            migrationBuilder.AddForeignKey(
                name: "fk_actionindicators_unitmeasurement_unitmeasurementid",
                table: "actionindicators",
                column: "unitmeasurementid",
                principalTable: "unitmeasurement",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_globalindicators_unitmeasurement_unitmeasurementid",
                table: "globalindicators",
                column: "unitmeasurementid",
                principalTable: "unitmeasurement",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_objectiveindicators_unitmeasurement_unitmeasurementid",
                table: "objectiveindicators",
                column: "unitmeasurementid",
                principalTable: "unitmeasurement",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_proyectionindicators_unitmeasurement_unitmeasurementid",
                table: "proyectionindicators",
                column: "unitmeasurementid",
                principalTable: "unitmeasurement",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_actionindicators_unitmeasurement_unitmeasurementid",
                table: "actionindicators");

            migrationBuilder.DropForeignKey(
                name: "fk_globalindicators_unitmeasurement_unitmeasurementid",
                table: "globalindicators");

            migrationBuilder.DropForeignKey(
                name: "fk_objectiveindicators_unitmeasurement_unitmeasurementid",
                table: "objectiveindicators");

            migrationBuilder.DropForeignKey(
                name: "fk_proyectionindicators_unitmeasurement_unitmeasurementid",
                table: "proyectionindicators");

            migrationBuilder.AddForeignKey(
                name: "fk_actionindicators_unitmeasurement_unitmeasurementid",
                table: "actionindicators",
                column: "unitmeasurementid",
                principalTable: "unitmeasurement",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_globalindicators_unitmeasurement_unitmeasurementid",
                table: "globalindicators",
                column: "unitmeasurementid",
                principalTable: "unitmeasurement",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_objectiveindicators_unitmeasurement_unitmeasurementid",
                table: "objectiveindicators",
                column: "unitmeasurementid",
                principalTable: "unitmeasurement",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "fk_proyectionindicators_unitmeasurement_unitmeasurementid",
                table: "proyectionindicators",
                column: "unitmeasurementid",
                principalTable: "unitmeasurement",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
