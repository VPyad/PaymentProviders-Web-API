using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentProvidersWebAPI.Migrations
{
    public partial class UpdateModelPaymentProviderContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProviders_Categories_CategoryId",
                table: "PaymentProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProviders_PaymentInfos_PaymentInfoId",
                table: "PaymentProviders");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentRegions_PaymentProviders_PaymentProviderId",
                table: "PaymentRegions");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPaymentInfos_Commissions_CommissionId",
                table: "ProductPaymentInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPaymentInfos_PaymentInfos_PaymentInfoId",
                table: "ProductPaymentInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderFields_PaymentProviders_PaymentProviderId",
                table: "ProviderFields");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderMaskListItems_ProviderFields_ProviderFieldId",
                table: "ProviderMaskListItems");

            migrationBuilder.DropIndex(
                name: "IX_PaymentRegions_PaymentProviderId",
                table: "PaymentRegions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProviderFields",
                table: "ProviderFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductPaymentInfos",
                table: "ProductPaymentInfos");

            migrationBuilder.DropIndex(
                name: "IX_ProductPaymentInfos_CommissionId",
                table: "ProductPaymentInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentProviders",
                table: "PaymentProviders");

            migrationBuilder.DropIndex(
                name: "IX_PaymentProviders_PaymentInfoId",
                table: "PaymentProviders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentInfos",
                table: "PaymentInfos");

            migrationBuilder.DropColumn(
                name: "PaymentProviderId",
                table: "PaymentRegions");

            migrationBuilder.DropColumn(
                name: "CommissionId",
                table: "ProductPaymentInfos");

            migrationBuilder.RenameTable(
                name: "ProviderFields",
                newName: "ProviderField");

            migrationBuilder.RenameTable(
                name: "ProductPaymentInfos",
                newName: "ProductPaymentInfo");

            migrationBuilder.RenameTable(
                name: "PaymentProviders",
                newName: "PaymentProvider");

            migrationBuilder.RenameTable(
                name: "PaymentInfos",
                newName: "PaymentInfo");

            migrationBuilder.RenameIndex(
                name: "IX_ProviderFields_PaymentProviderId",
                table: "ProviderField",
                newName: "IX_ProviderField_PaymentProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPaymentInfos_PaymentInfoId",
                table: "ProductPaymentInfo",
                newName: "IX_ProductPaymentInfo_PaymentInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentProviders_CategoryId",
                table: "PaymentProvider",
                newName: "IX_PaymentProvider_CategoryId");

            migrationBuilder.AlterColumn<long>(
                name: "ProviderFieldId",
                table: "ProviderMaskListItems",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProductPaymentInfoRef",
                table: "Commissions",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ProviderField",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "InterfaceType",
                table: "ProviderField",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "ProductType",
                table: "ProductPaymentInfo",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<long>(
                name: "PaymentProviderRef",
                table: "PaymentInfo",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProviderField",
                table: "ProviderField",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductPaymentInfo",
                table: "ProductPaymentInfo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentProvider",
                table: "PaymentProvider",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentInfo",
                table: "PaymentInfo",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PaymentProviderRegion",
                columns: table => new
                {
                    PaymentProviderId = table.Column<long>(nullable: false),
                    PaymentRegionId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProviderRegion", x => new { x.PaymentProviderId, x.PaymentRegionId });
                    table.ForeignKey(
                        name: "FK_PaymentProviderRegion_PaymentProvider_PaymentProviderId",
                        column: x => x.PaymentProviderId,
                        principalTable: "PaymentProvider",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PaymentProviderRegion_PaymentRegions_PaymentRegionId",
                        column: x => x.PaymentRegionId,
                        principalTable: "PaymentRegions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Commissions_ProductPaymentInfoRef",
                table: "Commissions",
                column: "ProductPaymentInfoRef",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentInfo_PaymentProviderRef",
                table: "PaymentInfo",
                column: "PaymentProviderRef",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviderRegion_PaymentRegionId",
                table: "PaymentProviderRegion",
                column: "PaymentRegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commissions_ProductPaymentInfo_ProductPaymentInfoRef",
                table: "Commissions",
                column: "ProductPaymentInfoRef",
                principalTable: "ProductPaymentInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentInfo_PaymentProvider_PaymentProviderRef",
                table: "PaymentInfo",
                column: "PaymentProviderRef",
                principalTable: "PaymentProvider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProvider_Categories_CategoryId",
                table: "PaymentProvider",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPaymentInfo_PaymentInfo_PaymentInfoId",
                table: "ProductPaymentInfo",
                column: "PaymentInfoId",
                principalTable: "PaymentInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderField_PaymentProvider_PaymentProviderId",
                table: "ProviderField",
                column: "PaymentProviderId",
                principalTable: "PaymentProvider",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderMaskListItems_ProviderField_ProviderFieldId",
                table: "ProviderMaskListItems",
                column: "ProviderFieldId",
                principalTable: "ProviderField",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commissions_ProductPaymentInfo_ProductPaymentInfoRef",
                table: "Commissions");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentInfo_PaymentProvider_PaymentProviderRef",
                table: "PaymentInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentProvider_Categories_CategoryId",
                table: "PaymentProvider");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductPaymentInfo_PaymentInfo_PaymentInfoId",
                table: "ProductPaymentInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderField_PaymentProvider_PaymentProviderId",
                table: "ProviderField");

            migrationBuilder.DropForeignKey(
                name: "FK_ProviderMaskListItems_ProviderField_ProviderFieldId",
                table: "ProviderMaskListItems");

            migrationBuilder.DropTable(
                name: "PaymentProviderRegion");

            migrationBuilder.DropIndex(
                name: "IX_Commissions_ProductPaymentInfoRef",
                table: "Commissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProviderField",
                table: "ProviderField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductPaymentInfo",
                table: "ProductPaymentInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentProvider",
                table: "PaymentProvider");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentInfo",
                table: "PaymentInfo");

            migrationBuilder.DropIndex(
                name: "IX_PaymentInfo_PaymentProviderRef",
                table: "PaymentInfo");

            migrationBuilder.DropColumn(
                name: "ProductPaymentInfoRef",
                table: "Commissions");

            migrationBuilder.DropColumn(
                name: "PaymentProviderRef",
                table: "PaymentInfo");

            migrationBuilder.RenameTable(
                name: "ProviderField",
                newName: "ProviderFields");

            migrationBuilder.RenameTable(
                name: "ProductPaymentInfo",
                newName: "ProductPaymentInfos");

            migrationBuilder.RenameTable(
                name: "PaymentProvider",
                newName: "PaymentProviders");

            migrationBuilder.RenameTable(
                name: "PaymentInfo",
                newName: "PaymentInfos");

            migrationBuilder.RenameIndex(
                name: "IX_ProviderField_PaymentProviderId",
                table: "ProviderFields",
                newName: "IX_ProviderFields_PaymentProviderId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductPaymentInfo_PaymentInfoId",
                table: "ProductPaymentInfos",
                newName: "IX_ProductPaymentInfos_PaymentInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentProvider_CategoryId",
                table: "PaymentProviders",
                newName: "IX_PaymentProviders_CategoryId");

            migrationBuilder.AlterColumn<long>(
                name: "ProviderFieldId",
                table: "ProviderMaskListItems",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddColumn<long>(
                name: "PaymentProviderId",
                table: "PaymentRegions",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Type",
                table: "ProviderFields",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "InterfaceType",
                table: "ProviderFields",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "ProductType",
                table: "ProductPaymentInfos",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<long>(
                name: "CommissionId",
                table: "ProductPaymentInfos",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProviderFields",
                table: "ProviderFields",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductPaymentInfos",
                table: "ProductPaymentInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentProviders",
                table: "PaymentProviders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentInfos",
                table: "PaymentInfos",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRegions_PaymentProviderId",
                table: "PaymentRegions",
                column: "PaymentProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPaymentInfos_CommissionId",
                table: "ProductPaymentInfos",
                column: "CommissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviders_PaymentInfoId",
                table: "PaymentProviders",
                column: "PaymentInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProviders_Categories_CategoryId",
                table: "PaymentProviders",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentProviders_PaymentInfos_PaymentInfoId",
                table: "PaymentProviders",
                column: "PaymentInfoId",
                principalTable: "PaymentInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentRegions_PaymentProviders_PaymentProviderId",
                table: "PaymentRegions",
                column: "PaymentProviderId",
                principalTable: "PaymentProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPaymentInfos_Commissions_CommissionId",
                table: "ProductPaymentInfos",
                column: "CommissionId",
                principalTable: "Commissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPaymentInfos_PaymentInfos_PaymentInfoId",
                table: "ProductPaymentInfos",
                column: "PaymentInfoId",
                principalTable: "PaymentInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderFields_PaymentProviders_PaymentProviderId",
                table: "ProviderFields",
                column: "PaymentProviderId",
                principalTable: "PaymentProviders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProviderMaskListItems_ProviderFields_ProviderFieldId",
                table: "ProviderMaskListItems",
                column: "ProviderFieldId",
                principalTable: "ProviderFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
