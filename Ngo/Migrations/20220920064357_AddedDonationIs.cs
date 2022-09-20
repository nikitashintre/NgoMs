using Microsoft.EntityFrameworkCore.Migrations;

namespace Ngo.Migrations
{
    public partial class AddedDonationIs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.CreateTable(
                name: "DonationInfos",
                columns: table => new
                {
                    DonationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonarName = table.Column<string>(maxLength: 80, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Mobile = table.Column<string>(nullable: false),
                    DonationAmount = table.Column<int>(nullable: false),
                    CampaignId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationInfos", x => x.DonationId);
                    table.ForeignKey(
                        name: "FK_DonationInfos_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "CamaignId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonationInfos_CampaignId",
                table: "DonationInfos",
                column: "CampaignId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DonationInfos");

            migrationBuilder.CreateTable(
                name: "DonationInfo",
                columns: table => new
                {
                    DonationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CampaignId = table.Column<int>(type: "int", nullable: false),
                    DonarName = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DonationAmount = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonationInfo", x => x.DonationId);
                    table.ForeignKey(
                        name: "FK_DonationInfo_Campaigns_CampaignId",
                        column: x => x.CampaignId,
                        principalTable: "Campaigns",
                        principalColumn: "CamaignId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DonationInfo_CampaignId",
                table: "DonationInfo",
                column: "CampaignId");
        }
    }
}
