// <copyright file="MyStringLengthAttribute.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using Agenda.Web.Resources;

namespace Agenda.Web.Models
{
    /// <summary>
    /// Custom StringLength attribute.
    /// </summary>
    /// <seealso cref="StringLengthAttribute" />
    public class MyStringLengthAttribute : StringLengthAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyStringLengthAttribute"/> class.
        /// </summary>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="minLength">The minimum length.</param>
        public MyStringLengthAttribute(
            int maxLength, int minLength = 0)
        : base(maxLength)
        {
            this.MinimumLength = minLength;
            this.ErrorMessageResourceType = typeof(ErrorMessageResource);
            this.ErrorMessageResourceName = nameof(ErrorMessageResource.StringLengthMessage);
        }
    }
}