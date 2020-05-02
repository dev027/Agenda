// <copyright file="SelectOption.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;

namespace Agenda.Web.Models
{
    /// <summary>
    /// Select option.
    /// </summary>
    public class SelectOption
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SelectOption"/> class.
        /// </summary>
        /// <param name="value">Value to return.</param>
        /// <param name="display">Text to display.</param>
        public SelectOption(string value, string display)
        {
            this.Value = value;
            this.Display = display;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectOption"/> class.
        /// </summary>
        /// <param name="value">Value to return.</param>
        /// <param name="display">Text to display.</param>
        public SelectOption(Guid value, string display)
        {
            this.Value = value.ToString();
            this.Display = display;
        }

        /// <summary>
        /// Gets the Value to return.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Gets the Text to display.
        /// </summary>
        public string Display { get; }
    }
}