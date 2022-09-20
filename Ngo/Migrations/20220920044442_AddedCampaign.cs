using Microsoft.EntityFrameworkCore.Migrations;

namespace Ngo.Migrations
{
    public partial class AddedCampaign : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campaigns",
                columns: table => new
                {
                    CamaignId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignName = table.Column<string>(maxLength: 100, nullable: false),
                    Discription = table.Column<string>(maxLength: 1000, nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaigns", x => x.CamaignId);
                    table.ForeignKey(
                        name: "FK_Campaigns_CampaignCategory_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "CampaignCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campaigns_CategoryId",
                table: "Campaigns",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Campaigns");
        }
    }
}
