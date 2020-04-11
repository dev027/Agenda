// <copyright file="IWho.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;

namespace Agenda.Utilities.Models.Whos
{
    /// <summary>
    /// Details of Who made the request.
    /// </summary>
    public interface IWho
    {
        /// <summary>
        /// Gets the Correlation Id.
        /// </summary>
        Guid CorrelationId { get; }

        /// <summary>
        /// Gets the Controller Name.
        /// </summary>
        string ControllerName { get; }

        /// <summary>
        /// Gets the Action Name.
        /// </summary>
        string ActionName { get; }

        /// <summary>
        /// Gets the Request Path.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Gets the Request Query String.
        /// </summary>
        string QueryString { get; }
    }
}