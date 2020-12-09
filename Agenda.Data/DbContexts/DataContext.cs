// <copyright file="DataContext.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Agenda.Data.Dtos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Agenda.Data.DbContexts
{
    /// <summary>
    /// Database Context.
    /// </summary>
    /// <seealso cref="IdentityDbContext" />
    public partial class DataContext : IdentityDbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataContext"/> class.
        /// </summary>
        /// <param name="options">DBContext Options.</param>
        public DataContext(DbContextOptions options)
        : base(options)
        {
        }

        /// <summary>
        /// Invokes the on model creating for all dtos.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void InvokeOnModelCreatingForAllDtos(ModelBuilder builder)
        {
            IList<Type> dtoTypes = typeof(BaseDto)
                .Assembly.GetTypes()
                .Where(t => t.IsSubclassOf(typeof(BaseDto)))
                .Where(t => !t.IsAbstract)
                .ToList();

            foreach (Type type in dtoTypes)
            {
                MethodInfo? method = type.GetMethod("OnModelCreating");
                if (method != null)
                {
                    method.Invoke(
                        null,
                        new object?[] { builder });
                }
            }
        }

        /// <summary>
        /// Disables the cascade delete.
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void DisableCascadeDelete(ModelBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            foreach (IMutableForeignKey foreignKey in builder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }

        /// <summary>
        /// Override this method to further configure the model that was discovered by convention from the entity types
        /// exposed in dbset properties on your derived context. The resulting model may be cached
        /// and re-used for subsequent instances of your derived context.
        /// </summary>
        /// <param name="builder">The builder being used to construct the model for this context. Databases (and other extensions) typically
        /// define extension methods on this object that allow you to configure aspects of the model that are specific
        /// to a given database.</param>
        /// <exception cref="ArgumentNullException">modelBuilder.</exception>
        /// <remarks>
        /// If a model is explicitly set on the options for this context" />)
        /// then this method will not be run.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            base.OnModelCreating(builder);

            InvokeOnModelCreatingForAllDtos(builder);
            DisableCascadeDelete(builder);
        }
    }
}