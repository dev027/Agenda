// <copyright file="CommitteeViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Domain.DomainObjects.Committees;

namespace Agenda.Web.ViewModels.CommitteeOverview
{
    /// <summary>
    /// Committee view model.
    /// </summary>
    public class CommitteeViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommitteeViewModel"/> class.
        /// </summary>
        /// <param name="committeeId">Committee Id.</param>
        /// <param name="name">Committee Name.</param>
        /// <param name="description">Committee Description.</param>
        public CommitteeViewModel(
            Guid committeeId,
            string name,
            string description)
        {
            this.CommitteeId = committeeId;
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Gets the Committee Id.
        /// </summary>
        public Guid CommitteeId { get; }

        /// <summary>
        /// Gets the Committee Name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the Committee Description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Creates the Committee view model.
        /// </summary>
        /// <param name="committee">Committee.</param>
        /// <returns>Committee view model.</returns>
        public static CommitteeViewModel Create(ICommittee committee)
        {
            if (committee == null)
            {
                throw new ArgumentNullException(nameof(committee));
            }

            return new CommitteeViewModel(
                committeeId: committee.Id,
                name: committee.Name,
                description: committee.Description);
        }
    }
}