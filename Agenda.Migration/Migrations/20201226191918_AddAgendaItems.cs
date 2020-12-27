// <copyright file="20201226191918_AddAgendaItems.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda.Migration.Migrations
{
    /// <inheritdoc />
    public partial class AddAgendaItems : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgendaItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MeetingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ElderSiblingId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgendaItemType = table.Column<int>(type: "int", nullable: false),
                    ChildNumberingType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendaItems_AgendaItems_ElderSiblingId",
                        column: x => x.ElderSiblingId,
                        principalTable: "AgendaItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgendaItems_AgendaItems_ParentId",
                        column: x => x.ParentId,
                        principalTable: "AgendaItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AgendaItems_Meetings_MeetingId",
                        column: x => x.MeetingId,
                        principalTable: "Meetings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgendaItems_ElderSiblingId",
                table: "AgendaItems",
                column: "ElderSiblingId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaItems_MeetingId",
                table: "AgendaItems",
                column: "MeetingId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaItems_ParentId",
                table: "AgendaItems",
                column: "ParentId");
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendaItems");
        }
    }
}
