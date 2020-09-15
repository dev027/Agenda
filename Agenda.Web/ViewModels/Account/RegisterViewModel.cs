// <copyright file="RegisterViewModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace Agenda.Web.ViewModels.Account
{
    /// <summary>
    /// Register View Model.
    /// </summary>
    public class RegisterViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterViewModel"/> class.
        /// </summary>
        public RegisterViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterViewModel"/> class.
        /// </summary>
        /// <param name="email">Email.</param>
        /// <param name="password">Password.</param>
        /// <param name="confirmPassword">Confirmation Password.</param>
        public RegisterViewModel(
            string email,
            string password,
            string confirmPassword)
        {
            this.Email = email;
            this.Password = password;
            this.ConfirmPassword = confirmPassword;
        }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the password confirmation.
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare(
            nameof(Password),
            ErrorMessage = "Password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Copies this instance.
        /// </summary>
        /// <returns>Copy.</returns>
        public RegisterViewModel Copy()
        {
            return new RegisterViewModel(
                email: this.Email,
                password: "PASSWORD",
                confirmPassword: this.Password == this.ConfirmPassword ? "MATCH" : "MISMATCH");
        }
    }
}