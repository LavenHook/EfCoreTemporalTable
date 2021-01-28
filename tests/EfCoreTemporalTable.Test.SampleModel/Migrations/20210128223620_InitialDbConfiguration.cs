using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EfCoreTemporalTable.Test.SampleModel.Migrations
{
    public partial class InitialDbConfiguration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ExampleEntity1",
                columns: table => new
                {
                    ExampleEntityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstMappedProperty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecondMappedProperty = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ThirdMappedProperty = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FourthMappedProperty = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExampleEntity1", x => x.ExampleEntityId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExampleEntity1");
        }
    }
}
