using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAcessLayer.Migrations
{
    /// <inheritdoc />
    public partial class BookReservationUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookReservations_ReservationStatuses_ReservationId",
                table: "BookReservations");

            migrationBuilder.DropIndex(
                name: "IX_BookReservations_ReservationId",
                table: "BookReservations");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "BookReservations");

            migrationBuilder.CreateIndex(
                name: "IX_BookReservations_StatusId",
                table: "BookReservations",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookReservations_ReservationStatuses_StatusId",
                table: "BookReservations",
                column: "StatusId",
                principalTable: "ReservationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookReservations_ReservationStatuses_StatusId",
                table: "BookReservations");

            migrationBuilder.DropIndex(
                name: "IX_BookReservations_StatusId",
                table: "BookReservations");

            migrationBuilder.AddColumn<Guid>(
                name: "ReservationId",
                table: "BookReservations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_BookReservations_ReservationId",
                table: "BookReservations",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookReservations_ReservationStatuses_ReservationId",
                table: "BookReservations",
                column: "ReservationId",
                principalTable: "ReservationStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
