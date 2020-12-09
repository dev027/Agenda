// <copyright file="EditViewModel.cs" company="Do It Wright">
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
        /// <param name="locationTypeId">Location Type Id.</param>
        /// <param name="code">Location Type Code.</param>
        /// <param name="name">Location Type Name.</param>
        /// <param name="description">Location Type Description.</param>
        public EditViewModel(
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
        [MyRequired]
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
        /// Creates the view model.
        /// </summary>
        /// <param name="locationType">Location Type.</param>
        /// <returns>Edit view model.</returns>
        public static EditViewModel Create(ILocationType locationType)
        {
            if (locationType == null)
            {
                throw new ArgumentNullException(nameof(locationType));
            }

            return new EditViewModel(
                formState: FormState.Initial,
                locationTypeId: locationType.Id,
                code: locationType.Code,
                name: locationType.Name,
                description: locationType.Description);
        }

        /// <summary>
        /// Converts instance to Location Type domain object.
        /// </summary>
        /// <returns>Location Type domain object.</returns>
        public ILocationType ToDomain()
        {
            return new Domain.DomainObjects.LocationTypes.LocationType(
                id: this.LocationTypeId,
                code: this.Code,
                name: this.Name,
                description: this.Description);
        }

        #endregion Public Methods
    }
}