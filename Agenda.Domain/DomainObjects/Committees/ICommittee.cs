// <copyright file="ICommittee.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.DomainObjects.Organisations;

namespace Agenda.Domain.DomainObjects.Committees
{
    /// <summary>
    /// Committee.
    /// </summary>
    public interface ICommittee
    {
        /// <summary>
        /// Gets the Committee Id.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the Organisation.
        /// </summary>
        IOrganisation Organisation { get; }

        /// <summary>
        /// Gets the Committee Name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the Committee Description.
        /// </summary>
        string Description { get; }
    }
}