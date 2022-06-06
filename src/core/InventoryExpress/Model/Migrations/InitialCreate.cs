using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryExpress.Model.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Media",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
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
                    table.PrimaryKey("PK_Media", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Currency = table.Column<string>(type: "VARCHAR(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Label = table.Column<string>(type: "VARCHAR(64)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Attribute",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false),
                    MediaID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attribute", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Attribute_Media_MediaID",
                        column: x => x.MediaID,
                        principalTable: "Media",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Condition",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Grade = table.Column<int>(type: "INTEGER (1)", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR (64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR (36)", nullable: false),
                    MediaID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condition", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Condition_Media_MediaID",
                        column: x => x.MediaID,
                        principalTable: "Media",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "CostCenter",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false),
                    MediaID = table.Column<int>(type: "INTEGER", nullable: true),
                    Tag = table.Column<string>(type: "VARCHAR(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenter", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CostCenter_Media_MediaID",
                        column: x => x.MediaID,
                        principalTable: "Media",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "LedgerAccount",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false),
                    MediaID = table.Column<int>(type: "INTEGER", nullable: true),
                    Tag = table.Column<string>(type: "VARCHAR(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LedgerAccount", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LedgerAccount_Media_MediaID",
                        column: x => x.MediaID,
                        principalTable: "Media",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Building = table.Column<string>(type: "VARCHAR (64)", nullable: true),
                    Room = table.Column<string>(type: "VARCHAR (64)", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR (64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR (36)", nullable: false),
                    MediaID = table.Column<int>(type: "INTEGER", nullable: true),
                    Tag = table.Column<string>(type: "VARCHAR (256)", nullable: true),
                    Address = table.Column<string>(type: "VARCHAR (256)", nullable: true),
                    Zip = table.Column<string>(type: "VARCHAR (10)", nullable: true),
                    Place = table.Column<string>(type: "VARCHAR (64)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Location_Media_MediaID",
                        column: x => x.MediaID,
                        principalTable: "Media",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false),
                    MediaID = table.Column<int>(type: "INTEGER", nullable: true),
                    Tag = table.Column<string>(type: "VARCHAR(256)", nullable: true),
                    Address = table.Column<string>(type: "VARCHAR(256)", nullable: true),
                    Zip = table.Column<string>(type: "VARCHAR(10)", nullable: true),
                    Place = table.Column<string>(type: "VARCHAR(64)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Manufacturer_Media_MediaID",
                        column: x => x.MediaID,
                        principalTable: "Media",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR (64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR (36)", nullable: false),
                    MediaID = table.Column<int>(type: "INTEGER", nullable: true),
                    Tag = table.Column<string>(type: "VARCHAR (256)", nullable: true),
                    Address = table.Column<string>(type: "VARCHAR (256)", nullable: true),
                    Zip = table.Column<string>(type: "VARCHAR (10)", nullable: true),
                    Place = table.Column<string>(type: "VARCHAR (64)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Supplier_Media_MediaID",
                        column: x => x.MediaID,
                        principalTable: "Media",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Template",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false),
                    MediaID = table.Column<int>(type: "INTEGER", nullable: true),
                    Tag = table.Column<string>(type: "VARCHAR(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Template", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Template_Media_MediaID",
                        column: x => x.MediaID,
                        principalTable: "Media",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CostValue = table.Column<decimal>(type: "DECIMAL", nullable: false),
                    TemplateID = table.Column<int>(type: "INTEGER", nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "DATE", nullable: true),
                    DerecognitionDate = table.Column<DateTime>(type: "DATE", nullable: true),
                    LocationID = table.Column<int>(type: "INTEGER", nullable: true),
                    CostCenterID = table.Column<int>(type: "INTEGER", nullable: true),
                    ManufacturerID = table.Column<int>(type: "INTEGER", nullable: true),
                    ConditionID = table.Column<int>(type: "INTEGER", nullable: true),
                    SupplierID = table.Column<int>(type: "INTEGER", nullable: true),
                    LedgerAccountID = table.Column<int>(type: "INTEGER", nullable: true),
                    ParentId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR(64)", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false),
                    MediaID = table.Column<int>(type: "INTEGER", nullable: true),
                    Tag = table.Column<string>(type: "VARCHAR(256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Inventory_Condition_ConditionID",
                        column: x => x.ConditionID,
                        principalTable: "Condition",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Inventory_CostCenter_CostCenterID",
                        column: x => x.CostCenterID,
                        principalTable: "CostCenter",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Inventory_Inventory_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Inventory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Inventory_LedgerAccount_LedgerAccountID",
                        column: x => x.LedgerAccountID,
                        principalTable: "LedgerAccount",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Inventory_Location_LocationID",
                        column: x => x.LocationID,
                        principalTable: "Location",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Inventory_Manufacturer_ManufacturerID",
                        column: x => x.ManufacturerID,
                        principalTable: "Manufacturer",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Inventory_Media_MediaID",
                        column: x => x.MediaID,
                        principalTable: "Media",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Inventory_Supplier_SupplierID",
                        column: x => x.SupplierID,
                        principalTable: "Supplier",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Inventory_Template_TemplateID",
                        column: x => x.TemplateID,
                        principalTable: "Template",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TemplateAttribute",
                columns: table => new
                {
                    TemplateID = table.Column<int>(type: "INTEGER", nullable: false),
                    AttributeID = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateAttribute", x => new { x.TemplateID, x.AttributeID });
                    table.ForeignKey(
                        name: "FK_TemplateAttribute_Attribute_AttributeID",
                        column: x => x.AttributeID,
                        principalTable: "Attribute",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TemplateAttribute_Template_TemplateID",
                        column: x => x.TemplateID,
                        principalTable: "Template",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ascription",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InventoryId = table.Column<int>(type: "INTEGER", nullable: true),
                    Name = table.Column<string>(type: "VARCHAR (64)", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false),
                    MediaID = table.Column<int>(type: "INTEGER", nullable: true),
                    Tag = table.Column<string>(type: "VARCHAR (256)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ascription", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ascription_Inventory_InventoryId",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Ascription_Media_MediaID",
                        column: x => x.MediaID,
                        principalTable: "Media",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "InventoryAttachment",
                columns: table => new
                {
                    InventoryID = table.Column<int>(type: "INTEGER", nullable: false),
                    MediaID = table.Column<int>(type: "INTEGER", nullable: false),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryAttachment", x => new { x.InventoryID, x.MediaID });
                    table.ForeignKey(
                        name: "FK_InventoryAttachment_Inventory_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryAttachment_Media_MediaID",
                        column: x => x.MediaID,
                        principalTable: "Media",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryAttribute",
                columns: table => new
                {
                    InventoryID = table.Column<int>(type: "INTEGER", nullable: false),
                    AttributeID = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryAttribute", x => new { x.InventoryID, x.AttributeID });
                    table.ForeignKey(
                        name: "FK_InventoryAttribute_Attribute_AttributeID",
                        column: x => x.AttributeID,
                        principalTable: "Attribute",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryAttribute_Inventory_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryComment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InventoryID = table.Column<int>(type: "INTEGER", nullable: false),
                    COMMENT = table.Column<string>(type: "TEXT", nullable: true),
                    CREATED = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    UPDATED = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    GUID = table.Column<string>(type: "CHAR (36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryComment_Inventory_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryJournal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InventoryID = table.Column<int>(type: "INTEGER", nullable: false),
                    Action = table.Column<string>(type: "VARCHAR(256)", nullable: true),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Guid = table.Column<string>(type: "CHAR(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryJournal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryJournal_Inventory_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTag",
                columns: table => new
                {
                    InventoryID = table.Column<int>(type: "INTEGER", nullable: false),
                    TagID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTag", x => new { x.InventoryID, x.TagID });
                    table.ForeignKey(
                        name: "FK_InventoryTag_Inventory_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventory",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryTag_Tag_TagID",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryJournalParameter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    InventoryJournalID = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR (256)", nullable: true),
                    OldValue = table.Column<string>(type: "VARCHAR (256)", nullable: true),
                    NewValue = table.Column<string>(type: "VARCHAR (256)", nullable: true),
                    Guid = table.Column<string>(type: "CHAR (36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryJournalParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InventoryJournalParameter_InventoryJournal_InventoryJournalID",
                        column: x => x.InventoryJournalID,
                        principalTable: "InventoryJournal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ascription_InventoryId",
                table: "Ascription",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Ascription_MediaID",
                table: "Ascription",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Guid",
                table: "Attribute",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_MediaID",
                table: "Attribute",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_Attribute_Name",
                table: "Attribute",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Condition_MediaID",
                table: "Condition",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenter_Guid",
                table: "CostCenter",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CostCenter_MediaID",
                table: "CostCenter",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_CostCenter_Name",
                table: "CostCenter",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ConditionID",
                table: "Inventory",
                column: "ConditionID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_CostCenterID",
                table: "Inventory",
                column: "CostCenterID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_Guid",
                table: "Inventory",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_LedgerAccountID",
                table: "Inventory",
                column: "LedgerAccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_LocationID",
                table: "Inventory",
                column: "LocationID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_ManufacturerID",
                table: "Inventory",
                column: "ManufacturerID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_MediaID",
                table: "Inventory",
                column: "MediaID");

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
                name: "IX_Inventory_SupplierID",
                table: "Inventory",
                column: "SupplierID");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_TemplateID",
                table: "Inventory",
                column: "TemplateID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAttachment_MediaID",
                table: "InventoryAttachment",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryAttribute_AttributeID",
                table: "InventoryAttribute",
                column: "AttributeID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryComment_InventoryID",
                table: "InventoryComment",
                column: "InventoryID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryJournal_InventoryID",
                table: "InventoryJournal",
                column: "InventoryID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryJournalParameter_InventoryJournalID",
                table: "InventoryJournalParameter",
                column: "InventoryJournalID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTag_TagID",
                table: "InventoryTag",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccount_Guid",
                table: "LedgerAccount",
                column: "Guid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LedgerAccount_MediaID",
                table: "LedgerAccount",
                column: "MediaID");

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
                name: "IX_Location_MediaID",
                table: "Location",
                column: "MediaID");

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
                name: "IX_Manufacturer_MediaID",
                table: "Manufacturer",
                column: "MediaID");

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
                name: "IX_Supplier_MediaID",
                table: "Supplier",
                column: "MediaID");

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
                name: "IX_Template_MediaID",
                table: "Template",
                column: "MediaID");

            migrationBuilder.CreateIndex(
                name: "IX_Template_Name",
                table: "Template",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TemplateAttribute_AttributeID",
                table: "TemplateAttribute",
                column: "AttributeID");
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
