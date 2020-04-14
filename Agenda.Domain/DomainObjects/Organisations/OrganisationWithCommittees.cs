// <copyright file="OrganisationWithCommittees.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Agenda.Domain.DomainObjects.Committees;

namespace Agenda.Domain.DomainObjects.Organisations
{
    /// <summary>
    /// Organisation with Committees.
    /// </summary>
    /// <seealso cref="Organisation" />
    /// <seealso cref="IOrganisationWithCommittees" />
    public class OrganisationWithCommittees : Organisation, IOrganisationWithCommittees
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrganisationWithCommittees"/> class.
        /// </summary>
        /// <param name="id">Organisation Id.</param>
        /// <param name="code">Organisation Code.</param>
        /// <param name="name">Organisation Name.</param>
        /// <param name="committees">Committees.</param>
        public OrganisationWithCommittees(
            Guid id,
            string code,
            string name,
            IList<ICommittee> committees)
            : base(id, code, name)
        {
            this.Committees = committees ?? throw new ArgumentNullException(nameof(committees));
        }

        /// <summary>
        /// Gets the Committees.
        /// </summary>
        public IList<ICommittee> Committees { get; }
    }
}