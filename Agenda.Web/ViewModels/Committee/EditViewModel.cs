// <copyright file="EditViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Web.Models;
using DomainMetadata = Agenda.Domain.DomainObjects.Committees.Committee.DomainMetadata;

namespace Agenda.Web.ViewModels.Committee
{
    /// <summary>
    /// Add view model.
    /// </summary>
    public class EditViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EditViewModel"/> class.
        /// </summary>
        public EditViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EditViewModel"/> class.
        /// </summary>
        /// <param name="formState">Form State.</param>
        /// <param name="id">Committee Id.</param>
        /// <param name="organisationName">Organisation Name.</param>
        /// <param name="name">Committee Name.</param>
        /// <param name="description">Committee Description.</param>
        public EditViewModel(
            FormState formState,
            Guid id,
            string organisationName,
            string name,
            string description)
        {
            this.FormState = formState;
            this.Id = id;
            this.OrganisationName = organisationName;
            this.Name = name;
            this.Description = description;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the Form State.
        /// </summary>
        public FormState FormState { get; set; }

        /// <summary>
        /// Gets or sets the Committee Id.
        /// </summary>
        [MyRequired]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Organisation Name.
        /// </summary>
        [Display(Name = "Organisation")]
        [MyRequired]
        public string OrganisationName { get; set; }

        /// <summary>
        /// Gets or sets the Committee Name.
        /// </summary>
        [Display(Name = "Name")]
        [MyStringLength(
            DomainMetadata.Name.MaxLength,
            DomainMetadata.Name.MinLength)]
        [MyRequired]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Committee Description.
        /// </summary>
        [Display(Name = "Description")]
        [MyStringLength(
            DomainMetadata.Description.MaxLength,
            DomainMetadata.Description.MinLength)]
        [MyRequired]
        public string Description { get; set; }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <param name="committee">Organisation.</param>
        /// <returns>Edit view model.</returns>
        public static EditViewModel Create(ICommittee committee)
        {
            if (committee == null)
            {
                throw new ArgumentNullException(nameof(committee));
            }

            return new EditViewModel(
                formState: FormState.Initial,
                id: committee.Id,
                organisationName: committee.Organisation.Name,
                name: committee.Name,
                description: committee.Description);
        }

        /// <summary>
        /// Converts instance to Committee domain object.
        /// </summary>
        /// <param name="organisation">Organisation.</param>
        /// <returns>Committee domain object.</returns>
        public ICommittee ToDomain(IOrganisation organisation)
        {
            return new Domain.DomainObjects.Committees.Committee(
                id: this.Id,
                organisation: organisation,
                name: this.Name,
                description: this.Description);
        }

        #endregion Public Methods
    }
}