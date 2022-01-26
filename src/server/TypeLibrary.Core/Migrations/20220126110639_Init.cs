using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TypeLibrary.Core.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlobData",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discipline = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlobData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Condition",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Format",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Format", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Aspect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Location_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Location",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PredefinedAttribute",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Values = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsMultiSelect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PredefinedAttribute", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "Purpose",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discipline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purpose", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Qualifier",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualifier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RdsCategory",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RdsCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SimpleType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Source",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Source", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TerminalType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ParentId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminalType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TerminalType_TerminalType_ParentId",
                        column: x => x.ParentId,
                        principalTable: "TerminalType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RdsCategoryId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SemanticReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aspect = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rds_RdsCategory_RdsCategoryId",
                        column: x => x.RdsCategoryId,
                        principalTable: "RdsCategory",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LibraryType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusId = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "4590637F39B6BA6F39C74293BE9138DF"),
                    TypeId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SemanticReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aspect = table.Column<int>(type: "int", nullable: false),
                    RdsId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PurposeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Unknown"),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterfaceType_TerminalTypeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LocationType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SymbolId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PredefinedAttributeData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransportType_TerminalTypeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryType_Purpose_PurposeId",
                        column: x => x.PurposeId,
                        principalTable: "Purpose",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LibraryType_Rds_RdsId",
                        column: x => x.RdsId,
                        principalTable: "Rds",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LibraryType_TerminalType_InterfaceType_TerminalTypeId",
                        column: x => x.InterfaceType_TerminalTypeId,
                        principalTable: "TerminalType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LibraryType_TerminalType_TransportType_TerminalTypeId",
                        column: x => x.TransportType_TerminalTypeId,
                        principalTable: "TerminalType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttributeType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aspect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QualifierId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SourceId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ConditionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FormatId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectValuesString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discipline = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InterfaceTypeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeType_Condition_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Condition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttributeType_Format_FormatId",
                        column: x => x.FormatId,
                        principalTable: "Format",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttributeType_LibraryType_InterfaceTypeId",
                        column: x => x.InterfaceTypeId,
                        principalTable: "LibraryType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttributeType_Qualifier_QualifierId",
                        column: x => x.QualifierId,
                        principalTable: "Qualifier",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttributeType_Source_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Source",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LibraryType_Collection",
                columns: table => new
                {
                    CollectionId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LibraryTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryType_Collection", x => new { x.CollectionId, x.LibraryTypeId });
                    table.ForeignKey(
                        name: "FK_LibraryType_Collection_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibraryType_Collection_LibraryType_LibraryTypeId",
                        column: x => x.LibraryTypeId,
                        principalTable: "LibraryType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NodeType_TerminalType",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ConnectorType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NodeTypeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TerminalTypeId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeType_TerminalType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NodeType_TerminalType_LibraryType_NodeTypeId",
                        column: x => x.NodeTypeId,
                        principalTable: "LibraryType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NodeType_TerminalType_TerminalType_TerminalTypeId",
                        column: x => x.TerminalTypeId,
                        principalTable: "TerminalType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SimpleType_NodeType",
                columns: table => new
                {
                    NodeTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SimpleTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleType_NodeType", x => new { x.NodeTypeId, x.SimpleTypeId });
                    table.ForeignKey(
                        name: "FK_SimpleType_NodeType_LibraryType_NodeTypeId",
                        column: x => x.NodeTypeId,
                        principalTable: "LibraryType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SimpleType_NodeType_SimpleType_SimpleTypeId",
                        column: x => x.SimpleTypeId,
                        principalTable: "SimpleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttributeType_Unit",
                columns: table => new
                {
                    AttributeTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UnitId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeType_Unit", x => new { x.AttributeTypeId, x.UnitId });
                    table.ForeignKey(
                        name: "FK_AttributeType_Unit_AttributeType_AttributeTypeId",
                        column: x => x.AttributeTypeId,
                        principalTable: "AttributeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttributeType_Unit_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NodeType_AttributeType",
                columns: table => new
                {
                    AttributeTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NodeTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodeType_AttributeType", x => new { x.AttributeTypeId, x.NodeTypeId });
                    table.ForeignKey(
                        name: "FK_NodeType_AttributeType_AttributeType_AttributeTypeId",
                        column: x => x.AttributeTypeId,
                        principalTable: "AttributeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NodeType_AttributeType_LibraryType_NodeTypeId",
                        column: x => x.NodeTypeId,
                        principalTable: "LibraryType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimpleType_AttributeType",
                columns: table => new
                {
                    AttributeTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SimpleTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleType_AttributeType", x => new { x.AttributeTypeId, x.SimpleTypeId });
                    table.ForeignKey(
                        name: "FK_SimpleType_AttributeType_AttributeType_AttributeTypeId",
                        column: x => x.AttributeTypeId,
                        principalTable: "AttributeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SimpleType_AttributeType_SimpleType_SimpleTypeId",
                        column: x => x.SimpleTypeId,
                        principalTable: "SimpleType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TerminalType_AttributeType",
                columns: table => new
                {
                    AttributeTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TerminalTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerminalType_AttributeType", x => new { x.AttributeTypeId, x.TerminalTypeId });
                    table.ForeignKey(
                        name: "FK_TerminalType_AttributeType_AttributeType_AttributeTypeId",
                        column: x => x.AttributeTypeId,
                        principalTable: "AttributeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TerminalType_AttributeType_TerminalType_TerminalTypeId",
                        column: x => x.TerminalTypeId,
                        principalTable: "TerminalType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransportType_AttributeType",
                columns: table => new
                {
                    AttributeTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransportTypeId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportType_AttributeType", x => new { x.AttributeTypeId, x.TransportTypeId });
                    table.ForeignKey(
                        name: "FK_TransportType_AttributeType_AttributeType_AttributeTypeId",
                        column: x => x.AttributeTypeId,
                        principalTable: "AttributeType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportType_AttributeType_LibraryType_TransportTypeId",
                        column: x => x.TransportTypeId,
                        principalTable: "LibraryType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AttributeType_ConditionId",
                table: "AttributeType",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeType_FormatId",
                table: "AttributeType",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeType_InterfaceTypeId",
                table: "AttributeType",
                column: "InterfaceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeType_QualifierId",
                table: "AttributeType",
                column: "QualifierId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeType_SourceId",
                table: "AttributeType",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeType_Unit_UnitId",
                table: "AttributeType_Unit",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryType_InterfaceType_TerminalTypeId",
                table: "LibraryType",
                column: "InterfaceType_TerminalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryType_PurposeId",
                table: "LibraryType",
                column: "PurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryType_RdsId",
                table: "LibraryType",
                column: "RdsId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryType_TransportType_TerminalTypeId",
                table: "LibraryType",
                column: "TransportType_TerminalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LibraryType_Collection_LibraryTypeId",
                table: "LibraryType_Collection",
                column: "LibraryTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_ParentId",
                table: "Location",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeType_AttributeType_NodeTypeId",
                table: "NodeType_AttributeType",
                column: "NodeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeType_TerminalType_NodeTypeId",
                table: "NodeType_TerminalType",
                column: "NodeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NodeType_TerminalType_TerminalTypeId",
                table: "NodeType_TerminalType",
                column: "TerminalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rds_RdsCategoryId",
                table: "Rds",
                column: "RdsCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_SimpleType_AttributeType_SimpleTypeId",
                table: "SimpleType_AttributeType",
                column: "SimpleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SimpleType_NodeType_SimpleTypeId",
                table: "SimpleType_NodeType",
                column: "SimpleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalType_ParentId",
                table: "TerminalType",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_TerminalType_AttributeType_TerminalTypeId",
                table: "TerminalType_AttributeType",
                column: "TerminalTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportType_AttributeType_TransportTypeId",
                table: "TransportType_AttributeType",
                column: "TransportTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AttributeType_Unit");

            migrationBuilder.DropTable(
                name: "BlobData");

            migrationBuilder.DropTable(
                name: "LibraryType_Collection");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "NodeType_AttributeType");

            migrationBuilder.DropTable(
                name: "NodeType_TerminalType");

            migrationBuilder.DropTable(
                name: "PredefinedAttribute");

            migrationBuilder.DropTable(
                name: "SimpleType_AttributeType");

            migrationBuilder.DropTable(
                name: "SimpleType_NodeType");

            migrationBuilder.DropTable(
                name: "TerminalType_AttributeType");

            migrationBuilder.DropTable(
                name: "TransportType_AttributeType");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropTable(
                name: "SimpleType");

            migrationBuilder.DropTable(
                name: "AttributeType");

            migrationBuilder.DropTable(
                name: "Condition");

            migrationBuilder.DropTable(
                name: "Format");

            migrationBuilder.DropTable(
                name: "LibraryType");

            migrationBuilder.DropTable(
                name: "Qualifier");

            migrationBuilder.DropTable(
                name: "Source");

            migrationBuilder.DropTable(
                name: "Purpose");

            migrationBuilder.DropTable(
                name: "Rds");

            migrationBuilder.DropTable(
                name: "TerminalType");

            migrationBuilder.DropTable(
                name: "RdsCategory");
        }
    }
}
