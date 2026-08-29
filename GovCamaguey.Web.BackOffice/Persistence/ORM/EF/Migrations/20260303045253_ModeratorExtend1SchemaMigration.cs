using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOROPO.SINETGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class ModeratorExtend1SchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isactionmoderator",
                table: "objectiveuserbridges");

            migrationBuilder.RenameColumn(
                name: "isproyectionmoderator",
                table: "objectiveuserbridges",
                newName: "isproyectionsincluded");

            migrationBuilder.RenameColumn(
                name: "isobjectivemoderator",
                table: "objectiveuserbridges",
                newName: "isactionsincluded");

            migrationBuilder.CreateTable(
                name: "actionuserbridges",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_actionuserbridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_actionuserbridges_actions_prowid",
                        column: x => x.prowid,
                        principalTable: "actions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_actionuserbridges_users_sternid",
                        column: x => x.sternid,
                        principalSchema: "guamaframework_security_personal",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "proyectionuserbridges",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<string>(type: "text", nullable: false),
                    isactionsincluded = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_proyectionuserbridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_proyectionuserbridges_proyections_prowid",
                        column: x => x.prowid,
                        principalTable: "proyections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_proyectionuserbridges_users_sternid",
                        column: x => x.sternid,
                        principalSchema: "guamaframework_security_personal",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_actionuserbridges_sternid",
                table: "actionuserbridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_proyectionuserbridges_sternid",
                table: "proyectionuserbridges",
                column: "sternid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "actionuserbridges");

            migrationBuilder.DropTable(
                name: "proyectionuserbridges");

            migrationBuilder.RenameColumn(
                name: "isproyectionsincluded",
                table: "objectiveuserbridges",
                newName: "isproyectionmoderator");

            migrationBuilder.RenameColumn(
                name: "isactionsincluded",
                table: "objectiveuserbridges",
                newName: "isobjectivemoderator");

            migrationBuilder.AddColumn<bool>(
                name: "isactionmoderator",
                table: "objectiveuserbridges",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
