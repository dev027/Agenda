// <copyright file="DataContext.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Agenda.Data.DbContexts
{
    /// <summary>
    /// Database Context.
    /// </summary>
    /// <seealso cref="DbContext" />
    public partial class DataContext : DbContext
    {
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        /// <param name="connectionString">Connection string.</param>
        public DataContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder == null)
            {
                throw new ArgumentNullException(nameof(optionsBuilder));
            }

            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            ////const string connectionString = "data source=WRIGHT1\\SQLEXPRESS01;" +
            ////                                "initial catalog=Agenda;" +
            ////                                "Integrated Security=True";

            optionsBuilder.UseSqlServer(this.connectionString);
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in dbset properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <exception cref="ArgumentNullException">modelBuilder.</exception>
        /// <remarks>
        /// If a model is explicitly set on the options for this context" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            base.OnModelCreating(modelBuilder);

            foreach (IMutableForeignKey foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}