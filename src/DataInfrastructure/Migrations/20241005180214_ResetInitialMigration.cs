using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class ResetInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "room",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    building_prefix = table.Column<string>(type: "text", nullable: false),
                    floor_number = table.Column<int>(type: "integer", nullable: false),
                    room_number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_room", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reservation",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoomId = table.Column<Guid>(type: "uuid", nullable: false),
                    reservation = table.Column<string>(type: "character varying(21)", maxLength: 21, nullable: false),
                    end = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    start = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    booker_id = table.Column<Guid>(type: "uuid", nullable: true),
                    study_group_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservation", x => x.id);
                    table.ForeignKey(
                        name: "FK_reservation_room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "room",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_reservation_RoomId",
                table: "reservation",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservation");

            migrationBuilder.DropTable(
                name: "room");
        }
    }
}
