using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class ObjectiveResponsibleCascadeFixSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_objectives_persons_responsibleid",
                table: "objectives");

            migrationBuilder.AddForeignKey(
                name: "fk_objectives_persons_responsibleid",
                table: "objectives",
                column: "responsibleid",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_objectives_persons_responsibleid",
                table: "objectives");

            migrationBuilder.AddForeignKey(
                name: "fk_objectives_persons_responsibleid",
                table: "objectives",
                column: "responsibleid",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
