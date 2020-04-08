// <copyright file="OrganisationViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Agenda.Domain.DomainObjects.Organisations;

namespace Agenda.Web.ViewModels.OrganisationOverview
{
    /// <summary>
    /// Organisation view model.
    /// </summary>
    public class OrganisationViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrganisationViewModel"/> class.
        /// </summary>
        /// <param name="id">Organisation Id.</param>
        /// <param name="code">Organisation Code.</param>
        /// <param name="name">Organisation Name.</param>
        public OrganisationViewModel(
            Guid id,
            string code,
            string name)
        {
            this.Id = id;
            this.Code = code;
            this.Name = name;
        }

        /// <summary>
        /// Gets the view action button text.
        /// </summary>
        [Display(Name = "View")]
        public string ViewActionButtonText { get; } = null;

        /// <summary>
        /// Gets the Organisation Id.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the Organisation Code.
        /// </summary>
        [DisplayName("Code")]
        public string Code { get; }

        /// <summary>
        /// Gets the Organisation Name.
        /// </summary>
        [DisplayName("Name")]
        public string Name { get; }

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <param name="organisation">Organisation.</param>
        /// <returns>View model.</returns>
        public static OrganisationViewModel Create(IOrganisation organisation)
        {
            if (organisation == null)
            {
                throw new ArgumentNullException(nameof(organisation));
            }

            return new OrganisationViewModel(
                organisation.Id,
                organisation.Code,
                organisation.Name);
        }
    }
}