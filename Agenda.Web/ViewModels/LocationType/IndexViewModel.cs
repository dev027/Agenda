// <copyright file="IndexViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using Agenda.Domain.DomainObjects.LocationTypes;

namespace Agenda.Web.ViewModels.LocationType
{
    /// <summary>
    /// View Location Type view model.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexViewModel"/> class.
        /// </summary>
        /// <param name="locationTypeId">Location Type Id.</param>
        /// <param name="code">Location Type Code.</param>
        /// <param name="name">Location Type Name.</param>
        /// <param name="description">Location Type Description.</param>
        public IndexViewModel(
            Guid locationTypeId,
            string code,
            string name,
            string description)
        {
            this.LocationTypeId = locationTypeId;
            this.Code = code;
            this.Name = name;
            this.Description = description;
        }

        /// <summary>
        /// Gets the Page Title.
        /// </summary>
        [Display(Name = "View Location Type")]
        public string PageTitle { get; } = null;

        /// <summary>
        /// Gets the Card Title.
        /// </summary>
        [Display(Name = "Location Type")]
        public string CardTitle { get; } = null;

        /// <summary>
        /// Gets the Location Type Id.
        /// </summary>
        public Guid LocationTypeId { get; }

        /// <summary>
        /// Gets the Location Type Code.
        /// </summary>
        [Display(Name = "Code")]
        public string Code { get; }

        /// <summary>
        /// Gets the Location Type Name.
        /// </summary>
        [Display(Name = "Name")]
        public string Name { get; }

        /// <summary>
        /// Gets Location Type Description.
        /// </summary>
        [Display(Name = "Description")]
        public string Description { get; }

        /// <summary>
        /// Creates the Index view model.
        /// </summary>
        /// <param name="locationType">Location Type.</param>
        /// <returns>View model.</returns>
        public static IndexViewModel Create(ILocationType locationType)
        {
            if (locationType == null)
            {
                throw new ArgumentNullException(nameof(locationType));
            }

            return new IndexViewModel(
                locationTypeId: locationType.Id,
                code: locationType.Code,
                name: locationType.Name,
                description: locationType.Description);
        }
    }
}