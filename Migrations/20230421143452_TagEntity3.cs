using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net_il_mio_fotoalbum.Migrations
{
    /// <inheritdoc />
    public partial class TagEntity3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PhotoTag",
                columns: table => new
                {
                    PhotosId = table.Column<int>(type: "int", nullable: false),
                    TagsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhotoTag", x => new { x.PhotosId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_PhotoTag_photos_PhotosId",
                        column: x => x.PhotosId,
                        principalTable: "photos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PhotoTag_tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PhotoTag_TagsId",
                table: "PhotoTag",
                column: "TagsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhotoTag");

            migrationBuilder.DropTable(
                name: "tags");
        }
    }
}
