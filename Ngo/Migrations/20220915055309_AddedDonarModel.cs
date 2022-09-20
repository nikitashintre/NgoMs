using Microsoft.EntityFrameworkCore.Migrations;

namespace Ngo.Migrations
{
    public partial class AddedDonarModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donars",
                columns: table => new
                {
                    DonarId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonarName = table.Column<string>(maxLength: 80, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Mobile = table.Column<string>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donars", x => x.DonarId);
                    table.ForeignKey(
                        name: "FK_Donars_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donars_EventId",
                table: "Donars",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donars");
        }
    }
}
