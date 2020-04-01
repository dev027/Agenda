// <copyright file="OnConfiguringTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Linq;
using Agenda.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.DbContexts.DataContextTests
{
    /// <summary>
    /// Test the <see cref="DataContext.OnConfiguring"/> method.
    /// </summary>
    [TestClass]
    public class OnConfiguringTests
    {
        /// <summary>
        /// Test that <see cref="DataContext.OnConfiguring"/> works with no pre-configuration.
        /// </summary>
        [TestMethod]
        public void Test_With_No_PreConfiguration()
        {
            // ACT
            using DataContext context = new DataContext();
            _ = context.Organisations.Any();
        }

        /// <summary>
        /// Test that <see cref="DataContext.OnConfiguring"/> works with pre-configuration options.
        /// </summary>
        [TestMethod]
        public void Test_With_PreConfiguration_Options()
        {
            // ARRANGE
            using DataContextWrapper contextWrapper = new DataContextWrapper();
            DbContextOptionsBuilder<DataContext> optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseInMemoryDatabase("Test");

            // ACT
            contextWrapper.TestOnConfiguring(optionsBuilder);
        }

        /// <summary>
        /// Test that <see cref="DataContext.OnConfiguring"/> throws exception with null options.
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Test_When_Called_With_Null_Options_Throw_Exceptions()
        {
            // ARRANGE
            using DataContextWrapper contextWrapper = new DataContextWrapper();

            // ACT
            contextWrapper.TestOnConfiguring(null);
        }

        /// <summary>
        /// Test Wrapper for DataContext so we can access the <see cref="DataContext.OnConfiguring"/>.
        /// </summary>
        /// <seealso cref="DataContext" />
        private class DataContextWrapper : DataContext
        {
            /// <summary>
            /// Tests the protected <see cref="DataContext.OnConfiguring"/> method.
            /// </summary>
            /// <param name="optionsBuilder">Options Builder.</param>
            public void TestOnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                this.OnConfiguring(optionsBuilder);
            }
        }
    }
}