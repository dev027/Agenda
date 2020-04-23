// <copyright file="IAgendaService.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Threading.Tasks;
using Agenda.Domain.ValueObjects.SetupStatii;
using Agenda.Utilities.Models.Whos;

namespace Agenda.Service
{
    /// <summary>
    /// Service Layer.
    /// </summary>
    public partial interface IAgendaService
    {
        /// <summary>
        /// Gets the setup status.
        /// </summary>
        /// <param name="who">Who Details.</param>
        /// <returns>Setup status.</returns>
        Task<ISetupStatus> GetSetupStatusAsync(
            IWho who);
    }
}