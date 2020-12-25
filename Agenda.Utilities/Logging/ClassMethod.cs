// <copyright file="ClassMethod.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.IO;

namespace Agenda.Utilities.Logging
{
    /// <summary>
    /// Object containing ClassName, MethodName and LineNumber.
    /// </summary>
    public class ClassMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClassMethod"/> class.
        /// </summary>
        /// <param name="className">ClassName.</param>
        /// <param name="methodName">MethodName.</param>
        /// <param name="lineNumber">Line Number.</param>
        public ClassMethod(
            string className,
            string methodName,
            int lineNumber)
        {
            this.ClassName = Path.GetFileName(className);
            this.MethodName = methodName;
            this.LineNumber = lineNumber;
        }

        /// <summary>
        /// Gets the ClassName.
        /// </summary>
        public string ClassName { get; }

        /// <summary>
        /// Gets the MethodName.
        /// </summary>
        public string MethodName { get; }

        /// <summary>
        /// Gets the Line Number.
        /// </summary>
        public int LineNumber { get; }
    }
}