// <copyright file="ValidationResultException.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Agenda.Domain.Exceptions
{
    /// <summary>
    /// ValidationException.
    /// </summary>
    [Serializable]
    public class ValidationResultException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResultException"/> class.
        /// </summary>
        /// <param name="result">Validation result.</param>
        public ValidationResultException(ValidationResult result)
            : base(ErrorMessage(result))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResultException"/> class.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public ValidationResultException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResultException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        [ExcludeFromCodeCoverage]
        public ValidationResultException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResultException"/> class.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference (Nothing in Visual Basic) if no inner exception is specified.</param>
        [ExcludeFromCodeCoverage]
        public ValidationResultException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationResultException"/> class.
        /// </summary>
        /// <param name="info">Serialization Info.</param>
        /// <param name="context">Streaming Context.</param>
        [ExcludeFromCodeCoverage]
        protected ValidationResultException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        private static string ErrorMessage(ValidationResult result)
        {
            return result.ToString();
        }
    }
}