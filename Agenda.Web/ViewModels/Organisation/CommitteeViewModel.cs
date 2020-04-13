// <copyright file="CommitteeViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Organisations;

namespace Agenda.Web.ViewModels.Organisation
{
    /// <summary>
    /// Committee view model.
    /// </summary>
    public class CommitteeViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommitteeViewModel"/> class.
        /// </summary>
        /// <param name="id">Committee Id.</param>
        /// <param name="name">Committee Name.</param>
        public CommitteeViewModel(
            Guid id,
            string name)
        {
            this.Id = id;
            this.Name = name;
        }

        /// <summary>
        /// Gets the view action button text.
        /// </summary>
        [Display(Name = "View")]
        public string ViewActionButtonText { get; } = null;

        /// <summary>
        /// Gets the Committee Id.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the Committee Name.
        /// </summary>
        [DisplayName("Name")]
        public string Name { get; }

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <param name="committee">Committee.</param>
        /// <returns>View model.</returns>
        public static CommitteeViewModel Create(ICommittee committee)
        {
            if (committee == null)
            {
                throw new ArgumentNullException(nameof(committee));
            }

            return new CommitteeViewModel(
                committee.Id,
                committee.Name);
        }
    }
}