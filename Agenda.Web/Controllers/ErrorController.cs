// <copyright file="ErrorController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Mvc;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// Error Controller.
    /// </summary>
    /// <seealso cref="Controller" />
    public class ErrorController : Controller
    {
        /// <summary>
        /// Display custom error page.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <returns>View.</returns>
        [Route("/Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            switch (statusCode)
            {
                case 404: // NotFound
                    return this.View("NotFound");
            }

            this.ViewBag.ErrorMessage = $"Error {statusCode}";

            return this.View("Unknown");
        }
    }
}