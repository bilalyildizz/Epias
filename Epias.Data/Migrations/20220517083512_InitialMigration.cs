using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Epias.Data.Migrations;

public partial class InitialMigration : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "TradeHistories",
            columns: table => new
            {
                Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                Conract = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Price = table.Column<double>(type: "float", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TradeHistories", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "TradeHistories");
    }
}

