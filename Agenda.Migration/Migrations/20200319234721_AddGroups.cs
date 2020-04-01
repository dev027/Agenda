// <copyright file="20200319234721_AddGroups.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Agenda.Migration.Migrations
{
    /// <summary>
    /// Add table Groups.
    /// </summary>
    /// <seealso cref="Microsoft.EntityFrameworkCore.Migrations.Migration" />
    public partial class AddGroups : Microsoft.EntityFrameworkCore.Migrations.Migration
    {
        /// <summary>
        /// <para>
        /// Builds the operations that will migrate the database 'up'.
        /// </para>
        /// <para>
        /// That is, builds the operations that will take the database from the state left in by the
        /// previous migration so that it is up-to-date with regard to this migration.
        /// </para>
        /// <para>
        /// This method must be overridden in each class the inherits from <see cref="Microsoft.EntityFrameworkCore.Migrations.Migration" />.
        /// </para>
        /// </summary>
        /// <param name="migrationBuilder">The <see cref="MigrationBuilder" /> that will build the operations.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<Guid>(),
                    OrganisationId = table.Column<Guid>(),
                    Name = table.Column<string>(),
                    Description = table.Column<string>()
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Groups_Organisations_OrganisationId",
                        column: x => x.OrganisationId,
                        principalTable: "Organisations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_OrganisationId",
                table: "Groups",
                column: "OrganisationId");
        }

        /// <summary>
        /// <para>
        /// Builds the operations that will migrate the database 'down'.
        /// </para>
        /// <para>
        /// That is, builds the operations that will take the database from the state left in by
        /// this migration so that it returns to the state that it was in before this migration was applied.
        /// </para>
        /// <para>
        /// This method must be overridden in each class the inherits from <see cref="Microsoft.EntityFrameworkCore.Migrations.Migration" /> if
        /// both 'up' and 'down' migrations are to be supported. If it is not overridden, then calling it
        /// will throw and it will not be possible to migrate in the 'down' direction.
        /// </para>
        /// </summary>
        /// <param name="migrationBuilder">The <see cref="MigrationBuilder" /> that will build the operations.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            if (migrationBuilder == null)
            {
                throw new ArgumentNullException(nameof(migrationBuilder));
            }

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
