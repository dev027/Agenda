// <copyright file="AgendaData.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using Agenda.Data.DbContexts;
using Agenda.Utilities.Models.Whos;

namespace Agenda.Data.Crud
{
    /// <summary>
    /// Data access.
    /// </summary>
    /// <seealso cref="IAgendaData"/>
    public partial class AgendaData : IAgendaData
    {
        private readonly DataContext context = new DataContext();

        private bool disposedValue; // To detect redundant calls

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    // Dispose managed state (managed objects).
                    this.context.Dispose();
                }

                this.disposedValue = true;
            }
        }

        /// <summary>
        /// Returns tag for use with .TagWith().
        /// </summary>
        /// <param name="who">Who details.</param>
        /// <param name="methodName">Method Name.</param>
        /// <returns>Tag.</returns>
        protected string Tag(IWho who, string methodName)
        {
            if (who == null)
            {
                throw new ArgumentNullException(nameof(who));
            }

            return $"{this.GetType().Name}.{methodName}.{who.CorrelationId}";
        }
    }
}