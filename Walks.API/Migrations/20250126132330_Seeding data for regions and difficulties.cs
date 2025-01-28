using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Walks.API.Migrations
{
    /// <inheritdoc />
    public partial class Seedingdataforregionsanddifficulties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulty",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("e6c508b4-e944-46e0-9c52-1a0ec86374aa"), "Medium" },
                    { new Guid("e6c508b4-e944-46e0-9c52-1a0ec86374ab"), "Hard" },
                    { new Guid("e6c508b4-e944-46e0-9c52-1a0ec86374ad"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Name", "RegionImageUrl", "code" },
                values: new object[,]
                {
                    { new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"), "Missouri", "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1", "MS" },
                    { new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f7"), "Michigan", "https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1", "MI" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: new Guid("e6c508b4-e944-46e0-9c52-1a0ec86374aa"));

            migrationBuilder.DeleteData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: new Guid("e6c508b4-e944-46e0-9c52-1a0ec86374ab"));

            migrationBuilder.DeleteData(
                table: "Difficulty",
                keyColumn: "Id",
                keyValue: new Guid("e6c508b4-e944-46e0-9c52-1a0ec86374ad"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("f7248fc3-2585-4efb-8d1d-1c555f4087f7"));
        }
    }
}
