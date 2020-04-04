// <copyright file="ErrorController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
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
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            ////IStatusCodeReExecuteFeature statusCodeResult =
            ////    this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404: // NotFound
                    return this.View("NotFound");
            }

            this.ViewBag.ErrorMessage = $"Error {statusCode}";

            return this.View("Unknown");
        }

        /// <summary>
        /// Global Exception Error Handler.
        /// </summary>
        /// <returns>View.</returns>
        [Route("/Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            IExceptionHandlerPathFeature exceptionDetails =
                this.HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            this.ViewBag.ExceptionPath = exceptionDetails.Path;
            this.ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            this.ViewBag.Stacktrace = exceptionDetails.Error.StackTrace;

            return this.View("Error");
        }
    }
}