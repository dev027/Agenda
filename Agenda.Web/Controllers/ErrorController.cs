// <copyright file="ErrorController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// Error Controller.
    /// </summary>
    /// <seealso cref="Controller" />
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public ErrorController(ILogger<ErrorController> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Display custom error page.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <returns>View.</returns>
        [Route("/Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            IStatusCodeReExecuteFeature statusCodeResult =
                this.HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404: // NotFound
                    this.logger.LogWarning(
                        "404 Error Occurred." +
                        $" Path = {statusCodeResult.OriginalPath}" +
                        $" and QueryString = {statusCodeResult.OriginalQueryString}");
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

            this.logger.LogError($"The path {exceptionDetails.Path} " +
                            $"threw an exception {exceptionDetails.Error}");

            this.ViewBag.ExceptionPath = exceptionDetails.Path;
            this.ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            this.ViewBag.Stacktrace = exceptionDetails.Error.StackTrace;

            return this.View("Error");
        }
    }
}