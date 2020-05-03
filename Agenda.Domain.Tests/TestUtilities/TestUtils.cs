// <copyright file="TestUtils.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Globalization;
using System.Reflection;
using Agenda.Domain.Tests.Resources;

namespace Agenda.Domain.Tests.TestUtilities
{
    /// <summary>
    /// Test Utilities.
    /// </summary>
    public static class TestUtils
    {
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
                        ExceptionResource.Property___WasNotFoundInType___,
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