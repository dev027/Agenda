// <copyright file="OrganisationController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Web.Models;
using Agenda.Web.ViewModels.Organisation;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// SOMETHING.
    /// </summary>
    /// <seealso cref="Controller" />
    public class OrganisationController : Controller
    {
        /// <summary>
        /// Adds this instance.
        /// </summary>
        /// <param name="model">Add view model.</param>
        /// <returns>View.</returns>
        [HttpGet]
        [HttpPost]
        public IActionResult Add(AddViewModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.FormState == FormState.Initial)
            {
                model = new AddViewModel(
                    formState: FormState.Initial,
                    code: string.Empty,
                    name: string.Empty);
                this.ModelState.Clear();
            }

            return this.View(model);
        }
    }
}