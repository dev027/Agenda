// <copyright file="IndexViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using Agenda.Web.Controllers;
using Agenda.Web.ViewModels.Shared;

namespace Agenda.Web.Areas.Api.Models.Home
{
    /// <summary>
    /// View model for <see cref="HomeController.Index"/>.
    /// </summary>
    public class IndexViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexViewModel"/> class.
        /// </summary>
        /// <param name="headerViewModel">The header.</param>
        public IndexViewModel(HeaderViewModel headerViewModel)
        {
            this.HeaderViewModel = headerViewModel;
        }

        /// <summary>
        /// Gets the header view model.
        /// </summary>
        public HeaderViewModel HeaderViewModel { get; }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns>Index view model.</returns>
        public static IndexViewModel Create()
        {
            return new IndexViewModel(
                headerViewModel: HeaderViewModel.Create());
        }
    }
}