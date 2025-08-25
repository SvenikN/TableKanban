using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;

#nullable disable

namespace TableKanban.Migrations
{
  /// <inheritdoc />
  public partial class edit1 : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
          name: "Tables",
          columns: table => new
          {
            TableId = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            TableName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
            Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
            DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Tables", x => x.TableId);
          });

      migrationBuilder.CreateTable(
          name: "Users",
          columns: table => new
          {
            UserId = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            UserName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
            Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
            DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Users", x => x.UserId);
          });

      migrationBuilder.CreateTable(
          name: "Stolbs",
          columns: table => new
          {
            StolbId = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            TableId = table.Column<int>(type: "integer", nullable: false),
            StolbName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
            Order = table.Column<int>(type: "integer", nullable: false),
            Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Stolbs", x => x.StolbId);
            table.ForeignKey(
                      name: "FK_Stolbs_Tables_TableId",
                      column: x => x.TableId,
                      principalTable: "Tables",
                      principalColumn: "TableId",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Cards",
          columns: table => new
          {
            CardId = table.Column<int>(type: "integer", nullable: false)
                  .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            StolbId = table.Column<int>(type: "integer", nullable: false),
            UserId = table.Column<int>(type: "integer", nullable: true),
            TableId = table.Column<int>(type: "integer", nullable: false),
            Title = table.Column<string>(type: "text", nullable: true),
            Description = table.Column<string>(type: "text", nullable: true),
            Status = table.Column<string>(type: "text", nullable: true),
            CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
          },
          constraints: table =>
          {
            table.PrimaryKey("PK_Cards", x => x.CardId);
            table.ForeignKey(
                      name: "FK_Cards_Stolbs_StolbId",
                      column: x => x.StolbId,
                      principalTable: "Stolbs",
                      principalColumn: "StolbId",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_Cards_Tables_TableId",
                      column: x => x.TableId,
                      principalTable: "Tables",
                      principalColumn: "TableId",
                      onDelete: ReferentialAction.Restrict);
            table.ForeignKey(
                      name: "FK_Cards_Users_UserId",
                      column: x => x.UserId,
                      principalTable: "Users",
                      principalColumn: "UserId",
                      onDelete: ReferentialAction.SetNull);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Cards_StolbId",
          table: "Cards",
          column: "StolbId");

      migrationBuilder.CreateIndex(
          name: "IX_Cards_TableId",
          table: "Cards",
          column: "TableId");

      migrationBuilder.CreateIndex(
          name: "IX_Cards_UserId",
          table: "Cards",
          column: "UserId");

      migrationBuilder.CreateIndex(
          name: "IX_Stolbs_TableId",
          table: "Stolbs",
          column: "TableId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(
          name: "Cards");

      migrationBuilder.DropTable(
          name: "Stolbs");

      migrationBuilder.DropTable(
          name: "Users");

      migrationBuilder.DropTable(
          name: "Tables");
    }
  }
}
