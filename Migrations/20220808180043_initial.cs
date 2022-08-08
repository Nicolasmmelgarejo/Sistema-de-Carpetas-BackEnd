using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "carpeta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCarpeta = table.Column<string>(type: "varchar(100)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carpeta", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(50)", nullable: false),
                    UserPassword = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "archivo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreArchivo = table.Column<string>(type: "varchar(100)", nullable: false),
                    ContenidoArchivo = table.Column<string>(type: "varchar(max)", nullable: false),
                    CarpetaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archivo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_archivo_carpeta_CarpetaId",
                        column: x => x.CarpetaId,
                        principalTable: "carpeta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tablaCarpetas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCarpeta = table.Column<string>(type: "varchar(100)", nullable: false),
                    CarpetaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tablaCarpetas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tablaCarpetas_carpeta_CarpetaId",
                        column: x => x.CarpetaId,
                        principalTable: "carpeta",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRole = table.Column<string>(type: "varchar(50)", nullable: false),
                    UserIdUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_user_Roles_user_UserIdUser",
                        column: x => x.UserIdUser,
                        principalTable: "user",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_archivo_CarpetaId",
                table: "archivo",
                column: "CarpetaId");

            migrationBuilder.CreateIndex(
                name: "IX_tablaCarpetas_CarpetaId",
                table: "tablaCarpetas",
                column: "CarpetaId");

            migrationBuilder.CreateIndex(
                name: "IX_user_Roles_UserIdUser",
                table: "user_Roles",
                column: "UserIdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "archivo");

            migrationBuilder.DropTable(
                name: "tablaCarpetas");

            migrationBuilder.DropTable(
                name: "user_Roles");

            migrationBuilder.DropTable(
                name: "carpeta");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
