using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CurrencyConverter.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Symbol = table.Column<string>(type: "character(3)", fixedLength: true, maxLength: 3, nullable: false),
                    InsertTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CurrencyRate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstCurrencyId = table.Column<int>(type: "integer", nullable: false),
                    SecondCurrencyId = table.Column<int>(type: "integer", nullable: false),
                    Rate = table.Column<double>(type: "double precision", nullable: false),
                    InsertTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeleteTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrencyRate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrencyRate_Currency_FirstCurrencyId",
                        column: x => x.FirstCurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CurrencyRate_Currency_SecondCurrencyId",
                        column: x => x.SecondCurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRate_FirstCurrencyId",
                table: "CurrencyRate",
                column: "FirstCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_CurrencyRate_SecondCurrencyId",
                table: "CurrencyRate",
                column: "SecondCurrencyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrencyRate");

            migrationBuilder.DropTable(
                name: "Currency");
        }
    }
}
