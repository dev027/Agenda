// <copyright file="BaseDomainModel.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Agenda.Domain.Exceptions;

namespace Agenda.Domain.DomainObjects
{
    /// <summary>
    /// Base model for all models in the Domain project.
    /// </summary>
    public abstract class BaseDomainModel
    {
        /// <summary>
        /// Delegate for additional validation.
        /// </summary>
        /// <returns>List of validation results.</returns>
        protected delegate IEnumerable<ValidationResult> AdditionalValidationDelegate();

        /// <summary>
        /// Validates the specified model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="additionalValidation">Additional Validation method.</param>
        protected static void Validate(
            BaseDomainModel model,
            AdditionalValidationDelegate? additionalValidation = null)
        {
            ValidationContext context = new ValidationContext(
                instance: model,
                serviceProvider: null,
                items: null);
            List<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(
                instance: model,
                validationContext: context,
                validationResults: results,
                validateAllProperties: true);

            IList<ValidationResult> moreResults = additionalValidation == null
                ? new List<ValidationResult>()
                : additionalValidation().ToList();

            if (moreResults.Any())
            {
                results.AddRange(moreResults);
                isValid = false;
            }

            if (isValid)
            {
                return;
            }

            IList<ValidationResultException> exceptions = results
                .Select(r => new ValidationResultException(r))
                .ToList();

            if (exceptions.Count == 1)
            {
                throw exceptions[0];
            }

            throw new AggregateException(exceptions);
        }
    }
}