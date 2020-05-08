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
            IAuditHeaderWithAuditDetails auditHeader,
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
                ColumnAttribute columnAttribute = (ColumnAttribute)propertyDescriptor.Attributes[typeof(ColumnAttribute)];
                string columnName = columnAttribute == null ? propertyInfo.Name : columnAttribute.Name;

                IAuditDetail auditDetail = AuditDetail.CreateForCreate(
                    auditHeader: auditHeader,
                    tableName: tableName,
                    columnName: columnName,
                    recordId: recordId,
                    newValue: newValue);

                auditHeader.AuditDetails.Add(auditDetail);
            }
        }
    }
}