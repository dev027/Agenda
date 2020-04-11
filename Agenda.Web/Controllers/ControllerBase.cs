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
        protected ControllerBase(Type controllerType)
        {
            if (controllerType == null)
            {
                throw new ArgumentNullException(nameof(controllerType));
            }

            this.controllerName = controllerType.Name;
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
                path: this.Request.Path,
                queryString: this.Request.QueryString.ToString());
        }

        /// <summary>
        /// Log entry into a controller action.
        /// </summary>
        /// <param name="logger">The logger.</param>
        protected void Entry(ILogger logger)
        {
            logger.LogDebug(
                "[{CorrelationId}] ENTRY: {ActionName}",
                this.who.CorrelationId,
                this.who.ActionName);
        }

        /// <summary>
        /// Log exit from a controller action.
        /// </summary>
        /// <param name="logger">The logger.</param>
        protected void Exit(ILogger logger)
        {
            logger.LogDebug(
                "[{CorrelationId}] EXIT: {ActionName}",
                this.who.CorrelationId,
                this.who.ActionName);
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
                "[{CorrelationId}] EXIT: {ActionName}: " +
                "Redirect to {RedirectControllerName}/{RedirectActionName}",
                this.who.CorrelationId,
                this.who.ActionName,
                redirectToAction.ControllerName,
                redirectToAction.ActionName);

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
                "[{CorrelationId}] EXIT: {ActionName}: " +
                "View {ViewName}, Model: {@Model}, Status: {StatusCode}",
                this.who.CorrelationId,
                this.who.ActionName,
                view.ViewName,
                view.Model,
                view.StatusCode);

            return view;
        }
    }
}