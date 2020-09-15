// <copyright file="AccountController.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

////using System;
////using System.Threading.Tasks;
////using Agenda.Utilities.Models.Whos;
////using Agenda.Web.ViewModels.Account;
////using Microsoft.AspNetCore.Identity;
////using Microsoft.AspNetCore.Mvc;
////using Microsoft.Extensions.Logging;

////namespace Agenda.Web.Controllers
////{
////    /// <summary>
////    /// Account Controller.
////    /// </summary>
////    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
////    public class AccountController : ControllerBase
////    {
////        private readonly ILogger<AccountController> logger;
////        private readonly UserManager<IdentityUser> userManager;
////        private readonly SignInManager<IdentityUser> signInManager;

////        /// <summary>
////        /// Initializes a new instance of the <see cref="AccountController"/> class.
////        /// </summary>
////        /// <param name="logger">Logger.</param>
////        /// <param name="userManager">User Manager.</param>
////        /// <param name="signInManager">Sign In Manager.</param>
////        /// <param name="isTestMode">Is Test Mode.</param>
////        public AccountController(
////            ILogger<AccountController> logger,
////            UserManager<IdentityUser> userManager,
////            SignInManager<IdentityUser> signInManager,
////            bool isTestMode = false)
////            : base(typeof(AccountController), isTestMode)
////        {
////            this.logger = logger;
////            this.userManager = userManager;
////            this.signInManager = signInManager;
////        }

////        /// <summary>
////        /// Register New User.
////        /// </summary>
////        /// <returns>View.</returns>
////        [HttpGet]
////        public IActionResult Register()
////        {
////            return this.View();
////        }

////        /// <summary>
////        /// SOMETHING.
////        /// </summary>
////        /// <param name="model">The model.</param>
////        /// <returns>SOMETHING.ELSE.</returns>
////        [HttpPost]
////        public async Task<IActionResult> Register(RegisterViewModel model)
////        {
////            IWho who = this.Who(nameof(this.Register));

////            RegisterViewModel model1 = model?.Copy();

////            this.logger.LogDebug(
////                "ENTRY {ActionName}(who, model) {@who} {@model}",
////                who.ActionName,
////                who,
////                model1);

////            if (model == null)
////            {
////                throw new ArgumentNullException(nameof(model));
////            }

////            if (this.ModelState.IsValid)
////            {
////                IdentityUser user = new IdentityUser
////                {
////                    UserName = model.Email,
////                    Email = model.Email
////                };

////                IdentityResult result = await this.userManager.CreateAsync(
////                    user: user,
////                    password: model.Password)
////                    .ConfigureAwait(false);

////                if (result.Succeeded)
////                {
////                    await this.signInManager.SignInAsync(
////                            user: user,
////                            isPersistent: false)
////                        .ConfigureAwait(false);
////                    return this.ExitRedirectToAction(
////                        this.logger,
////                        this.RedirectToAction("Index", "Home"));
////                }

////                foreach (IdentityError error in result.Errors)
////                {
////                    this.ModelState.AddModelError(string.Empty, error.Description);
////                }
////            }

////            return this.ExitView(this.logger, this.View(model));
////        }
////    }
////}
