// <copyright file="OnModelCreatingTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Data.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.DbContexts.DataContextTests
{
    /// <summary>
    /// Test the <see cref="DataContext.OnModelCreating"/> method.
    /// </summary>
    [TestClass]
    public class OnModelCreatingTests
    {
        /// <summary>
        /// Test that <see cref="DataContext.OnConfiguring"/> works with pre-configuration options.
        /// </summary>
        [TestMethod]
        public void Test_With_PreConfiguration_Options()
        {
            // ARRANGE
            using DataContextWrapper contextWrapper = new DataContextWrapper();
            ModelBuilder modelBuilder = new ModelBuilder(new ConventionSet());

            // ACT
            contextWrapper.TestOnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Test that <see cref="DataContext.OnConfiguring"/> throws exception with null options.
        /// </summary>
        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void Test_When_Called_With_Null_Options_Throw_Exceptions()
        {
            // ARRANGE
            using DataContextWrapper dataContextWrapper = new DataContextWrapper();

            // ACT
            dataContextWrapper.TestOnModelCreating(null);
        }

        /// <summary>
        /// Test Wrapper for DataContext so we can access the <see cref="DataContext.OnConfiguring"/>.
        /// </summary>
        /// <seealso cref="DataContext" />
        private class DataContextWrapper : DataContext
        {
            /// <summary>
            /// Tests the protected <see cref="DataContext.OnModelCreating"/> method.
            /// </summary>
            /// <param name="modelBuilder">Model Builder.</param>
            public void TestOnModelCreating(ModelBuilder modelBuilder)
            {
                this.OnModelCreating(modelBuilder);
            }
        }
    }
}