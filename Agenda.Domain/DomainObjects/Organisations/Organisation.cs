﻿// <copyright file="Organisation.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;

namespace Agenda.Domain.DomainObjects.Organisations
{
    /// <summary>
    /// Organisation.
    /// </summary>
    public class Organisation : IOrganisation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Organisation"/> class.
        /// </summary>
        /// <param name="id">Organisation Id.</param>
        /// <param name="code">Organisation Code.</param>
        /// <param name="name">Organisation Name.</param>
        public Organisation(
            Guid id,
            string code,
            string name)
        {
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException(nameof(code));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            this.Id = id;
            this.Code = code;
            this.Name = name;
        }

        /// <inheritdoc/>
        public Guid Id { get; }

        /// <inheritdoc/>
        public string Code { get; }

        /// <inheritdoc/>
        public string Name { get; }
    }
}