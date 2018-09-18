using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentProvidersWebAPI.Migrations
{
    public partial class CreatePaymentProvidersDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CategoryCode = table.Column<string>(nullable: true),
                    NameRu = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commissions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsAbsolute = table.Column<bool>(nullable: false),
                    CommissionValue = table.Column<double>(nullable: false),
                    MinSum = table.Column<double>(nullable: false),
                    MaxSum = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentInfos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentProviders",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProviderCode = table.Column<string>(nullable: true),
                    NameRu = table.Column<string>(nullable: true),
                    PaymentInfoId = table.Column<long>(nullable: true),
                    CatalogCode = table.Column<string>(nullable: true),
                    CategoryId = table.Column<long>(nullable: true),
                    Deleted = table.Column<bool>(nullable: false),
                    Mrlist = table.Column<bool>(nullable: false),
                    MultiCheck = table.Column<bool>(nullable: false),
                    NoSavePt = table.Column<bool>(nullable: false),
                    Check = table.Column<bool>(nullable: false),
                    IsSupportRequestRSTEP = table.Column<bool>(nullable: false),
                    Order = table.Column<long>(nullable: false),
                    ChequeName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentProviders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentProviders_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentProviders_PaymentInfos_PaymentInfoId",
                        column: x => x.PaymentInfoId,
                        principalTable: "PaymentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductPaymentInfos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductType = table.Column<int>(nullable: false),
                    CommissionId = table.Column<long>(nullable: true),
                    MinSum = table.Column<double>(nullable: false),
                    MaxSum = table.Column<double>(nullable: false),
                    PaymentInfoId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductPaymentInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductPaymentInfos_Commissions_CommissionId",
                        column: x => x.CommissionId,
                        principalTable: "Commissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductPaymentInfos_PaymentInfos_PaymentInfoId",
                        column: x => x.PaymentInfoId,
                        principalTable: "PaymentInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PaymentRegions",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PaymentProviderId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentRegions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentRegions_PaymentProviders_PaymentProviderId",
                        column: x => x.PaymentProviderId,
                        principalTable: "PaymentProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProviderFields",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<int>(nullable: false),
                    InterfaceType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    Direction = table.Column<int>(nullable: false),
                    DontShow = table.Column<bool>(nullable: false),
                    Mask = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    MinLength = table.Column<int>(nullable: true),
                    MaxLength = table.Column<int>(nullable: true),
                    RegExp = table.Column<string>(nullable: true),
                    DontTicket = table.Column<bool>(nullable: false),
                    PaymentProviderId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderFields", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProviderFields_PaymentProviders_PaymentProviderId",
                        column: x => x.PaymentProviderId,
                        principalTable: "PaymentProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProviderMaskListItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(nullable: true),
                    Desc = table.Column<string>(nullable: true),
                    ProviderFieldId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderMaskListItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProviderMaskListItems_ProviderFields_ProviderFieldId",
                        column: x => x.ProviderFieldId,
                        principalTable: "ProviderFields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviders_CategoryId",
                table: "PaymentProviders",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentProviders_PaymentInfoId",
                table: "PaymentProviders",
                column: "PaymentInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentRegions_PaymentProviderId",
                table: "PaymentRegions",
                column: "PaymentProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPaymentInfos_CommissionId",
                table: "ProductPaymentInfos",
                column: "CommissionId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductPaymentInfos_PaymentInfoId",
                table: "ProductPaymentInfos",
                column: "PaymentInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderFields_PaymentProviderId",
                table: "ProviderFields",
                column: "PaymentProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_ProviderMaskListItems_ProviderFieldId",
                table: "ProviderMaskListItems",
                column: "ProviderFieldId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentRegions");

            migrationBuilder.DropTable(
                name: "ProductPaymentInfos");

            migrationBuilder.DropTable(
                name: "ProviderMaskListItems");

            migrationBuilder.DropTable(
                name: "Commissions");

            migrationBuilder.DropTable(
                name: "ProviderFields");

            migrationBuilder.DropTable(
                name: "PaymentProviders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "PaymentInfos");
        }
    }
}
