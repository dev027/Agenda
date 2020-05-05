// <copyright file="20200505170547_AddBgColourToOrganisation.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda.Migration.Migrations
{
    /// <summary>
    /// Add BgColour to Organisations.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.Migrations.Migration" />
    public partial class AddBgColourToOrganisation : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

            migrationBuilder.AddColumn<string>(
                name: "BgColour",
                table: "Organisations",
                maxLength: 50,
                nullable: false,
                defaultValue: "000000");
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

            migrationBuilder.DropColumn(
                name: "BgColour",
                table: "Organisations");
        }
    }
}
