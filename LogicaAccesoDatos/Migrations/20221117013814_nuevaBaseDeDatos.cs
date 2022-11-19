using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LogicaAccesoDatos.Migrations
{
    public partial class nuevaBaseDeDatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Grupos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grupos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Horarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hora = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regiones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regiones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Paises",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(maxLength: 100, nullable: false),
                    Codigo = table.Column<string>(maxLength: 3, nullable: false),
                    Pbi = table.Column<int>(nullable: false),
                    Poblacion = table.Column<int>(nullable: false),
                    Bandera = table.Column<string>(nullable: true),
                    RegionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Paises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Paises_Regiones_RegionId",
                        column: x => x.RegionId,
                        principalTable: "Regiones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Selecciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaisId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    CantPotencialApostadores = table.Column<int>(nullable: false),
                    Puntos = table.Column<int>(nullable: false),
                    GrupoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Selecciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Selecciones_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Selecciones_Paises_PaisId",
                        column: x => x.PaisId,
                        principalTable: "Paises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Partidos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Seleccion1Id = table.Column<int>(nullable: true),
                    Seleccion2Id = table.Column<int>(nullable: true),
                    Fecha = table.Column<DateTime>(nullable: false),
                    HoraId = table.Column<int>(nullable: true),
                    Estado = table.Column<string>(nullable: true),
                    CantidadRojasS1 = table.Column<int>(nullable: false),
                    CantidadRojasS2 = table.Column<int>(nullable: false),
                    CantidadAmarillasS1 = table.Column<int>(nullable: false),
                    CantidadAmarillasS2 = table.Column<int>(nullable: false),
                    CantidadRojasAcAmarillasS1 = table.Column<int>(nullable: false),
                    CantidadRojasAcAmarillasS2 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partidos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Partidos_Horarios_HoraId",
                        column: x => x.HoraId,
                        principalTable: "Horarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partidos_Selecciones_Seleccion1Id",
                        column: x => x.Seleccion1Id,
                        principalTable: "Selecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Partidos_Selecciones_Seleccion2Id",
                        column: x => x.Seleccion2Id,
                        principalTable: "Selecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PartidoGrupo",
                columns: table => new
                {
                    PartidoId = table.Column<int>(nullable: false),
                    GrupoId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PartidoGrupo", x => new { x.PartidoId, x.GrupoId });
                    table.ForeignKey(
                        name: "FK_PartidoGrupo_Grupos_GrupoId",
                        column: x => x.GrupoId,
                        principalTable: "Grupos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PartidoGrupo_Partidos_PartidoId",
                        column: x => x.PartidoId,
                        principalTable: "Partidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SeleccionPartido",
                columns: table => new
                {
                    PartidoId = table.Column<int>(nullable: false),
                    SeleccionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeleccionPartido", x => new { x.PartidoId, x.SeleccionId });
                    table.ForeignKey(
                        name: "FK_SeleccionPartido_Partidos_PartidoId",
                        column: x => x.PartidoId,
                        principalTable: "Partidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeleccionPartido_Selecciones_SeleccionId",
                        column: x => x.SeleccionId,
                        principalTable: "Selecciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Grupos_Nombre",
                table: "Grupos",
                column: "Nombre",
                unique: true,
                filter: "[Nombre] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Paises_Codigo",
                table: "Paises",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paises_Nombre",
                table: "Paises",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Paises_RegionId",
                table: "Paises",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_PartidoGrupo_GrupoId",
                table: "PartidoGrupo",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_HoraId",
                table: "Partidos",
                column: "HoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_Seleccion1Id",
                table: "Partidos",
                column: "Seleccion1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Partidos_Seleccion2Id",
                table: "Partidos",
                column: "Seleccion2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Selecciones_Email",
                table: "Selecciones",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Selecciones_GrupoId",
                table: "Selecciones",
                column: "GrupoId");

            migrationBuilder.CreateIndex(
                name: "IX_Selecciones_PaisId",
                table: "Selecciones",
                column: "PaisId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Selecciones_Telefono",
                table: "Selecciones",
                column: "Telefono",
                unique: true,
                filter: "[Telefono] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SeleccionPartido_SeleccionId",
                table: "SeleccionPartido",
                column: "SeleccionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PartidoGrupo");

            migrationBuilder.DropTable(
                name: "SeleccionPartido");

            migrationBuilder.DropTable(
                name: "Partidos");

            migrationBuilder.DropTable(
                name: "Horarios");

            migrationBuilder.DropTable(
                name: "Selecciones");

            migrationBuilder.DropTable(
                name: "Grupos");

            migrationBuilder.DropTable(
                name: "Paises");

            migrationBuilder.DropTable(
                name: "Regiones");
        }
    }
}
