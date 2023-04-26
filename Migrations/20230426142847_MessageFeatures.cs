using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net_il_mio_fotoalbum.Migrations
{
    /// <inheritdoc />
    public partial class MessageFeatures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "surname",
                table: "messages",
                newName: "sender_id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "messages",
                newName: "recipient_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "sender_id",
                table: "messages",
                newName: "surname");

            migrationBuilder.RenameColumn(
                name: "recipient_id",
                table: "messages",
                newName: "name");
        }
    }
}
