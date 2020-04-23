// <copyright file="20200423193700_AddLocationTable.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda.Migration.Migrations
{
    /// <summary>
    /// Add Location Table.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.Migrations.Migration" />
    public partial class AddLocationTable : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrganisationId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    What3Words = table.Column<string>(maxLength: 32, nullable: false),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Locations_OrganisationId",
                table: "Locations",
                column: "OrganisationId");
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
