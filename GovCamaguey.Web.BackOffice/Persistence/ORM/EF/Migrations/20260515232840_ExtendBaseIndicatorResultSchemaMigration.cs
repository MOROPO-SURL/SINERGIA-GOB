using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class ExtendBaseIndicatorResultSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "proyectionindicatorresults",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "objectiveindicatorresults",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "globalindicatorresults",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "actionindicatorresults",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "proyectionindicatorresults");

            migrationBuilder.DropColumn(
                name: "description",
                table: "objectiveindicatorresults");

            migrationBuilder.DropColumn(
                name: "description",
                table: "globalindicatorresults");

            migrationBuilder.DropColumn(
                name: "description",
                table: "actionindicatorresults");
        }
    }
}
