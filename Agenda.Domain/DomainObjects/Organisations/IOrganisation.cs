// <copyright file="IOrganisation.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;

namespace Agenda.Domain.DomainObjects.Organisations
{
    /// <summary>
    /// Organisation.
    /// </summary>
    public interface IOrganisation
    {
        /// <summary>
        /// Gets the Organisation Id.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the Organisation Code.
        /// </summary>
        string Code { get; }

        /// <summary>
        /// Gets the Organisation Name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the Background Colour.
        /// </summary>
        string BgColour { get; }
    }
}