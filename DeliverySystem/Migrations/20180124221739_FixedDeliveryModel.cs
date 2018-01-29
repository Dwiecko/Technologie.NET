using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DeliverySystem1.Migrations
{
    public partial class FixedDeliveryModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_AspNetUsers_ApplicationUserID",
                table: "Delivery");

            migrationBuilder.DropIndex(
                name: "IX_Delivery_ApplicationUserID",
                table: "Delivery");

            migrationBuilder.DropColumn(
                name: "ApplicationUserID",
                table: "Delivery");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserID",
                table: "Delivery",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_ApplicationUserID",
                table: "Delivery",
                column: "ApplicationUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_AspNetUsers_ApplicationUserID",
                table: "Delivery",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
