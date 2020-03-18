// <copyright file="InstanceFactoryException.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Runtime.Serialization;

namespace Agenda.Utilities.Exceptions
{
    /// <summary>
    /// InstanceFactoryException.
    /// </summary>
    [Serializable]
    public class InstanceFactoryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceFactoryException"/> class.
        /// </summary>
        public InstanceFactoryException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceFactoryException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public InstanceFactoryException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceFactoryException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">InnerException.</param>
        public InstanceFactoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InstanceFactoryException"/> class.
        /// </summary>
        /// <param name="info">Serialization Info.</param>
        /// <param name="context">Streaming Context.</param>
        protected InstanceFactoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}