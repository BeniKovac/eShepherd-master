using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace web.Migrations
{
    public partial class Fixing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Crede",
                columns: table => new
                {
                    CredeID = table.Column<string>(nullable: false),
                    Opombe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crede", x => x.CredeID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Oven",
                columns: table => new
                {
                    OvenID = table.Column<string>(maxLength: 10, nullable: false),
                    CredaID = table.Column<string>(nullable: true),
                    DatumRojstva = table.Column<DateTime>(nullable: true),
                    Pasma = table.Column<string>(nullable: true),
                    mamaID = table.Column<string>(nullable: true),
                    oceID = table.Column<string>(nullable: true),
                    SteviloSorojencev = table.Column<int>(nullable: true),
                    Stanje = table.Column<string>(nullable: true),
                    Opombe = table.Column<string>(nullable: true),
                    Poreklo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oven", x => x.OvenID);
                    table.ForeignKey(
                        name: "FK_Oven_Crede_CredaID",
                        column: x => x.CredaID,
                        principalTable: "Crede",
                        principalColumn: "CredeID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ovca",
                columns: table => new
                {
                    OvcaID = table.Column<string>(maxLength: 10, nullable: false),
                    CredaID = table.Column<string>(nullable: true),
                    DatumRojstva = table.Column<DateTime>(nullable: true),
                    Pasma = table.Column<string>(nullable: true),
                    mamaID = table.Column<string>(nullable: true),
                    oceID = table.Column<string>(nullable: true),
                    SteviloSorojencev = table.Column<int>(nullable: true),
                    Stanje = table.Column<string>(nullable: true),
                    Opombe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ovca", x => x.OvcaID);
                    table.ForeignKey(
                        name: "FK_Ovca_Crede_CredaID",
                        column: x => x.CredaID,
                        principalTable: "Crede",
                        principalColumn: "CredeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ovca_Ovca_mamaID",
                        column: x => x.mamaID,
                        principalTable: "Ovca",
                        principalColumn: "OvcaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ovca_Oven_oceID",
                        column: x => x.oceID,
                        principalTable: "Oven",
                        principalColumn: "OvenID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gonitev",
                columns: table => new
                {
                    GonitevID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumGonitve = table.Column<DateTime>(nullable: false),
                    OvcaID = table.Column<string>(nullable: true),
                    OvenID = table.Column<string>(nullable: true),
                    Opombe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gonitev", x => x.GonitevID);
                    table.ForeignKey(
                        name: "FK_Gonitev_Ovca_OvcaID",
                        column: x => x.OvcaID,
                        principalTable: "Ovca",
                        principalColumn: "OvcaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Gonitev_Oven_OvenID",
                        column: x => x.OvenID,
                        principalTable: "Oven",
                        principalColumn: "OvenID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kotitev",
                columns: table => new
                {
                    kotitevID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumKotitve = table.Column<DateTime>(nullable: false),
                    OvcaID = table.Column<string>(nullable: true),
                    OvenID = table.Column<string>(nullable: true),
                    SteviloMrtvih = table.Column<int>(nullable: false),
                    Opombe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kotitev", x => x.kotitevID);
                    table.ForeignKey(
                        name: "FK_Kotitev_Ovca_OvcaID",
                        column: x => x.OvcaID,
                        principalTable: "Ovca",
                        principalColumn: "OvcaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Kotitev_Oven_OvenID",
                        column: x => x.OvenID,
                        principalTable: "Oven",
                        principalColumn: "OvenID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Jagenjcek",
                columns: table => new
                {
                    skritIdJagenjcka = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdJagenjcka = table.Column<string>(nullable: false),
                    kotitevID = table.Column<int>(nullable: false),
                    spol = table.Column<string>(nullable: true),
                    CredaCredeID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jagenjcek", x => x.skritIdJagenjcka);
                    table.ForeignKey(
                        name: "FK_Jagenjcek_Crede_CredaCredeID",
                        column: x => x.CredaCredeID,
                        principalTable: "Crede",
                        principalColumn: "CredeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Jagenjcek_Kotitev_kotitevID",
                        column: x => x.kotitevID,
                        principalTable: "Kotitev",
                        principalColumn: "kotitevID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Gonitev_OvcaID",
                table: "Gonitev",
                column: "OvcaID");

            migrationBuilder.CreateIndex(
                name: "IX_Gonitev_OvenID",
                table: "Gonitev",
                column: "OvenID");

            migrationBuilder.CreateIndex(
                name: "IX_Jagenjcek_CredaCredeID",
                table: "Jagenjcek",
                column: "CredaCredeID");

            migrationBuilder.CreateIndex(
                name: "IX_Jagenjcek_kotitevID",
                table: "Jagenjcek",
                column: "kotitevID");

            migrationBuilder.CreateIndex(
                name: "IX_Kotitev_OvcaID",
                table: "Kotitev",
                column: "OvcaID");

            migrationBuilder.CreateIndex(
                name: "IX_Kotitev_OvenID",
                table: "Kotitev",
                column: "OvenID");

            migrationBuilder.CreateIndex(
                name: "IX_Ovca_CredaID",
                table: "Ovca",
                column: "CredaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ovca_mamaID",
                table: "Ovca",
                column: "mamaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ovca_oceID",
                table: "Ovca",
                column: "oceID");

            migrationBuilder.CreateIndex(
                name: "IX_Oven_CredaID",
                table: "Oven",
                column: "CredaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Gonitev");

            migrationBuilder.DropTable(
                name: "Jagenjcek");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Kotitev");

            migrationBuilder.DropTable(
                name: "Ovca");

            migrationBuilder.DropTable(
                name: "Oven");

            migrationBuilder.DropTable(
                name: "Crede");
        }
    }
}
