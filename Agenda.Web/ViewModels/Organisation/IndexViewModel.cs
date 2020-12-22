// <copyright file="IndexViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Agenda.Domain.DomainObjects.Organisations;

namespace Agenda.Web.ViewModels.Organisation
{
    /// <summary>
    /// View Organisation view model.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexViewModel"/> class.
        /// </summary>
        /// <param name="organisationId">Organisation Id.</param>
        /// <param name="code">Organisation Code.</param>
        /// <param name="name">Organisation Name.</param>
        /// <param name="bgColour">Background Colour.</param>
        /// <param name="committeeViewModels">Committee view models.</param>
        public IndexViewModel(
            Guid organisationId,
            string code,
            string name,
            string bgColour,
            IList<CommitteeViewModel> committeeViewModels)
        {
            this.OrganisationId = organisationId;
            this.Code = code;
            this.Name = name;
            this.BgColour = bgColour;
            this.CommitteeViewModels = committeeViewModels;
        }

        /// <summary>
        /// Gets the Page Title.
        /// </summary>
        /// <value>
        /// The page title.
        /// </value>
        [Display(Name = "View Organisation")]
        public string PageTitle { get; } = null;

        /// <summary>
        /// Gets the Page Title.
        /// </summary>
        /// <value>
        /// The page title.
        /// </value>
        [Display(Name = "Organisation")]
        public string CardTitle { get; } = null;

        /// <summary>
        /// Gets the Organisation Id.
        /// </summary>
        public Guid OrganisationId { get; }

        /// <summary>
        /// Gets the Organisation Code.
        /// </summary>
        [Display(Name = "Code")]
        public string Code { get; }

        /// <summary>
        /// Gets Organisation Name.
        /// </summary>
        [Display(Name = "Name")]
        public string Name { get; }

        /// <summary>
        /// Gets Background Colour.
        /// </summary>
        [Display(Name = "Background Colour")]
        public string BgColour { get; }

        /// <summary>
        /// Gets the Background Colour prefixed with a hash.
        /// </summary>
        public string BgColourWithHash => $"#{this.BgColour}";

        /// <summary>
        /// Gets the Committee view models.
        /// </summary>
        public IList<CommitteeViewModel> CommitteeViewModels { get; }

        /// <summary>
        /// Creates the Index view model.
        /// </summary>
        /// <param name="organisation">Organisation.</param>
        /// <returns>View model.</returns>
        public static IndexViewModel Create(
            IOrganisationWithCommittees organisation)
        {
            if (organisation == null)
            {
                throw new ArgumentNullException(nameof(organisation));
            }

            return new IndexViewModel(
                organisationId: organisation.Id,
                code: organisation.Code,
                name: organisation.Name,
                bgColour: organisation.BgColour,
                committeeViewModels: organisation.Committees.Select(CommitteeViewModel.Create).ToList());
        }
    }
}