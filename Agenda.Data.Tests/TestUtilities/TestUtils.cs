// <copyright file="TestUtils.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Globalization;
using System.Reflection;
using Agenda.Data.DbContexts;
using Agenda.Data.Tests.Resources;
using Microsoft.EntityFrameworkCore;

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

        /// <summary>
        /// Sets the private property value.
        /// </summary>
        /// <typeparam name="T">Property Type.</typeparam>
        /// <param name="target">Target object.</param>
        /// <param name="propName">Property name.</param>
        /// <param name="value">Value to set.</param>
        public static void SetPrivatePropertyValue<T>(
            this object target,
            string propName,
            T value)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            Type t = target.GetType();
            if (t.GetProperty(propName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance) == null)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(propName),
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ExceptionResources.Property___WasNotFoundInType___,
                        propName,
                        target.GetType().FullName));
            }

            t.InvokeMember(
                name: propName,
                invokeAttr: BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance,
                binder: null,
                target: target,
                args: new object[] { value },
                culture: CultureInfo.InvariantCulture);
        }
    }
}