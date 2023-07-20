using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CoreSharp.Templates.Blazor.Infrastructure.Migrations;

public partial class AddProductsTable : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Products",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                DateCreatedUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                DateModifiedUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
            },
            constraints: table => table.PrimaryKey("PK_Products", x => x.Id));
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Products");
    }
}
