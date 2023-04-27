using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net_il_mio_fotoalbum.Migrations
{
    /// <inheritdoc />
    public partial class MessagePhotoId2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_messages_photo_id",
                table: "messages",
                column: "photo_id");

            migrationBuilder.AddForeignKey(
                name: "FK_messages_photos_photo_id",
                table: "messages",
                column: "photo_id",
                principalTable: "photos",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_messages_photos_photo_id",
                table: "messages");

            migrationBuilder.DropIndex(
                name: "IX_messages_photo_id",
                table: "messages");
        }
    }
}
