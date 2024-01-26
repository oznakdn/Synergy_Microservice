using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Synergy.TeamService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDeveloperSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DevelopersSkills_Developers_DeveloperId",
                table: "DevelopersSkills");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeveloperId",
                table: "DevelopersSkills",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DevelopersSkills_Developers_DeveloperId",
                table: "DevelopersSkills",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DevelopersSkills_Developers_DeveloperId",
                table: "DevelopersSkills");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeveloperId",
                table: "DevelopersSkills",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_DevelopersSkills_Developers_DeveloperId",
                table: "DevelopersSkills",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id");
        }
    }
}
