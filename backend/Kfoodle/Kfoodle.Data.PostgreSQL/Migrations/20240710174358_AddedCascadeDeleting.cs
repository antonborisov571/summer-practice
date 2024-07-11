using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kfoodle.Data.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddedCascadeDeleting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Choices_SingleChoiceId",
                table: "Answers");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Choices_SingleChoiceId",
                table: "Answers",
                column: "SingleChoiceId",
                principalTable: "Choices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Choices_SingleChoiceId",
                table: "Answers");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Choices_SingleChoiceId",
                table: "Answers",
                column: "SingleChoiceId",
                principalTable: "Choices",
                principalColumn: "Id");
        }
    }
}
