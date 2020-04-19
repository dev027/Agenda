// <copyright file="MyRequiredAttribute.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using Agenda.Web.Resources;

namespace Agenda.Web.Models.ValidationAttributes
{
    /// <summary>
    /// Custom Required attribute.
    /// </summary>
    /// <seealso cref="RequiredAttribute" />
    public class MyRequiredAttribute : RequiredAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyRequiredAttribute"/> class.
        /// </summary>
        public MyRequiredAttribute()
        {
            this.ErrorMessageResourceType = typeof(ErrorMessageResource);
            this.ErrorMessageResourceName = nameof(ErrorMessageResource.RequiredMessage);
        }
    }
}