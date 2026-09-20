using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MOROPO.SINERGIAGOB.GovCamaguey.Web.BackOffice.Persistence.ORM.EF.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchemaMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "guamaframework_location");

            migrationBuilder.EnsureSchema(
                name: "guamaframework_media");

            migrationBuilder.EnsureSchema(
                name: "guamaframework_identity");

            migrationBuilder.EnsureSchema(
                name: "guamaframework_core_security_common");

            migrationBuilder.EnsureSchema(
                name: "guamaframework_security_personal");

            migrationBuilder.CreateTable(
                name: "actions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    archived = table.Column<bool>(type: "boolean", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    index = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_actions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "countries",
                schema: "guamaframework_location",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    isocode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_countries", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "emailcontactitems",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    emailaddress = table.Column<string>(type: "text", nullable: false),
                    label = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_emailcontactitems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "guidelines",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    archived = table.Column<bool>(type: "boolean", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    number = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_guidelines", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                schema: "guamaframework_core_security_common",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    family = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "phonecontactitems",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    phonenumber = table.Column<string>(type: "text", nullable: false),
                    label = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_phonecontactitems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "proyections",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    archived = table.Column<bool>(type: "boolean", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    index = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_proyections", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "guamaframework_core_security_common",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "socialcontactitems",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    socialid = table.Column<string>(type: "text", nullable: false),
                    label = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_socialcontactitems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "thumbnails",
                schema: "guamaframework_media",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contenttype = table.Column<string>(type: "text", nullable: false),
                    length = table.Column<long>(type: "bigint", nullable: false),
                    about = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_thumbnails", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "traces",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<int>(type: "integer", nullable: false),
                    author = table.Column<string>(type: "text", nullable: true),
                    registered = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    entityname = table.Column<string>(type: "text", nullable: false),
                    beforedata = table.Column<string>(type: "text", nullable: false),
                    afterdata = table.Column<string>(type: "text", nullable: true),
                    nextid = table.Column<long>(type: "bigint", nullable: false),
                    signature = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_traces", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "unitmeasurement",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    symbol = table.Column<string>(type: "text", nullable: false),
                    archived = table.Column<bool>(type: "boolean", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_unitmeasurement", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "websitecontactitems",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    label = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_websitecontactitems", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "actionmonthlyclosures",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    actionid = table.Column<Guid>(type: "uuid", nullable: false),
                    at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_actionmonthlyclosures", x => x.id);
                    table.ForeignKey(
                        name: "fk_actionmonthlyclosures_actions_actionid",
                        column: x => x.actionid,
                        principalTable: "actions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "regions",
                schema: "guamaframework_location",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    countryid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_regions", x => x.id);
                    table.ForeignKey(
                        name: "fk_regions_countries_countryid",
                        column: x => x.countryid,
                        principalSchema: "guamaframework_location",
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "proyectionguidelinebridges",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_proyectionguidelinebridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_proyectionguidelinebridges_guidelines_sternid",
                        column: x => x.sternid,
                        principalTable: "guidelines",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_proyectionguidelinebridges_proyections_prowid",
                        column: x => x.prowid,
                        principalTable: "proyections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "proyectionmonthlyclosures",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    proyectionid = table.Column<Guid>(type: "uuid", nullable: false),
                    at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_proyectionmonthlyclosures", x => x.id);
                    table.ForeignKey(
                        name: "fk_proyectionmonthlyclosures_proyections_proyectionid",
                        column: x => x.proyectionid,
                        principalTable: "proyections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rolepermissionbridges",
                schema: "guamaframework_core_security_common",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rolepermissionbridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_rolepermissionbridges_permissions_sternid",
                        column: x => x.sternid,
                        principalSchema: "guamaframework_core_security_common",
                        principalTable: "permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_rolepermissionbridges_roles_prowid",
                        column: x => x.prowid,
                        principalSchema: "guamaframework_core_security_common",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "documents",
                schema: "guamaframework_media",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    contenttype = table.Column<string>(type: "text", nullable: false),
                    length = table.Column<long>(type: "bigint", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    thumbnailid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_documents", x => x.id);
                    table.ForeignKey(
                        name: "fk_documents_thumbnails_thumbnailid",
                        column: x => x.thumbnailid,
                        principalSchema: "guamaframework_media",
                        principalTable: "thumbnails",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "images",
                schema: "guamaframework_media",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    contenttype = table.Column<string>(type: "text", nullable: false),
                    length = table.Column<long>(type: "bigint", nullable: false),
                    about = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    height = table.Column<int>(type: "integer", nullable: false),
                    width = table.Column<int>(type: "integer", nullable: false),
                    thumbnailid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_images", x => x.id);
                    table.ForeignKey(
                        name: "fk_images_thumbnails_thumbnailid",
                        column: x => x.thumbnailid,
                        principalSchema: "guamaframework_media",
                        principalTable: "thumbnails",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "videos",
                schema: "guamaframework_media",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    createat = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    extension = table.Column<string>(type: "text", nullable: false),
                    length = table.Column<long>(type: "bigint", nullable: false),
                    about = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    duration = table.Column<TimeSpan>(type: "interval", nullable: false),
                    format = table.Column<string>(type: "text", nullable: false),
                    framesize = table.Column<string>(type: "text", nullable: false),
                    colormodel = table.Column<string>(type: "text", nullable: false),
                    fps = table.Column<double>(type: "double precision", nullable: false),
                    audioformat = table.Column<string>(type: "text", nullable: false),
                    audiochanneloutput = table.Column<string>(type: "text", nullable: false),
                    thumbnailid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_videos", x => x.id);
                    table.ForeignKey(
                        name: "fk_videos_thumbnails_thumbnailid",
                        column: x => x.thumbnailid,
                        principalSchema: "guamaframework_media",
                        principalTable: "thumbnails",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "indicators",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    archived = table.Column<bool>(type: "boolean", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    index = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    unitmeasurementid = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false),
                    actionid = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "states",
                schema: "guamaframework_location",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    regionid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_states", x => x.id);
                    table.ForeignKey(
                        name: "fk_states_regions_regionid",
                        column: x => x.regionid,
                        principalSchema: "guamaframework_location",
                        principalTable: "regions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationidentities",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ownerid = table.Column<Guid>(type: "uuid", nullable: true),
                    imageid = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    alias = table.Column<string>(type: "text", nullable: true),
                    borndate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    about = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationidentities", x => x.id);
                    table.ForeignKey(
                        name: "fk_organizationidentities_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "guamaframework_media",
                        principalTable: "images",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_organizationidentities_organizationidentities_ownerid",
                        column: x => x.ownerid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "personidentities",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    lastname = table.Column<string>(type: "text", nullable: true),
                    sex = table.Column<int>(type: "integer", nullable: false),
                    civilstatus = table.Column<int>(type: "integer", nullable: false),
                    imageid = table.Column<Guid>(type: "uuid", nullable: true),
                    name = table.Column<string>(type: "text", nullable: false),
                    alias = table.Column<string>(type: "text", nullable: true),
                    borndate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    about = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personidentities", x => x.id);
                    table.ForeignKey(
                        name: "fk_personidentities_images_imageid",
                        column: x => x.imageid,
                        principalSchema: "guamaframework_media",
                        principalTable: "images",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "results",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    indicatorid = table.Column<Guid>(type: "uuid", nullable: false),
                    amount = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_results", x => x.id);
                    table.ForeignKey(
                        name: "fk_results_indicators_indicatorid",
                        column: x => x.indicatorid,
                        principalTable: "indicators",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "municipalities",
                schema: "guamaframework_location",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    stateid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_municipalities", x => x.id);
                    table.ForeignKey(
                        name: "fk_municipalities_states_stateid",
                        column: x => x.stateid,
                        principalSchema: "guamaframework_location",
                        principalTable: "states",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationemailcontactitembridges",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationemailcontactitembridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_organizationemailcontactitembridges_emailcontactitems_stern~",
                        column: x => x.sternid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "emailcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationemailcontactitembridges_organizationidentities_~",
                        column: x => x.prowid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationphonecontactitembridges",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationphonecontactitembridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_organizationphonecontactitembridges_organizationidentities_~",
                        column: x => x.prowid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationphonecontactitembridges_phonecontactitems_stern~",
                        column: x => x.sternid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "phonecontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    archived = table.Column<bool>(type: "boolean", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    identityid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizations", x => x.id);
                    table.ForeignKey(
                        name: "fk_organizations_organizationidentities_identityid",
                        column: x => x.identityid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationsocialcontactitembridges",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationsocialcontactitembridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_organizationsocialcontactitembridges_organizationidentities~",
                        column: x => x.prowid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationsocialcontactitembridges_socialcontactitems_ste~",
                        column: x => x.sternid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "socialcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationwebsitecontactitembridges",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationwebsitecontactitembridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_organizationwebsitecontactitembridges_organizationidentitie~",
                        column: x => x.prowid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationwebsitecontactitembridges_websitecontactitems_s~",
                        column: x => x.sternid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "websitecontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "organizationpersonmembershipbridges",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationpersonmembershipbridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_organizationpersonmembershipbridges_organizationidentities_~",
                        column: x => x.prowid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationpersonmembershipbridges_personidentities_sternid",
                        column: x => x.sternid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personemailcontactitembridges",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personemailcontactitembridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_personemailcontactitembridges_emailcontactitems_sternid",
                        column: x => x.sternid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "emailcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personemailcontactitembridges_personidentities_prowid",
                        column: x => x.prowid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personphonecontactitembridges",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personphonecontactitembridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_personphonecontactitembridges_personidentities_prowid",
                        column: x => x.prowid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personphonecontactitembridges_phonecontactitems_sternid",
                        column: x => x.sternid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "phonecontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "persons",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    archived = table.Column<bool>(type: "boolean", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    identityid = table.Column<Guid>(type: "uuid", nullable: false),
                    role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_persons", x => x.id);
                    table.ForeignKey(
                        name: "fk_persons_personidentities_identityid",
                        column: x => x.identityid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personsocialcontactitembridges",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personsocialcontactitembridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_personsocialcontactitembridges_personidentities_prowid",
                        column: x => x.prowid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personsocialcontactitembridges_socialcontactitems_sternid",
                        column: x => x.sternid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "socialcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personwebsitecontactitembridges",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personwebsitecontactitembridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_personwebsitecontactitembridges_personidentities_prowid",
                        column: x => x.prowid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personwebsitecontactitembridges_websitecontactitems_sternid",
                        column: x => x.sternid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "websitecontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "guamaframework_security_personal",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    passwordchangerequired = table.Column<bool>(type: "boolean", nullable: false),
                    hasfullaccess = table.Column<bool>(type: "boolean", nullable: false),
                    created = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    lastsessiondate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    failauthenticationcount = table.Column<int>(type: "integer", nullable: false),
                    identityid = table.Column<Guid>(type: "uuid", nullable: false),
                    signature = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_users_personidentities_identityid",
                        column: x => x.identityid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "localities",
                schema: "guamaframework_location",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    municipalityid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_localities", x => x.id);
                    table.ForeignKey(
                        name: "fk_localities_municipalities_municipalityid",
                        column: x => x.municipalityid,
                        principalSchema: "guamaframework_location",
                        principalTable: "municipalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "actionorganizationparticipationbridges",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_actionorganizationparticipationbridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_actionorganizationparticipationbridges_actions_prowid",
                        column: x => x.prowid,
                        principalTable: "actions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_actionorganizationparticipationbridges_organizations_sternid",
                        column: x => x.sternid,
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "actionorganizationresponsabilitybridges",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_actionorganizationresponsabilitybridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_actionorganizationresponsabilitybridges_actions_prowid",
                        column: x => x.prowid,
                        principalTable: "actions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_actionorganizationresponsabilitybridges_organizations_stern~",
                        column: x => x.sternid,
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "proyectionorganizationparticipationbridges",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_proyectionorganizationparticipationbridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_proyectionorganizationparticipationbridges_organizations_st~",
                        column: x => x.sternid,
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_proyectionorganizationparticipationbridges_proyections_prow~",
                        column: x => x.prowid,
                        principalTable: "proyections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "proyectionorganizationresponsabilitybridges",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_proyectionorganizationresponsabilitybridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_proyectionorganizationresponsabilitybridges_organizations_s~",
                        column: x => x.sternid,
                        principalTable: "organizations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_proyectionorganizationresponsabilitybridges_proyections_pro~",
                        column: x => x.prowid,
                        principalTable: "proyections",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "objectives",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    archived = table.Column<bool>(type: "boolean", nullable: false),
                    enabled = table.Column<bool>(type: "boolean", nullable: false),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    index = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    responsibleid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_objectives", x => x.id);
                    table.ForeignKey(
                        name: "fk_objectives_persons_responsibleid",
                        column: x => x.responsibleid,
                        principalTable: "persons",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "userrolebridges",
                schema: "guamaframework_security_personal",
                columns: table => new
                {
                    prowid = table.Column<string>(type: "text", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false),
                    signature = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_userrolebridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_userrolebridges_roles_sternid",
                        column: x => x.sternid,
                        principalSchema: "guamaframework_core_security_common",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_userrolebridges_users_prowid",
                        column: x => x.prowid,
                        principalSchema: "guamaframework_security_personal",
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "locations",
                schema: "guamaframework_location",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    discriminator = table.Column<string>(type: "text", nullable: false),
                    localityid = table.Column<Guid>(type: "uuid", nullable: true),
                    municipalityid = table.Column<Guid>(type: "uuid", nullable: true),
                    stateid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_locations", x => x.id);
                    table.ForeignKey(
                        name: "fk_locations_localities_localityid",
                        column: x => x.localityid,
                        principalSchema: "guamaframework_location",
                        principalTable: "localities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_locations_municipalities_municipalityid",
                        column: x => x.municipalityid,
                        principalSchema: "guamaframework_location",
                        principalTable: "municipalities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_locations_states_stateid",
                        column: x => x.stateid,
                        principalSchema: "guamaframework_location",
                        principalTable: "states",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "postalcontactitems",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    locationid = table.Column<Guid>(type: "uuid", nullable: true),
                    zipcode = table.Column<int>(type: "integer", nullable: false),
                    longitude = table.Column<decimal>(type: "numeric", nullable: false),
                    latitude = table.Column<decimal>(type: "numeric", nullable: false),
                    label = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_postalcontactitems", x => x.id);
                    table.ForeignKey(
                        name: "fk_postalcontactitems_locations_locationid",
                        column: x => x.locationid,
                        principalSchema: "guamaframework_location",
                        principalTable: "locations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "organizationpostalcontactitembridges",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_organizationpostalcontactitembridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_organizationpostalcontactitembridges_organizationidentities~",
                        column: x => x.prowid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "organizationidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_organizationpostalcontactitembridges_postalcontactitems_ste~",
                        column: x => x.sternid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "postalcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "personpostalcontactitembridges",
                schema: "guamaframework_identity",
                columns: table => new
                {
                    prowid = table.Column<Guid>(type: "uuid", nullable: false),
                    sternid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_personpostalcontactitembridges", x => new { x.prowid, x.sternid });
                    table.ForeignKey(
                        name: "fk_personpostalcontactitembridges_personidentities_prowid",
                        column: x => x.prowid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "personidentities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_personpostalcontactitembridges_postalcontactitems_sternid",
                        column: x => x.sternid,
                        principalSchema: "guamaframework_identity",
                        principalTable: "postalcontactitems",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_actionmonthlyclosures_actionid",
                table: "actionmonthlyclosures",
                column: "actionid");

            migrationBuilder.CreateIndex(
                name: "ix_actionorganizationparticipationbridges_sternid",
                table: "actionorganizationparticipationbridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_actionorganizationresponsabilitybridges_sternid",
                table: "actionorganizationresponsabilitybridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_actions_index",
                table: "actions",
                column: "index",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_countries_isocode",
                schema: "guamaframework_location",
                table: "countries",
                column: "isocode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_countries_name",
                schema: "guamaframework_location",
                table: "countries",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_documents_thumbnailid",
                schema: "guamaframework_media",
                table: "documents",
                column: "thumbnailid");

            migrationBuilder.CreateIndex(
                name: "ix_images_thumbnailid",
                schema: "guamaframework_media",
                table: "images",
                column: "thumbnailid");

            migrationBuilder.CreateIndex(
                name: "ix_indicators_actionid",
                table: "indicators",
                column: "actionid");

            migrationBuilder.CreateIndex(
                name: "ix_indicators_unitmeasurementid",
                table: "indicators",
                column: "unitmeasurementid");

            migrationBuilder.CreateIndex(
                name: "ix_localities_municipalityid",
                schema: "guamaframework_location",
                table: "localities",
                column: "municipalityid");

            migrationBuilder.CreateIndex(
                name: "ix_localities_name_municipalityid",
                schema: "guamaframework_location",
                table: "localities",
                columns: new[] { "name", "municipalityid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_locations_localityid",
                schema: "guamaframework_location",
                table: "locations",
                column: "localityid");

            migrationBuilder.CreateIndex(
                name: "ix_locations_municipalityid",
                schema: "guamaframework_location",
                table: "locations",
                column: "municipalityid");

            migrationBuilder.CreateIndex(
                name: "ix_locations_stateid",
                schema: "guamaframework_location",
                table: "locations",
                column: "stateid");

            migrationBuilder.CreateIndex(
                name: "ix_municipalities_name_stateid",
                schema: "guamaframework_location",
                table: "municipalities",
                columns: new[] { "name", "stateid" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_municipalities_stateid",
                schema: "guamaframework_location",
                table: "municipalities",
                column: "stateid");

            migrationBuilder.CreateIndex(
                name: "ix_objectives_index",
                table: "objectives",
                column: "index",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_objectives_responsibleid",
                table: "objectives",
                column: "responsibleid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationemailcontactitembridges_sternid",
                schema: "guamaframework_identity",
                table: "organizationemailcontactitembridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationidentities_imageid",
                schema: "guamaframework_identity",
                table: "organizationidentities",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationidentities_ownerid",
                schema: "guamaframework_identity",
                table: "organizationidentities",
                column: "ownerid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationpersonmembershipbridges_sternid",
                schema: "guamaframework_identity",
                table: "organizationpersonmembershipbridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationphonecontactitembridges_sternid",
                schema: "guamaframework_identity",
                table: "organizationphonecontactitembridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationpostalcontactitembridges_sternid",
                schema: "guamaframework_identity",
                table: "organizationpostalcontactitembridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_organizations_identityid",
                table: "organizations",
                column: "identityid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_organizationsocialcontactitembridges_sternid",
                schema: "guamaframework_identity",
                table: "organizationsocialcontactitembridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_organizationwebsitecontactitembridges_sternid",
                schema: "guamaframework_identity",
                table: "organizationwebsitecontactitembridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_personemailcontactitembridges_sternid",
                schema: "guamaframework_identity",
                table: "personemailcontactitembridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_personidentities_imageid",
                schema: "guamaframework_identity",
                table: "personidentities",
                column: "imageid");

            migrationBuilder.CreateIndex(
                name: "ix_personphonecontactitembridges_sternid",
                schema: "guamaframework_identity",
                table: "personphonecontactitembridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_personpostalcontactitembridges_sternid",
                schema: "guamaframework_identity",
                table: "personpostalcontactitembridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_persons_identityid",
                table: "persons",
                column: "identityid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_personsocialcontactitembridges_sternid",
                schema: "guamaframework_identity",
                table: "personsocialcontactitembridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_personwebsitecontactitembridges_sternid",
                schema: "guamaframework_identity",
                table: "personwebsitecontactitembridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_postalcontactitems_locationid",
                schema: "guamaframework_identity",
                table: "postalcontactitems",
                column: "locationid");

            migrationBuilder.CreateIndex(
                name: "ix_proyectionguidelinebridges_sternid",
                table: "proyectionguidelinebridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_proyectionmonthlyclosures_proyectionid",
                table: "proyectionmonthlyclosures",
                column: "proyectionid");

            migrationBuilder.CreateIndex(
                name: "ix_proyectionorganizationparticipationbridges_sternid",
                table: "proyectionorganizationparticipationbridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_proyectionorganizationresponsabilitybridges_sternid",
                table: "proyectionorganizationresponsabilitybridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_proyections_index",
                table: "proyections",
                column: "index",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_regions_countryid",
                schema: "guamaframework_location",
                table: "regions",
                column: "countryid");

            migrationBuilder.CreateIndex(
                name: "ix_results_indicatorid",
                table: "results",
                column: "indicatorid");

            migrationBuilder.CreateIndex(
                name: "ix_rolepermissionbridges_sternid",
                schema: "guamaframework_core_security_common",
                table: "rolepermissionbridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_states_regionid",
                schema: "guamaframework_location",
                table: "states",
                column: "regionid");

            migrationBuilder.CreateIndex(
                name: "ix_userrolebridges_sternid",
                schema: "guamaframework_security_personal",
                table: "userrolebridges",
                column: "sternid");

            migrationBuilder.CreateIndex(
                name: "ix_users_identityid",
                schema: "guamaframework_security_personal",
                table: "users",
                column: "identityid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_videos_thumbnailid",
                schema: "guamaframework_media",
                table: "videos",
                column: "thumbnailid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "actionmonthlyclosures");

            migrationBuilder.DropTable(
                name: "actionorganizationparticipationbridges");

            migrationBuilder.DropTable(
                name: "actionorganizationresponsabilitybridges");

            migrationBuilder.DropTable(
                name: "documents",
                schema: "guamaframework_media");

            migrationBuilder.DropTable(
                name: "objectives");

            migrationBuilder.DropTable(
                name: "organizationemailcontactitembridges",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "organizationpersonmembershipbridges",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "organizationphonecontactitembridges",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "organizationpostalcontactitembridges",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "organizationsocialcontactitembridges",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "organizationwebsitecontactitembridges",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "personemailcontactitembridges",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "personphonecontactitembridges",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "personpostalcontactitembridges",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "personsocialcontactitembridges",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "personwebsitecontactitembridges",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "proyectionguidelinebridges");

            migrationBuilder.DropTable(
                name: "proyectionmonthlyclosures");

            migrationBuilder.DropTable(
                name: "proyectionorganizationparticipationbridges");

            migrationBuilder.DropTable(
                name: "proyectionorganizationresponsabilitybridges");

            migrationBuilder.DropTable(
                name: "results");

            migrationBuilder.DropTable(
                name: "rolepermissionbridges",
                schema: "guamaframework_core_security_common");

            migrationBuilder.DropTable(
                name: "traces");

            migrationBuilder.DropTable(
                name: "userrolebridges",
                schema: "guamaframework_security_personal");

            migrationBuilder.DropTable(
                name: "videos",
                schema: "guamaframework_media");

            migrationBuilder.DropTable(
                name: "persons");

            migrationBuilder.DropTable(
                name: "emailcontactitems",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "phonecontactitems",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "postalcontactitems",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "socialcontactitems",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "websitecontactitems",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "guidelines");

            migrationBuilder.DropTable(
                name: "organizations");

            migrationBuilder.DropTable(
                name: "proyections");

            migrationBuilder.DropTable(
                name: "indicators");

            migrationBuilder.DropTable(
                name: "permissions",
                schema: "guamaframework_core_security_common");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "guamaframework_core_security_common");

            migrationBuilder.DropTable(
                name: "users",
                schema: "guamaframework_security_personal");

            migrationBuilder.DropTable(
                name: "locations",
                schema: "guamaframework_location");

            migrationBuilder.DropTable(
                name: "organizationidentities",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "actions");

            migrationBuilder.DropTable(
                name: "unitmeasurement");

            migrationBuilder.DropTable(
                name: "personidentities",
                schema: "guamaframework_identity");

            migrationBuilder.DropTable(
                name: "localities",
                schema: "guamaframework_location");

            migrationBuilder.DropTable(
                name: "images",
                schema: "guamaframework_media");

            migrationBuilder.DropTable(
                name: "municipalities",
                schema: "guamaframework_location");

            migrationBuilder.DropTable(
                name: "thumbnails",
                schema: "guamaframework_media");

            migrationBuilder.DropTable(
                name: "states",
                schema: "guamaframework_location");

            migrationBuilder.DropTable(
                name: "regions",
                schema: "guamaframework_location");

            migrationBuilder.DropTable(
                name: "countries",
                schema: "guamaframework_location");
        }
    }
}
