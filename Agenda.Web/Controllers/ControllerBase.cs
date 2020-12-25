// <copyright file="ControllerBase.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Runtime.CompilerServices;
using Agenda.Utilities.Models.Whos;
using Microsoft.AspNetCore.Mvc;

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
        protected IWho Who(
            [CallerMemberName] string actionName = "Unknown")
        {
            return this.who ??= new Who(
                controllerName: this.controllerName,
                actionName: actionName,
                path: this.Request?.Path ?? "No Path",
                queryString: this.Request?.QueryString.ToString() ?? "No Query String",
                clientIpAddress: this.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? "None",
                username: null);
        }
    }
}