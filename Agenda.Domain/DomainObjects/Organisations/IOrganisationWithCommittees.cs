// <copyright file="IOrganisationWithCommittees.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Agenda.Domain.DomainObjects.Committees;

namespace Agenda.Domain.DomainObjects.Organisations
{
    /// <summary>
    /// Organisation with Committees.
    /// </summary>
    /// <seealso cref="IOrganisation" />
    public interface IOrganisationWithCommittees : IOrganisation
    {
        /// <summary>
        /// Gets the Committees.
        /// </summary>
        IList<ICommittee> Committees { get; }
    }
}