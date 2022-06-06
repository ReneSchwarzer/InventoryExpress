using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryExpress.Model.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE TABLE inventoryattribute_temp_table AS SELECT * FROM InventoryAttribute;");
            migrationBuilder.Sql("DROP TABLE InventoryAttribute;");

            migrationBuilder.CreateTable(
                name: "InventoryAttribute",
                columns: table => new
                {
                    InventoryID = table.Column<int>(type: "INTEGER", nullable: false),
                    AttributeID = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
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
            
            migrationBuilder.Sql(@"INSERT INTO InventoryAttribute (InventoryID,AttributeID,Value,Created) 
                                   SELECT InventoryID,
                                      AttributeID,
                                      Value,
                                      Created
                                 FROM inventoryattribute_temp_table;");
            
            migrationBuilder.Sql("DROP TABLE inventoryattribute_temp_table;");
            migrationBuilder.Sql("CREATE INDEX IX_InventoryAttribute_AttributeID ON InventoryAttribute (\"AttributeID\");");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Updated",
                table: "InventoryAttribute");
        }
    }
}
