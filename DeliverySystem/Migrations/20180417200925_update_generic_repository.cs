using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DeliverySystem1.Migrations
{
    public partial class update_generic_repository : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Category_CategoryID",
                table: "Delivery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Delivery",
                table: "Delivery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "DeliveryID",
                table: "Delivery");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Delivery",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Delivery",
                table: "Delivery",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Category_CategoryID",
                table: "Delivery",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Delivery_Category_CategoryID",
                table: "Delivery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Delivery",
                table: "Delivery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Delivery");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Category");

            migrationBuilder.AddColumn<int>(
                name: "DeliveryID",
                table: "Delivery",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Category",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Delivery",
                table: "Delivery",
                column: "DeliveryID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Delivery_Category_CategoryID",
                table: "Delivery",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "CategoryID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
