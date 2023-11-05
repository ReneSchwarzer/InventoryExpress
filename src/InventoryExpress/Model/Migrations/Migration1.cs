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
                    InventoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    AttributeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Updated = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
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
            
            migrationBuilder.Sql(@"INSERT INTO InventoryAttribute (InventoryId,AttributeId,Value,Created) 
                                   SELECT InventoryId,
                                      AttributeId,
                                      Value,
                                      Created
                                 FROM inventoryattribute_temp_table;");
            
            migrationBuilder.Sql("DROP TABLE inventoryattribute_temp_table;");
            migrationBuilder.Sql("CREATE INDEX IX_InventoryAttribute_AttributeId ON InventoryAttribute (\"AttributeId\");");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Updated",
                table: "InventoryAttribute");
        }
    }
}
