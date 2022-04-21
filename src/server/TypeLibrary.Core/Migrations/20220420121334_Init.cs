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
                name: "Attribute",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Aspect = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Discipline = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Select = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    SelectValuesString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AttributeQualifier = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    AttributeSource = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    AttributeCondition = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    AttributeFormat = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attribute_Attribute_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Attribute",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttributeAspect",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Aspect = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeAspect", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttributeAspect_AttributeAspect_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AttributeAspect",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttributeCondition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", maxLength: 127, nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeCondition", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeFormat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeFormat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributePredefined",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    IsMultiSelect = table.Column<bool>(type: "bit", nullable: false),
                    ValueStringList = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aspect = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributePredefined", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "AttributeQualifier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeQualifier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttributeSource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttributeSource", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Blob",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Discipline = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blob", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Collection",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    CompanyId = table.Column<int>(type: "int", maxLength: 127, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Purpose",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Discipline = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Purpose", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rds",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Simple",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simple", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Terminal",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terminal_Terminal_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Terminal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Unit",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Node",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    RdsId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    RdsName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    PurposeId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    PurposeName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    FirstVersionId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Aspect = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    State = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    CompanyId = table.Column<int>(type: "int", maxLength: 127, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    BlobId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    AttributeAspectId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)),
                    CreatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false, defaultValue: "Unknown"),
                    SelectedAttributePredefined = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Node", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Node_AttributeAspect_AttributeAspectId",
                        column: x => x.AttributeAspectId,
                        principalTable: "AttributeAspect",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Node_Blob_BlobId",
                        column: x => x.BlobId,
                        principalTable: "Blob",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Node_Node_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Node",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attribute_Simple",
                columns: table => new
                {
                    AttributeId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    SimpleId = table.Column<string>(type: "nvarchar(127)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute_Simple", x => new { x.AttributeId, x.SimpleId });
                    table.ForeignKey(
                        name: "FK_Attribute_Simple_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attribute_Simple_Simple_SimpleId",
                        column: x => x.SimpleId,
                        principalTable: "Simple",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interface",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    RdsId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    RdsName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    PurposeId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    PurposeName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    FirstVersionId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Aspect = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: true),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)),
                    CreatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false, defaultValue: "Unknown"),
                    Interface_TerminalId = table.Column<string>(type: "nvarchar(127)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interface", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interface_Interface_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Interface",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Interface_Terminal_Interface_TerminalId",
                        column: x => x.Interface_TerminalId,
                        principalTable: "Terminal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Terminal_Attribute",
                columns: table => new
                {
                    AttributeId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    TerminalId = table.Column<string>(type: "nvarchar(127)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminal_Attribute", x => new { x.AttributeId, x.TerminalId });
                    table.ForeignKey(
                        name: "FK_Terminal_Attribute_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Terminal_Attribute_Terminal_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transport",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Iri = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    RdsId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    RdsName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    PurposeId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    PurposeName = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    ParentId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: true),
                    Version = table.Column<string>(type: "nvarchar(7)", maxLength: 7, nullable: false),
                    FirstVersionId = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Aspect = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(511)", maxLength: 511, nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(63)", maxLength: 63, nullable: false),
                    Transport_TerminalId = table.Column<string>(type: "nvarchar(127)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transport_Terminal_Transport_TerminalId",
                        column: x => x.Transport_TerminalId,
                        principalTable: "Terminal",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transport_Transport_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Transport",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attribute_Unit",
                columns: table => new
                {
                    AttributeId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    UnitId = table.Column<string>(type: "nvarchar(127)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute_Unit", x => new { x.AttributeId, x.UnitId });
                    table.ForeignKey(
                        name: "FK_Attribute_Unit_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attribute_Unit_Unit_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attribute_Node",
                columns: table => new
                {
                    AttributeId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    NodeId = table.Column<string>(type: "nvarchar(127)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute_Node", x => new { x.AttributeId, x.NodeId });
                    table.ForeignKey(
                        name: "FK_Attribute_Node_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attribute_Node_Node_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Node_Collection",
                columns: table => new
                {
                    CollectionId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    NodeId = table.Column<string>(type: "nvarchar(127)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Node_Collection", x => new { x.CollectionId, x.NodeId });
                    table.ForeignKey(
                        name: "FK_Node_Collection_Collection_CollectionId",
                        column: x => x.CollectionId,
                        principalTable: "Collection",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Node_Collection_Node_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Simple_Node",
                columns: table => new
                {
                    NodeId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    SimpleId = table.Column<string>(type: "nvarchar(127)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Simple_Node", x => new { x.NodeId, x.SimpleId });
                    table.ForeignKey(
                        name: "FK_Simple_Node_Node_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Node",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Simple_Node_Simple_SimpleId",
                        column: x => x.SimpleId,
                        principalTable: "Simple",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Terminal_Node",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(127)", maxLength: 127, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    ConnectorDirection = table.Column<string>(type: "nvarchar(31)", maxLength: 31, nullable: false),
                    NodeId = table.Column<string>(type: "nvarchar(127)", nullable: true),
                    TerminalId = table.Column<string>(type: "nvarchar(127)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terminal_Node", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Terminal_Node_Node_NodeId",
                        column: x => x.NodeId,
                        principalTable: "Node",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Terminal_Node_Terminal_TerminalId",
                        column: x => x.TerminalId,
                        principalTable: "Terminal",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attribute_Interface",
                columns: table => new
                {
                    AttributeId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    InterfaceId = table.Column<string>(type: "nvarchar(127)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute_Interface", x => new { x.AttributeId, x.InterfaceId });
                    table.ForeignKey(
                        name: "FK_Attribute_Interface_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attribute_Interface_Interface_InterfaceId",
                        column: x => x.InterfaceId,
                        principalTable: "Interface",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attribute_Transport",
                columns: table => new
                {
                    AttributeId = table.Column<string>(type: "nvarchar(127)", nullable: false),
                    TransportId = table.Column<string>(type: "nvarchar(127)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute_Transport", x => new { x.AttributeId, x.TransportId });
                    table.ForeignKey(
                        name: "FK_Attribute_Transport_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attribute_Transport_Transport_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transport",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_ParentId",
                table: "Attribute",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Interface_InterfaceId",
                table: "Attribute_Interface",
                column: "InterfaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Node_NodeId",
                table: "Attribute_Node",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Simple_SimpleId",
                table: "Attribute_Simple",
                column: "SimpleId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Transport_TransportId",
                table: "Attribute_Transport",
                column: "TransportId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Unit_UnitId",
                table: "Attribute_Unit",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeAspect_ParentId",
                table: "AttributeAspect",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_AttributeFormat_Name",
                table: "AttributeFormat",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeQualifier_Name",
                table: "AttributeQualifier",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttributeSource_Name",
                table: "AttributeSource",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interface_Interface_TerminalId",
                table: "Interface",
                column: "Interface_TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_Interface_ParentId",
                table: "Interface",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Node_AttributeAspectId",
                table: "Node",
                column: "AttributeAspectId");

            migrationBuilder.CreateIndex(
                name: "IX_Node_BlobId",
                table: "Node",
                column: "BlobId");

            migrationBuilder.CreateIndex(
                name: "IX_Node_ParentId",
                table: "Node",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Node_Collection_NodeId",
                table: "Node_Collection",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Simple_Node_SimpleId",
                table: "Simple_Node",
                column: "SimpleId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_ParentId",
                table: "Terminal",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_Attribute_TerminalId",
                table: "Terminal_Attribute",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_Node_NodeId",
                table: "Terminal_Node",
                column: "NodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Terminal_Node_TerminalId",
                table: "Terminal_Node",
                column: "TerminalId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_ParentId",
                table: "Transport",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_Transport_TerminalId",
                table: "Transport",
                column: "Transport_TerminalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attribute_Interface");

            migrationBuilder.DropTable(
                name: "Attribute_Node");

            migrationBuilder.DropTable(
                name: "Attribute_Simple");

            migrationBuilder.DropTable(
                name: "Attribute_Transport");

            migrationBuilder.DropTable(
                name: "Attribute_Unit");

            migrationBuilder.DropTable(
                name: "AttributeCondition");

            migrationBuilder.DropTable(
                name: "AttributeFormat");

            migrationBuilder.DropTable(
                name: "AttributePredefined");

            migrationBuilder.DropTable(
                name: "AttributeQualifier");

            migrationBuilder.DropTable(
                name: "AttributeSource");

            migrationBuilder.DropTable(
                name: "Node_Collection");

            migrationBuilder.DropTable(
                name: "Purpose");

            migrationBuilder.DropTable(
                name: "Rds");

            migrationBuilder.DropTable(
                name: "Simple_Node");

            migrationBuilder.DropTable(
                name: "Terminal_Attribute");

            migrationBuilder.DropTable(
                name: "Terminal_Node");

            migrationBuilder.DropTable(
                name: "Interface");

            migrationBuilder.DropTable(
                name: "Transport");

            migrationBuilder.DropTable(
                name: "Unit");

            migrationBuilder.DropTable(
                name: "Collection");

            migrationBuilder.DropTable(
                name: "Simple");

            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.DropTable(
                name: "Node");

            migrationBuilder.DropTable(
                name: "Terminal");

            migrationBuilder.DropTable(
                name: "AttributeAspect");

            migrationBuilder.DropTable(
                name: "Blob");
        }
    }
}
