// <copyright file="SetUpStatus.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.Resources;

namespace Agenda.Domain.ValueObjects.SetupStatii
{
    /// <summary>
    /// Setup Status.
    /// </summary>
    /// <seealso cref="ISetupStatus" />
    public class SetupStatus : ISetupStatus
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SetupStatus"/> class.
        /// </summary>
        /// <param name="haveOrganisations">True if Have Organisations.</param>
        /// <param name="haveCommittees">True if Have Committees.</param>
        public SetupStatus(
            bool haveOrganisations,
            bool haveCommittees)
        {
            if (haveCommittees && !haveOrganisations)
            {
                throw new ArgumentException(
                    ExceptionResource.CannotHaveCommitteesWithoutOrganisations);
            }

            this.HaveOrganisations = haveOrganisations;
            this.HaveCommittees = haveCommittees;
        }

        /// <inheritdoc/>
        public bool HaveOrganisations { get; }

        /// <inheritdoc/>
        public bool HaveCommittees { get; }
    }
}