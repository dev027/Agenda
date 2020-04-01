// <copyright file="HomeController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using Agenda.Web.Areas.Api.Models.Home;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.Web.Controllers
{
    /// <summary>
    /// Home Controller.
    /// </summary>
    /// <seealso cref="Controller" />
    public class HomeController : Controller
    {
        /// <summary>
        /// Display the default view.
        /// </summary>
        /// <returns>Default View.</returns>
        [HttpGet("/")]
        [HttpGet("/[controller]")]
        [HttpGet("/[controller]/index")]
        public IActionResult Index()
        {
            IndexViewModel model = IndexViewModel.Create();
            return this.View(model);
        }
    }
}