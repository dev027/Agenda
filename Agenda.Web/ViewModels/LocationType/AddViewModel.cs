// <copyright file="AddViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using Agenda.Domain.DomainObjects.LocationTypes;
using Agenda.Web.Models;
using Agenda.Web.Models.ValidationAttributes;
using DomainMetadata = Agenda.Domain.DomainObjects.LocationTypes.DomainMetadata;

namespace Agenda.Web.ViewModels.LocationType
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
        /// <param name="locationTypeId">Location Type Id.</param>
        /// <param name="code">Location Type Code.</param>
        /// <param name="name">Location Type Name.</param>
        /// <param name="description">Location Type Description.</param>
        public AddViewModel(
            FormState formState,
            Guid locationTypeId,
            string code,
            string name,
            string description)
        {
            this.FormState = formState;
            this.LocationTypeId = locationTypeId;
            this.Code = code;
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
        /// Gets or sets the Location Type Id.
        /// </summary>
        public Guid LocationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the Location Type Code.
        /// </summary>
        [Display(Name = "Code")]
        [MyStringLength(
            DomainMetadata.Code.MaxLength,
            DomainMetadata.Code.MinLength)]
        [MyRequired]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the Location Type Name.
        /// </summary>
        [Display(Name = "Name")]
        [MyStringLength(
            DomainMetadata.Name.MaxLength,
            DomainMetadata.Name.MinLength)]
        [MyRequired]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Location Type Description.
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
        /// <param name="locationType">Location Type.</param>
        /// <returns>Add view model.</returns>
        public static AddViewModel Create(ILocationType locationType)
        {
            if (locationType == null)
            {
                throw new ArgumentNullException(nameof(locationType));
            }

            return new AddViewModel(
                formState: FormState.Initial,
                locationTypeId: locationType.Id,
                code: string.Empty,
                name: string.Empty,
                description: string.Empty);
        }

        /// <summary>
        /// Converts view model to Location Type domain object.
        /// </summary>
        /// <returns>Committee domain object.</returns>
        public ILocationType ToDomain()
        {
            return new Domain.DomainObjects.LocationTypes.LocationType(
                id: Guid.NewGuid(),
                code: this.Code,
                name: this.Name,
                description: this.Description);
        }

        #endregion Public Methods
    }
}