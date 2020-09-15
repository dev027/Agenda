// <copyright file="Audit.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using Agenda.Data.Dtos;
using Agenda.Domain.DomainObjects.AuditDetails;
using Agenda.Domain.DomainObjects.AuditHeaders;

namespace Agenda.Data.Utilities
{
    /// <summary>
    /// Audit routines.
    /// </summary>
    internal static class Audit
    {
        /// <summary>
        /// Audits the create.
        /// </summary>
        /// <param name="auditHeader">Audit Header.</param>
        /// <param name="newObject">New value.</param>
        /// <param name="recordId">Record Id.</param>
        internal static void AuditCreate(
            IAuditHeaderWithAuditDetails? auditHeader,
            BaseDto newObject,
            Guid recordId)
        {
            if (auditHeader == null)
            {
                return;
            }

            Type type = newObject.GetType();
            string tableName = type.Name; // TODO:[SJW] Assumed table name matches type name

            PropertyDescriptorCollection propertyDescriptors = TypeDescriptor.GetProperties(type);

            foreach (PropertyInfo propertyInfo in type.GetProperties().OrderBy(p => p.MetadataToken))
            {
                if (!PropertyUtilities.IsAuditableColumn(propertyDescriptors, propertyInfo))
                {
                    continue;
                }

                string newValue = propertyInfo.GetGetMethod().Invoke(newObject, null) == null
                    ? string.Empty
                    : propertyInfo.GetGetMethod().Invoke(newObject, null).ToString();

                PropertyDescriptor propertyDescriptor = propertyDescriptors[propertyInfo.Name];
                ColumnAttribute? columnAttribute =
                    (ColumnAttribute?)propertyDescriptor.Attributes[typeof(ColumnAttribute)];
                string columnName = columnAttribute == null
                    ? propertyInfo.Name
                    : columnAttribute.Name;

                IAuditDetail auditDetail = AuditDetail.CreateForCreate(
                    auditHeader: auditHeader,
                    tableName: tableName,
                    columnName: columnName,
                    recordId: recordId,
                    newValue: newValue);

                auditHeader.AuditDetails.Add(auditDetail);
            }
        }

        /// <summary>
        /// Audits the update.
        /// </summary>
        /// <param name="auditHeader">Audit Header.</param>
        /// <param name="recordId">Record Id.</param>
        /// <param name="oldObject">Old Value.</param>
        /// <param name="newObject">New Value.</param>
        internal static void AuditUpdate(
            IAuditHeaderWithAuditDetails? auditHeader,
            Guid recordId,
            BaseDto oldObject,
            BaseDto newObject)
        {
            if (auditHeader == null)
            {
                return;
            }

            Type type = newObject.GetType();
            string tableName = type.Name; // TODO:[SJW] Assumed table name matches type name

            PropertyDescriptorCollection propertyDescriptors = TypeDescriptor.GetProperties(type);

            foreach (PropertyInfo propertyInfo in type.GetProperties().OrderBy(p => p.MetadataToken))
            {
                // Skip if not auditable field
                if (!PropertyUtilities.IsAuditableColumn(propertyDescriptors, propertyInfo))
                {
                    continue;
                }

                // Get old and new values
                string oldValue = propertyInfo.GetGetMethod().Invoke(oldObject, null) == null
                    ? string.Empty
                    : propertyInfo.GetGetMethod().Invoke(oldObject, null).ToString();

                string newValue = propertyInfo.GetGetMethod().Invoke(newObject, null) == null
                    ? string.Empty
                    : propertyInfo.GetGetMethod().Invoke(newObject, null).ToString();

                // Skip if value unchanged
                if (CompareNullable.AreEqual(oldValue, newValue))
                {
                    continue;
                }

                PropertyDescriptor propertyDescriptor = propertyDescriptors[propertyInfo.Name];
                ColumnAttribute? columnAttribute =
                    (ColumnAttribute?)propertyDescriptor.Attributes[typeof(ColumnAttribute)];
                var columnName = columnAttribute == null
                    ? propertyInfo.Name
                    : columnAttribute.Name;

                IAuditDetail auditDetail = AuditDetail.CreateForUpdate(
                    auditHeader: auditHeader,
                    tableName: tableName,
                    columnName: columnName,
                    recordId: recordId,
                    oldValue: oldValue,
                    newValue: newValue);

                auditHeader.AuditDetails.Add(auditDetail);
            }
        }
    }
}