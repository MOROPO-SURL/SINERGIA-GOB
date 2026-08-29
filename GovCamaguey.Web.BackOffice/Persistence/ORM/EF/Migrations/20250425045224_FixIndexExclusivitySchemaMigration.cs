using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class FixIndexExclusivitySchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_proyections_index",
                table: "proyections");

            migrationBuilder.DropIndex(
                name: "ix_actions_index",
                table: "actions");

            migrationBuilder.CreateIndex(
                name: "ix_proyections_index_objectiveid",
                table: "proyections",
                columns: new[] { "index", "objectiveid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_actions_index_proyectionid",
                table: "actions",
                columns: new[] { "index", "proyectionid" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_proyections_index_objectiveid",
                table: "proyections");

            migrationBuilder.DropIndex(
                name: "ix_actions_index_proyectionid",
                table: "actions");

            migrationBuilder.CreateIndex(
                name: "ix_proyections_index",
                table: "proyections",
                column: "index",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_actions_index",
                table: "actions",
                column: "index",
                unique: true);
        }
    }
}
