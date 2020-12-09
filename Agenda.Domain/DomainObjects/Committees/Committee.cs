// <copyright file="Committee.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.DomainObjects.Organisations;

namespace Agenda.Domain.DomainObjects.Committees
{
    /// <summary>
    /// Group. Typically a committee, sub-committee or working party.
    /// </summary>
    public class Committee : ICommittee
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Committee"/> class.
        /// </summary>
        /// <param name="id">Committee Id.</param>
        /// <param name="organisation">Organisation.</param>
        /// <param name="name">Group Name.</param>
        /// <param name="description">Group Description.</param>
        public Committee(
            Guid id,
            IOrganisation organisation,
            string name,
            string description)
        {
            this.Id = id;
            this.Organisation = organisation;
            this.Name = !string.IsNullOrEmpty(name) ? name : throw new ArgumentNullException(nameof(name));
            this.Description = !string.IsNullOrEmpty(description) ? description : throw new ArgumentNullException(nameof(description));
        }

        /// <inheritdoc/>
        public Guid Id { get; }

        /// <inheritdoc/>
        public IOrganisation Organisation { get; }

        /// <inheritdoc/>
        public string Name { get; }

        /// <inheritdoc/>
        public string Description { get; }
    }
}