// <copyright file="20200509123126_AddUsernameAndCorrelationIdToAuditHeaders.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda.Migration.Migrations
{
    /// <summary>
    /// Add Username and Correlation Id to Audit Headers.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.Migrations.Migration" />
    public partial class AddUsernameAndCorrelationIdToAuditHeaders : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

            migrationBuilder.AddColumn<Guid>(
                name: "CorrelationId",
                table: "AuditHeaders",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "AuditHeaders",
                maxLength: 100,
                nullable: false,
                defaultValue: "Guest");
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

            migrationBuilder.DropColumn(
                name: "CorrelationId",
                table: "AuditHeaders");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "AuditHeaders");
        }
    }
}
