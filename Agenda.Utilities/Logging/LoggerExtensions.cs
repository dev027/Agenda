// <copyright file="LoggerExtensions.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Runtime.CompilerServices;
using Agenda.Utilities.Models.Whos;
using Microsoft.Extensions.Logging;

namespace Agenda.Utilities.Logging
{
    /// <summary>
    /// Logger Extensions.
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Reports the entry into a method.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="who">Who details.</param>
        /// <param name="arguments">Arguments to the method.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="lineNumber">The line number.</param>
        public static void ReportEntry(
            this ILogger logger,
            IWho who,
            object? arguments = null,
            [CallerFilePath] string className = "Unknown",
            [CallerMemberName] string methodName = "Unknown",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (!logger.IsEnabled(LogLevel.Trace))
            {
                return;
            }

            ClassMethod classMethod = new ClassMethod(className, methodName, lineNumber);

            logger.LogTrace(
                ResourceMessageTemplates.ReportEntry,
                "Entry",
                arguments,
                who,
                classMethod);
        }

        /// <summary>
        /// Reports the entry into a method.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="who">Who details.</param>
        /// <param name="returnArgs">Return arguments from the method.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="lineNumber">The line number.</param>
        public static void ReportExit(
            this ILogger logger,
            IWho who,
            object? returnArgs = null,
            [CallerFilePath] string className = "Unknown",
            [CallerMemberName] string methodName = "Unknown",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (!logger.IsEnabled(LogLevel.Trace))
            {
                return;
            }

            ClassMethod classMethod = new ClassMethod(className, methodName, lineNumber);

            logger.LogTrace(
                ResourceMessageTemplates.ReportExit,
                "Exit",
                returnArgs,
                who,
                classMethod);
        }

        /// <summary>
        /// Reports the entry into a method.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="who">Who details.</param>
        /// <param name="viewName">Name of the View.</param>
        /// <param name="model">View Model.</param>
        /// <param name="statusCode">Status Code.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="lineNumber">The line number.</param>
        public static void ReportExitView(
            this ILogger logger,
            IWho who,
            string? viewName,
            object? model,
            int? statusCode,
            [CallerFilePath] string className = "Unknown",
            [CallerMemberName] string methodName = "Unknown",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (!logger.IsEnabled(LogLevel.Trace))
            {
                return;
            }

            ClassMethod classMethod = new ClassMethod(className, methodName, lineNumber);

            logger.LogTrace(
                ResourceMessageTemplates.ReportExitView,
                "Exit",
                viewName ?? classMethod.MethodName,
                model,
                statusCode,
                who,
                classMethod);
        }

        /// <summary>
        /// Reports the entry into a method.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="who">Who details.</param>
        /// <param name="controllerName">Name of the Controller.</param>
        /// <param name="actionName">Name of the Action.</param>
        /// <param name="model">View Model.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="lineNumber">The line number.</param>
        public static void ReportExitRedirectToAction(
            this ILogger logger,
            IWho who,
            string? controllerName,
            string? actionName,
            object? model,
            [CallerFilePath] string className = "Unknown",
            [CallerMemberName] string methodName = "Unknown",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (!logger.IsEnabled(LogLevel.Trace))
            {
                return;
            }

            ClassMethod classMethod = new ClassMethod(className, methodName, lineNumber);

            logger.LogTrace(
                ResourceMessageTemplates.ReportExitRedirectToAction,
                "Exit",
                controllerName ?? classMethod.ClassName,
                actionName ?? classMethod.MethodName,
                model,
                who,
                classMethod);
        }

        /// <summary>
        /// Log a debug message.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="who">Who details.</param>
        /// <param name="message">Debug message.</param>
        /// <param name="data">Relevant data.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="lineNumber">The line number.</param>
        public static void Debug(
            this ILogger logger,
            IWho who,
            string message,
            object? data = null,
            [CallerFilePath] string className = "Unknown",
            [CallerMemberName] string methodName = "Unknown",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (!logger.IsEnabled(LogLevel.Debug))
            {
                return;
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            ClassMethod classMethod = new ClassMethod(className, methodName, lineNumber);

            logger.LogDebug(
                ResourceMessageTemplates.Debug,
                message,
                data,
                who,
                classMethod);
        }

        /// <summary>
        /// Log a warning message.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="who">Who details.</param>
        /// <param name="message">Warning message.</param>
        /// <param name="data">Relevant data.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="lineNumber">The line number.</param>
        public static void Warning(
            this ILogger logger,
            IWho who,
            string message,
            object? data = null,
            [CallerFilePath] string className = "Unknown",
            [CallerMemberName] string methodName = "Unknown",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (!logger.IsEnabled(LogLevel.Warning))
            {
                return;
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            ClassMethod classMethod = new ClassMethod(className, methodName, lineNumber);

            logger.LogDebug(
                ResourceMessageTemplates.Warning,
                message,
                data,
                who,
                classMethod);
        }

        /// <summary>
        /// Log an error message.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="who">Who details.</param>
        /// <param name="message">Error message.</param>
        /// <param name="data">Relevant data.</param>
        /// <param name="className">Name of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="lineNumber">The line number.</param>
        public static void Error(
            this ILogger logger,
            IWho who,
            string message,
            object? data = null,
            [CallerFilePath] string className = "Unknown",
            [CallerMemberName] string methodName = "Unknown",
            [CallerLineNumber] int lineNumber = 0)
        {
            if (!logger.IsEnabled(LogLevel.Error))
            {
                return;
            }

            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            ClassMethod classMethod = new ClassMethod(className, methodName, lineNumber);

            logger.LogDebug(
                ResourceMessageTemplates.Error,
                message,
                data,
                who,
                classMethod);
        }
    }
}