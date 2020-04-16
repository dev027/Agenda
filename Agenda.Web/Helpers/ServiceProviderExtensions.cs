// <copyright file="ServiceProviderExtensions.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Microsoft.Extensions.DependencyInjection;

namespace Agenda.Web.Helpers
{
    /// <summary>
    /// Service Provider extension methods.
    /// </summary>
    public static class ServiceProviderExtensions
    {
        /// <summary>
        /// Used to shorten code registration code.
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="provider">The provider.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>TYpe.</returns>
        public static T ResolveWith<T>(this IServiceProvider provider, params object[] parameters)
            where T : class =>
            ActivatorUtilities.CreateInstance<T>(provider, parameters);
    }
}
