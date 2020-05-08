// <copyright file="AuditIgnoreAttribute.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;

namespace Agenda.Data.Attributes
{
    /// <summary>
    /// Marks a property on a DTO to be excluded from the Audit.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class AuditIgnoreAttribute : Attribute
    {
    }
}