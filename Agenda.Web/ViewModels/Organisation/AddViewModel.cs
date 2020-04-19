// <copyright file="AddViewModel.cs" company="Do It Wright">
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
        /// <param name="code">Organisation Code.</param>
        /// <param name="name">Organisation Name.</param>
        public AddViewModel(
            FormState formState,
            string code,
            string name)
        {
            this.FormState = formState;
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
        /// Converts view model to Organisation domain object.
        /// </summary>
        /// <returns>Organisation domain object.</returns>
        public IOrganisation ToDomain()
        {
            return new Domain.DomainObjects.Organisations.Organisation(
                id: Guid.NewGuid(),
                code: this.Code,
                name: this.Name);
        }

        #endregion Public Methods
    }
}