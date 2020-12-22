// <copyright file="AgendaDataCommittee.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Threading.Tasks;
using Agenda.Data.Dtos;
using Agenda.Data.Utilities;
using Agenda.Domain.DomainObjects.AuditHeaders;
using Agenda.Domain.DomainObjects.Committees;
using Agenda.Utilities.Models.Whos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Agenda.Data.Crud
{
    /// <summary>
    /// CRUD operations for Committees.
    /// </summary>
    public partial class AgendaData
    {
        #region Create

        /// <inheritdoc/>
        public async Task CreateCommitteeAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            ICommittee committee)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, committee) {@who} {@committee}",
                nameof(this.CreateCommitteeAsync),
                who,
                committee);

            CommitteeDto dto = CommitteeDto.ToDto(committee);

            this.context.Committees.Add(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);
            Audit.AuditCreate(auditHeader, dto, dto.Id);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.CreateCommitteeAsync),
                who);
        }

        #endregion Create

        #region Read

        /// <inheritdoc/>
        public async Task<ICommittee> GetCommitteeByIdAsync(
            IWho who,
            Guid committeeId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, committeeId) {@who} {committeeId}",
                nameof(this.GetCommitteeByIdAsync),
                who,
                committeeId);

            ICommittee committee = (await this.context.Committees
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetCommitteeByIdAsync)))
                    .Include(c => c.Organisation)
                    .FirstOrDefaultAsync(c => c.Id == committeeId)
                    .ConfigureAwait(false))
                .ToDomain();

            this.logger.LogTrace(
                "EXIT {Method}(who, committee) {@who} {@committee}",
                nameof(this.GetCommitteeByIdAsync),
                who,
                committee);

            return committee;
        }

        /// <inheritdoc />
        public async Task<ICommitteeWithMeetings> GetCommitteeByIdWithMeetingsAsync(
            IWho who,
            Guid committeeId)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, committeeId) {@who} {committeeId}",
                nameof(this.GetCommitteeByIdWithMeetingsAsync),
                who,
                committeeId);

            ICommitteeWithMeetings committeeWithMeetings = (await this.context.Committees
                    .AsNoTracking()
                    .TagWith(this.Tag(who, nameof(this.GetCommitteeByIdWithMeetingsAsync)))
                    .Include(c => c.Organisation)
                    .Include(c => c.Meetings)
                    .FirstOrDefaultAsync(c => c.Id == committeeId)
                    .ConfigureAwait(false))
                .ToDomainWithMeetings();

            this.logger.LogTrace(
                "EXIT {Method}(who, committeeWithMeetings) {@who} {@committeeWithMeetings}",
                nameof(this.GetCommitteeByIdWithMeetingsAsync),
                who,
                committeeWithMeetings);

            return committeeWithMeetings;
        }

        /// <inheritdoc/>
        public async Task<bool> HaveCommitteesAsync(IWho who)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who) {@who}",
                nameof(this.HaveCommitteesAsync),
                who);

            bool haveCommittees = await this.context.Committees
                .TagWith(this.Tag(who, nameof(this.HaveCommitteesAsync)))
                .AnyAsync()
                .ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who, haveCommittees) {@who} {haveCommittees}",
                nameof(this.HaveCommitteesAsync),
                who,
                haveCommittees);

            return haveCommittees;
        }

        #endregion Read

        #region Update

        /// <inheritdoc/>
        public async Task UpdateCommitteeAsync(
            IWho who,
            IAuditHeaderWithAuditDetails auditHeader,
            ICommittee committee)
        {
            this.logger.LogTrace(
                "ENTRY {Method}(who, committee) {@who} {@committee}",
                nameof(this.UpdateCommitteeAsync),
                who,
                committee);

            CommitteeDto dto = CommitteeDto.ToDto(committee);
            CommitteeDto original = await this.context.FindAsync<CommitteeDto>(committee.Id);
            Audit.AuditUpdate(auditHeader, dto.Id, original, dto);

            this.context.Entry(original).CurrentValues.SetValues(dto);
            await this.context.SaveChangesAsync().ConfigureAwait(false);

            this.logger.LogTrace(
                "EXIT {Method}(who) {@who}",
                nameof(this.UpdateCommitteeAsync),
                who);
        }

        #endregion Update
    }
}