// <copyright file="FormState.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

namespace Agenda.Web.Models
{
    /// <summary>
    /// The submit status of a form.
    /// </summary>
    public enum FormState
    {
        /// <summary>
        /// The initial state for new forms.
        /// </summary>
        Initial = 0,

        /// <summary>
        /// The submitted by the Submit Button.
        /// </summary>
        SubmittedBySubmitButton = 1,

        /// <summary>
        /// The submitted by AJAX,
        /// </summary>
        SubmittedByAjax = 2
    }
}