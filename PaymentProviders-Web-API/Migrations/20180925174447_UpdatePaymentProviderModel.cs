using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentProvidersWebAPI.Migrations
{
    public partial class UpdatePaymentProviderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegionString",
                table: "PaymentProvider",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegionString",
                table: "PaymentProvider");
        }
    }
}
