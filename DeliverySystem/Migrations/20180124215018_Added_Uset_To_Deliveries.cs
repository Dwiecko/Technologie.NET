using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DeliverySystem1.Migrations
{
    public partial class Added_Uset_To_Deliveries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_AspNetUsers_ApplicationUserId",
                table: "Delivery");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Delivery",
                newName: "ApplicationUserID");

            migrationBuilder.RenameIndex(
                name: "IX_Delivery_ApplicationUserId",
                table: "Delivery",
                newName: "IX_Delivery_ApplicationUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_AspNetUsers_ApplicationUserID",
                table: "Delivery",
                column: "ApplicationUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_AspNetUsers_ApplicationUserID",
                table: "Delivery");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserID",
                table: "Delivery",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Delivery_ApplicationUserID",
                table: "Delivery",
                newName: "IX_Delivery_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_AspNetUsers_ApplicationUserId",
                table: "Delivery",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
