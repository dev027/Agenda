// <copyright file="20200501182149_LocationIsParentOfMeeting.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda.Migration.Migrations
{
    /// <summary>
    /// Make the Locations table a parent of the Meetings table.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.Migrations.Migration" />
    public partial class LocationIsParentOfMeeting : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Meetings");

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Meetings",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Meetings_LocationId",
                table: "Meetings",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_Locations_LocationId",
                table: "Meetings",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_Locations_LocationId",
                table: "Meetings");

            migrationBuilder.DropIndex(
                name: "IX_Meetings_LocationId",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Meetings");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Meetings",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: string.Empty);
        }
    }
}
