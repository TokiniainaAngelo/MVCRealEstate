using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCRealEstate.Migrations
{
    /// <inheritdoc />
    public partial class relationsCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offer_OwnerInfo_OwnerInfoId",
                table: "Offer");


            migrationBuilder.AlterColumn<int>(
                name: "OwnerInfoId",
                table: "Offer",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OfferMediaId",
                table: "Offer",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "MessagesId",
                table: "Interest",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Message",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SenderType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SenderFullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    InterestRequestId = table.Column<int>(type: "int", nullable: false),
                    InterestId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageId);
                    table.ForeignKey(
                        name: "FK_Message_Interest_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interest",
                        principalColumn: "InterestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Message_InterestId",
                table: "Message",
                column: "InterestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_OwnerInfo_OwnerInfoId",
                table: "Offer",
                column: "OwnerInfoId",
                principalTable: "OwnerInfo",
                principalColumn: "OwnerInfoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Offer_OwnerInfo_OwnerInfoId",
                table: "Offer");

            migrationBuilder.DropTable(
                name: "Message");

            migrationBuilder.DropColumn(
                name: "MessagesId",
                table: "Interest");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerInfoId",
                table: "Offer",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "OfferMediaId",
                table: "Offer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "InterestMessage",
                columns: table => new
                {
                    InterestMessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterestId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InterestRequestId = table.Column<int>(type: "int", nullable: false),
                    SenderFullName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SenderType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestMessage", x => x.InterestMessageId);
                    table.ForeignKey(
                        name: "FK_InterestMessage_Interest_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interest",
                        principalColumn: "InterestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterestMessage_InterestId",
                table: "InterestMessage",
                column: "InterestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Offer_OwnerInfo_OwnerInfoId",
                table: "Offer",
                column: "OwnerInfoId",
                principalTable: "OwnerInfo",
                principalColumn: "OwnerInfoId");
        }
    }
}
