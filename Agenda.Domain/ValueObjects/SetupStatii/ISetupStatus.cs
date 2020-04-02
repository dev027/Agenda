// <copyright file="ISetupStatus.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

namespace Agenda.Domain.ValueObjects.SetupStatii
{
    /// <summary>
    /// Setup Status.
    /// </summary>
    public interface ISetupStatus
    {
        /// <summary>
        /// Gets a value indicating whether [have organisations].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [have organisations]; otherwise, <c>false</c>.
        /// </value>
        bool HaveOrganisations { get; }

        /// <summary>
        /// Gets a value indicating whether [have meetings].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [have meetings]; otherwise, <c>false</c>.
        /// </value>
        bool HaveCommittees { get; }
    }
}