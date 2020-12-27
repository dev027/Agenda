// <copyright file="AgendaItemType.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

namespace Agenda.Domain.ValueObjects.Enums
{
    /// <summary>
    /// Agenda Item Types.
    /// </summary>
    public enum AgendaItemType
    {
        /// <summary>
        /// Normal
        /// </summary>
        Normal = 0,

        /// <summary>
        /// Root
        /// </summary>
        Root = 1,

        /// <summary>
        /// Apologies
        /// </summary>
        Apologies = 2,

        /// <summary>
        /// Next Meeting Date
        /// </summary>
        NextMeetingDate = 3
    }
}