﻿// <copyright file="EditViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using Agenda.Domain.DomainObjects.Organisations;
using Agenda.Web.Models;
using Agenda.Web.Models.ValidationAttributes;
using DomainMetadata = Agenda.Domain.DomainObjects.Organisations.DomainMetadata;

namespace Agenda.Web.ViewModels.Organisation
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
        /// <param name="id">Organisation Id.</param>
        /// <param name="code">Organisation Code.</param>
        /// <param name="name">Organisation Name.</param>
        public EditViewModel(
            FormState formState,
            Guid id,
            string code,
            string name)
        {
            this.FormState = formState;
            this.Id = id;
            this.Code = code;
            this.Name = name;
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
        [MyRequired]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the Organisation Code.
        /// </summary>
        [Display(Name = "Code")]
        [MyStringLength(
            DomainMetadata.Code.MaxLength,
            DomainMetadata.Code.MinLength)]
        [MyRequired]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the Organisation Name.
        /// </summary>
        [Display(Name = "Name")]
        [MyStringLength(
            DomainMetadata.Name.MaxLength,
            DomainMetadata.Name.MinLength)]
        [MyRequired]
        public string Name { get; set; }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Creates the view model.
        /// </summary>
        /// <param name="organisation">Organisation.</param>
        /// <returns>Edit view model.</returns>
        public static EditViewModel Create(IOrganisation organisation)
        {
            if (organisation == null)
            {
                throw new ArgumentNullException(nameof(organisation));
            }

            return new EditViewModel(
                formState: FormState.Initial,
                id: organisation.Id,
                code: organisation.Code,
                name: organisation.Name);
        }

        /// <summary>
        /// Converts instance to Organisation domain object.
        /// </summary>
        /// <returns>Organisation domain object.</returns>
        public IOrganisation ToDomain()
        {
            return new Domain.DomainObjects.Organisations.Organisation(
                this.Id,
                this.Code,
                this.Name);
        }

        #endregion Public Methods
    }
}