// <copyright file="HeaderViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

namespace Agenda.Web.ViewModels.Shared
{
    /// <summary>
    /// View model for Shared/_header.cshtml.
    /// </summary>
    public class HeaderViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HeaderViewModel"/> class.
        /// </summary>
        /// <param name="siteName">Site Name.</param>
        /// <param name="siteIconClass">Site Icon class name.</param>
        public HeaderViewModel(
            string siteName,
            string siteIconClass)
        {
            this.SiteName = siteName;
            this.SiteIconClass = siteIconClass;
        }

        /// <summary>
        /// Gets the name of the site.
        /// </summary>
        public string SiteName { get; }

        /// <summary>
        /// Gets the site icon class.
        /// </summary>
        public string SiteIconClass { get; }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>Header View Model.</returns>
        public static HeaderViewModel Create()
        {
            return new HeaderViewModel(
                siteName: "Meetings",
                siteIconClass: "fas fa-file-alt");
        }
    }
}