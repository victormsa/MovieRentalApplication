using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RentalWorkPlease.Migrations
{
    public partial class addedRental : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rental",
                columns: table => new
                {
                    RentalID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cpf = table.Column<int>(nullable: false),
                    RentalDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rental", x => x.RentalID);
                });

            migrationBuilder.CreateTable(
                name: "MovieAssign",
                columns: table => new
                {
                    MovieID = table.Column<int>(nullable: false),
                    RentalID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieAssign", x => new { x.MovieID, x.RentalID });
                    table.ForeignKey(
                        name: "FK_MovieAssign_Movie_MovieID",
                        column: x => x.MovieID,
                        principalTable: "Movie",
                        principalColumn: "MovieID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieAssign_Rental_RentalID",
                        column: x => x.RentalID,
                        principalTable: "Rental",
                        principalColumn: "RentalID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieAssign_RentalID",
                table: "MovieAssign",
                column: "RentalID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieAssign");

            migrationBuilder.DropTable(
                name: "Rental");
        }
    }
}
