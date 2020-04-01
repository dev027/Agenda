// <copyright file="ReadDatabaseTests.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Linq;
using Agenda.Data.DbContexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Agenda.Data.Tests.Dtos.MeetingTests
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
            using DataContext context = new DataContext();

            _ = context.Meetings.Any();
            _ = context.Meetings.FirstOrDefault();
        }
    }
}