// <copyright file="ControllerBase.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Utilities.Models.Whos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// Base class for controllers.
    /// </summary>
    /// <seealso cref="Controller" />
    public abstract class ControllerBase : Controller
    {
        private readonly string controllerName;
        private IWho who;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControllerBase"/> class.
        /// </summary>
        /// <param name="controllerType">Type of the controller.</param>
        /// <param name="isTestingMode">Is Testing Mode.</param>
        protected ControllerBase(
            Type controllerType,
            bool isTestingMode)
        {
            if (controllerType == null)
            {
                throw new ArgumentNullException(nameof(controllerType));
            }

            this.controllerName = controllerType.Name;

            if (isTestingMode)
            {
                this.who = new Who(
                    controllerName: this.controllerName,
                    actionName: "Action",
                    path: "Path",
                    queryString: "QueryString",
                    clientIpAddress: "127.0.0.1",
                    username: null);
            }
        }

        /// <summary>
        /// Details of Who made this request.
        /// </summary>
        /// <param name="actionName">Action Name.</param>
        /// <returns> Who details. </returns>
        protected IWho Who(string actionName)
        {
            return this.who ??= new Who(
                controllerName: this.controllerName,
                actionName: actionName,
                path: this.Request?.Path ?? "No Path",
                queryString: this.Request?.QueryString.ToString() ?? "No Query String",
                clientIpAddress: this.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "None",
                username: null);
        }

        /// <summary>
        /// Log exit from a controller action when redirecting to a different action.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="redirectToAction">The redirect to action.</param>
        /// <returns>Redirect To Action result.</returns>
        protected RedirectToActionResult ExitRedirectToAction(
                    ILogger logger,
                    RedirectToActionResult redirectToAction)
        {
            if (redirectToAction == null)
            {
                throw new ArgumentNullException(nameof(redirectToAction));
            }

            logger.LogDebug(
                "EXIT {ActionName}: " +
                "Redirect to {RedirectControllerName}/{RedirectActionName}, " +
                "RouteValue {RouteValue} by {@who}",
                this.who.ActionName,
                redirectToAction.ControllerName ?? this.who.ControllerName,
                redirectToAction.ActionName,
                redirectToAction.RouteValues,
                this.who);

            return redirectToAction;
        }

        /// <summary>
        /// Log exit from a controller action when returning a view.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="view">The view.</param>
        /// <returns>View result.</returns>
        protected ViewResult ExitView(
                    ILogger logger,
                    ViewResult view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            logger.LogDebug(
                "EXIT {ActionName}: " +
                "View {ViewName}, Model: {@Model}, Status: {StatusCode} by {@who}",
                this.who.ActionName,
                view.ViewName ?? this.who.ActionName,
                view.Model,
                view.StatusCode,
                this.who);

            return view;
        }
    }
}