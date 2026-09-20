using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceTargetPriorityByIndexSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "priority",
                table: "proyections");

            migrationBuilder.DropColumn(
                name: "priority",
                table: "objectives");

            migrationBuilder.DropColumn(
                name: "number",
                table: "guidelines");

            migrationBuilder.DropColumn(
                name: "priority",
                table: "actions");

            migrationBuilder.RenameColumn(
                name: "priority",
                table: "guidelines",
                newName: "index");

            migrationBuilder.CreateIndex(
                name: "ix_guidelines_index",
                table: "guidelines",
                column: "index",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_guidelines_index",
                table: "guidelines");

            migrationBuilder.RenameColumn(
                name: "index",
                table: "guidelines",
                newName: "priority");

            migrationBuilder.AddColumn<int>(
                name: "priority",
                table: "proyections",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "priority",
                table: "objectives",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "number",
                table: "guidelines",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "priority",
                table: "actions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
