using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryExpress.Model.Migrations
{
    internal partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false),
                    Tag = table.Column<string>(type: "VARCHAR(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Media", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Currency = table.Column<string>(type: "VARCHAR(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(type: "VARCHAR(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false),
                    MediaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attribute_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Condition",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Grade = table.Column<int>(type: "INTEGER (1)", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR (64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR (36)", nullable: false),
                    MediaId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Condition_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CostCenter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false),
                    MediaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Tag = table.Column<string>(type: "VARCHAR(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CostCenter_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "LedgerAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false),
                    MediaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Tag = table.Column<string>(type: "VARCHAR(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LedgerAccount_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Building = table.Column<string>(type: "VARCHAR (64)", nullable: true),
                    Room = table.Column<string>(type: "VARCHAR (64)", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR (64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR (36)", nullable: false),
                    MediaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Tag = table.Column<string>(type: "VARCHAR (256)", nullable: true),
                    Address = table.Column<string>(type: "VARCHAR (256)", nullable: true),
                    Zip = table.Column<string>(type: "VARCHAR (10)", nullable: true),
                    Place = table.Column<string>(type: "VARCHAR (64)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false),
                    MediaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Tag = table.Column<string>(type: "VARCHAR(256)", nullable: true),
                    Address = table.Column<string>(type: "VARCHAR(256)", nullable: true),
                    Zip = table.Column<string>(type: "VARCHAR(10)", nullable: true),
                    Place = table.Column<string>(type: "VARCHAR(64)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Manufacturer_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR (64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR (36)", nullable: false),
                    MediaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Tag = table.Column<string>(type: "VARCHAR (256)", nullable: true),
                    Address = table.Column<string>(type: "VARCHAR (256)", nullable: true),
                    Zip = table.Column<string>(type: "VARCHAR (10)", nullable: true),
                    Place = table.Column<string>(type: "VARCHAR (64)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Supplier_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false),
                    MediaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Tag = table.Column<string>(type: "VARCHAR(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Template_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CostValue = table.Column<decimal>(type: "DECIMAL", nullable: false),
                    TemplateId = table.Column<int>(type: "INTEGER", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "DATE", nullable: true),
                    DerecognitionDate = table.Column<DateTime>(type: "DATE", nullable: true),
                    LocationId = table.Column<int>(type: "INTEGER", nullable: true),
                    CostCenterId = table.Column<int>(type: "INTEGER", nullable: true),
                    ManufacturerId = table.Column<int>(type: "INTEGER", nullable: true),
                    ConditionId = table.Column<int>(type: "INTEGER", nullable: true),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: true),
                    LedgerAccountId = table.Column<int>(type: "INTEGER", nullable: true),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false),
                    MediaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Tag = table.Column<string>(type: "VARCHAR(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventory_Condition_ConditionId",
                        column: x => x.ConditionId,
                        principalTable: "Condition",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inventory_CostCenter_CostCenterId",
                        column: x => x.CostCenterId,
                        principalTable: "CostCenter",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inventory_Inventory_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Inventory_LedgerAccount_LedgerAccountId",
                        column: x => x.LedgerAccountId,
                        principalTable: "LedgerAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inventory_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inventory_Manufacturer_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturer",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inventory_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Inventory_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Inventory_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Template",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TemplateAttribute",
                columns: table => new
                {
                    TemplateId = table.Column<int>(type: "INTEGER", nullable: false),
                    AttributeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateAttribute", x => new { x.TemplateId, x.AttributeId });
                    table.ForeignKey(
                        name: "FK_TemplateAttribute_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateAttribute_Template_TemplateId",
                        column: x => x.TemplateId,
                        principalTable: "Template",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ascription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InventoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR (64)", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false),
                    MediaId = table.Column<int>(type: "INTEGER", nullable: true),
                    Tag = table.Column<string>(type: "VARCHAR (256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ascription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ascription_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ascription_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "InventoryAttachment",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    MediaId = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryAttachment", x => new { x.InventoryId, x.MediaId });
                    table.ForeignKey(
                        name: "FK_InventoryAttachment_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryAttachment_Media_MediaId",
                        column: x => x.MediaId,
                        principalTable: "Media",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryAttribute",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    AttributeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryAttribute", x => new { x.InventoryId, x.AttributeId });
                    table.ForeignKey(
                        name: "FK_InventoryAttribute_Attribute_AttributeId",
                        column: x => x.AttributeId,
                        principalTable: "Attribute",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryAttribute_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InventoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    COMMENT = table.Column<string>(type: "TEXT", nullable: true),
                    CREATED = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UPDATED = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR (36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryComment_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryJournal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InventoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Action = table.Column<string>(type: "VARCHAR(256)", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryJournal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryJournal_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTag",
                columns: table => new
                {
                    InventoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTag", x => new { x.InventoryId, x.TagId });
                    table.ForeignKey(
                        name: "FK_InventoryTag_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryTag_Tag_TagId",
                        column: x => x.TagId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryJournalParameter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InventoryJournalId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR (256)", nullable: true),
                    OldValue = table.Column<string>(type: "VARCHAR (256)", nullable: true),
                    NewValue = table.Column<string>(type: "VARCHAR (256)", nullable: true),
                    Guid = table.Column<string>(type: "CHAR (36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryJournalParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryJournalParameter_InventoryJournal_InventoryJournalId",
                        column: x => x.InventoryJournalId,
                        principalTable: "InventoryJournal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ascription_InventoryId",
                table: "Ascription",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Ascription_MediaId",
                table: "Ascription",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Guid",
                table: "Attribute",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_MediaId",
                table: "Attribute",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Name",
                table: "Attribute",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Condition_MediaId",
                table: "Condition",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenter_Guid",
                table: "CostCenter",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CostCenter_MediaId",
                table: "CostCenter",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenter_Name",
                table: "CostCenter",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ConditionId",
                table: "Inventory",
                column: "ConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_CostCenterId",
                table: "Inventory",
                column: "CostCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_Guid",
                table: "Inventory",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_LedgerAccountId",
                table: "Inventory",
                column: "LedgerAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_LocationId",
                table: "Inventory",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ManufacturerId",
                table: "Inventory",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_MediaId",
                table: "Inventory",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_Name",
                table: "Inventory",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ParentId",
                table: "Inventory",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_SupplierId",
                table: "Inventory",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_TemplateId",
                table: "Inventory",
                column: "TemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAttachment_MediaId",
                table: "InventoryAttachment",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAttribute_AttributeId",
                table: "InventoryAttribute",
                column: "AttributeId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryComment_InventoryId",
                table: "InventoryComment",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryJournal_InventoryId",
                table: "InventoryJournal",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryJournalParameter_InventoryJournalId",
                table: "InventoryJournalParameter",
                column: "InventoryJournalId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTag_TagId",
                table: "InventoryTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccount_Guid",
                table: "LedgerAccount",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccount_MediaId",
                table: "LedgerAccount",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccount_Name",
                table: "LedgerAccount",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_Guid",
                table: "Location",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Location_MediaId",
                table: "Location",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Location_Name",
                table: "Location",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturer_Guid",
                table: "Manufacturer",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturer_MediaId",
                table: "Manufacturer",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturer_Name",
                table: "Manufacturer",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Media_Guid",
                table: "Media",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_Guid",
                table: "Supplier",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_MediaId",
                table: "Supplier",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_Name",
                table: "Supplier",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Template_Guid",
                table: "Template",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Template_MediaId",
                table: "Template",
                column: "MediaId");

            migrationBuilder.CreateIndex(
                name: "IX_Template_Name",
                table: "Template",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TemplateAttribute_AttributeId",
                table: "TemplateAttribute",
                column: "AttributeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ascription");

            migrationBuilder.DropTable(
                name: "InventoryAttachment");

            migrationBuilder.DropTable(
                name: "InventoryAttribute");

            migrationBuilder.DropTable(
                name: "InventoryComment");

            migrationBuilder.DropTable(
                name: "InventoryJournalParameter");

            migrationBuilder.DropTable(
                name: "InventoryTag");

            migrationBuilder.DropTable(
                name: "Setting");

            migrationBuilder.DropTable(
                name: "TemplateAttribute");

            migrationBuilder.DropTable(
                name: "InventoryJournal");

            migrationBuilder.DropTable(
                name: "Tag");

            migrationBuilder.DropTable(
                name: "Attribute");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "Condition");

            migrationBuilder.DropTable(
                name: "CostCenter");

            migrationBuilder.DropTable(
                name: "LedgerAccount");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Manufacturer");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Template");

            migrationBuilder.DropTable(
                name: "Media");
        }
    }
}
