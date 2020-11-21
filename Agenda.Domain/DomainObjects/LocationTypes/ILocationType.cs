// <copyright file="ILocationType.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;

namespace Agenda.Domain.DomainObjects.LocationTypes
{
    /// <summary>
    /// Location Type.
    /// </summary>
    public interface ILocationType
    {
        /// <summary>
        /// Gets the Location Type Id.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the Location Type Code.
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Gets the Location Type Name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the Location Type Description.
        /// </summary>
        string Description { get; }
    }
}