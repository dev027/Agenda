// <copyright file="Who.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;

namespace Agenda.Utilities.Models.Whos
{
    /// <summary>
    /// Who.
    /// </summary>
    /// <seealso cref="IWho" />
    public class Who : IWho
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Who"/> class.
        /// </summary>
        /// <param name="controllerName">Controller Name.</param>
        /// <param name="actionName">Action Name.</param>
        /// <param name="path">Request Path.</param>
        /// <param name="queryString">Request Query String.</param>
        public Who(
            string controllerName,
            string actionName,
            string path,
            string queryString)
        {
            this.CorrelationId = Guid.NewGuid();
            this.ControllerName = controllerName;
            this.ActionName = actionName;
            this.Path = path;
            this.QueryString = queryString;
        }

        /// <inheritdoc/>
        public Guid CorrelationId { get; }

        /// <inheritdoc/>
        public string ControllerName { get; }

        /// <inheritdoc/>
        public string ActionName { get; }

        /// <inheritdoc/>
        public string Path { get; }

        /// <inheritdoc/>
        public string QueryString { get; }
    }
}