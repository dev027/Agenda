﻿// <copyright file="ReadDatabaseTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Linq;
using Agenda.Data.DbContexts;
using Agenda.Data.Tests.TestUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.OrganisationTests
{
    /// <summary>
    /// Read database tests.
    /// </summary>
    [TestClass]
    public class ReadDatabaseTests
    {
        /// <summary>
        /// Tests the read from database.
        /// </summary>
        [TestMethod]
        public void Test_Read_From_Database()
        {
            using DataContext context = new DataContext(TestUtils.DbContextOptions);

            _ = context.Organisations.Any();
            _ = context.Organisations.FirstOrDefault();

            Assert.IsTrue(true);
        }
    }
}