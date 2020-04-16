// <copyright file="TestUtils.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using Agenda.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace Agenda.Data.Tests.TestUtilities
{
    /// <summary>
    /// Test Utilities.
    /// </summary>
    public static class TestUtils
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        public static string ConnectionString =>
            "data source=WRIGHT1\\SQLEXPRESS01;initial catalog=Agenda;Integrated Security=True";

        /// <summary>
        /// Gets the database context options.
        /// </summary>
        /// <value>
        /// The database context options.
        /// </value>
        public static DbContextOptions<DataContext> DbContextOptions
        {
            get
            {
                DbContextOptionsBuilder<DataContext> builder = new DbContextOptionsBuilder<DataContext>();
                builder.UseSqlServer(TestUtils.ConnectionString);
                return builder.Options;
            }
        }
    }
}