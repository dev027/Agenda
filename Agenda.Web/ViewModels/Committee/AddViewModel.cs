﻿// <copyright file="AddViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Web.Models;
using Agenda.Web.Models.ValidationAttributes;
using DomainMetadata = Agenda.Domain.DomainObjects.Committees.DomainMetadata;

namespace Agenda.Web.ViewModels.Committee
{
    /// <summary>
    /// Add view model.
    /// </summary>
    public class AddViewModel
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AddViewModel"/> class.
        /// </summary>
        public AddViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddViewModel"/> class.
        /// </summary>
        /// <param name="formState">Form State.</param>
        /// <param name="organisationId">Organisation Id.</param>
        /// <param name="organisationName">Organisation Name.</param>
        /// <param name="name">Committee Name.</param>
        /// <param name="description">Committee Description.</param>
        public AddViewModel(
            FormState formState,
            Guid organisationId,
            string organisationName,
            string name,
            string description)
        {
            this.FormState = formState;
            this.OrganisationId = organisationId;
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
        /// Gets or sets the Organisation Id.
        /// </summary>
        public Guid OrganisationId { get; set; }

        /// <summary>
        /// Gets or sets the Organisation Name.
        /// </summary>
        [Display(Name = "Organisation")]
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
        /// Creates the Add view model.
        /// </summary>
        /// <param name="organisation">Organisation.</param>
        /// <returns>Add view model.</returns>
        public static AddViewModel Create(IOrganisation organisation)
        {
            if (organisation == null)
            {
                throw new ArgumentNullException(nameof(organisation));
            }

            return new AddViewModel(
                formState: FormState.Initial,
                organisationId: organisation.Id,
                organisationName: organisation.Name,
                name: string.Empty,
                description: string.Empty);
        }

        /// <summary>
        /// Converts view model to Committee domain object.
        /// </summary>
        /// <param name="organisation">Organisation.</param>
        /// <returns>Committee domain object.</returns>
        public ICommittee ToDomain(IOrganisation organisation)
        {
            return new Domain.DomainObjects.Committees.Committee(
                id: Guid.NewGuid(),
                organisation: organisation,
                name: this.Name,
                description: this.Description);
        }

        #endregion Public Methods
    }
}