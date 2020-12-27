// <copyright file="AgendaItemDto.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using Agenda.Domain.DomainObjects.AgendaItems;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Utilities.ExtensionMethods.Integer;
using Microsoft.EntityFrameworkCore;

namespace Agenda.Data.Dtos
{
    /// <summary>
    /// Agenda Item DTO.
    /// </summary>
    public class AgendaItemDto : BaseDto
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AgendaItemDto"/> class.
        /// </summary>
        public AgendaItemDto()
        {
            this.Text = null!;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgendaItemDto"/> class.
        /// </summary>
        /// <param name="id">Agenda Item Id.</param>
        /// <param name="meetingId">Meeting Id.</param>
        /// <param name="parentId">Parent Agenda Item Id.</param>
        /// <param name="elderSiblingId">Elder Sibling Agenda Item Id.</param>
        /// <param name="text">Text.</param>
        /// <param name="agendaItemType">Agenda Item Type.</param>
        /// <param name="childNumberingType">Child Numbering Type.</param>
        public AgendaItemDto(
            Guid id,
            Guid meetingId,
            Guid? parentId,
            Guid? elderSiblingId,
            string text,
            AgendaItemType agendaItemType,
            NumberingType childNumberingType)
        {
            this.Id = id;
            this.MeetingId = meetingId;
            this.ParentId = parentId;
            this.ElderSiblingId = elderSiblingId;
            this.Text = text;
            this.AgendaItemType = agendaItemType;
            this.ChildNumberingType = childNumberingType;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Agenda Item Id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the Meeting Id.
        /// </summary>
        public Guid MeetingId { get; private set; }

        /// <summary>
        /// Gets the Parent Agenda Item Id.
        /// </summary>
        public Guid? ParentId { get; private set; }

        /// <summary>
        /// Gets the Elder Sibling Agenda Item Id.
        /// </summary>
        public Guid? ElderSiblingId { get; private set; }

        /// <summary>
        /// Gets the Text.
        /// </summary>
        [Required]
        public string Text { get; private set; }

        /// <summary>
        /// Gets the Agenda Item Type.
        /// </summary>
        public AgendaItemType AgendaItemType { get; private set; }

        /// <summary>
        /// Gets the Child Numbering Type.
        /// </summary>
        public NumberingType ChildNumberingType { get; private set; }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Called when [model creating].
        /// </summary>
        /// <param name="builder">The builder.</param>
        public static void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AgendaItemDto>(
                e =>
                {
                    e.HasOne<MeetingDto>()
                        .WithMany()
                        .HasForeignKey(x => x.MeetingId);

                    e.HasOne<AgendaItemDto>()
                        .WithMany()
                        .HasForeignKey(x => x.ParentId);

                    e.HasOne<AgendaItemDto>()
                        .WithMany()
                        .HasForeignKey(x => x.ElderSiblingId);

                    e.Property(p => p.ChildNumberingType)
                        .HasMaxLength(20)
                        .HasConversion<string>();

                    e.Property(p => p.AgendaItemType)
                        .HasMaxLength(20)
                        .HasConversion<string>();
                });
        }

        /// <summary>
        /// Converts domain object to DTO.
        /// </summary>
        /// <param name="agendaItem">Agenda Item.</param>
        /// <returns>Agenda Item DTO.</returns>
        public static AgendaItemDto ToDto(IAgendaItem agendaItem)
        {
            if (agendaItem == null)
            {
                throw new ArgumentNullException(nameof(agendaItem));
            }

            return new AgendaItemDto(
                id: agendaItem.Id,
                meetingId: agendaItem.MeetingId,
                parentId: agendaItem.ParentId,
                elderSiblingId: agendaItem.ElderSiblingId,
                text: agendaItem.Text,
                agendaItemType: agendaItem.AgendaItemType,
                childNumberingType: agendaItem.ChildNumberingType);
        }

        /// <summary>
        /// Converts instance to domain object.
        /// </summary>
        /// <returns>Agenda Item.</returns>
        public IAgendaItem ToDomain()
        {
            return new AgendaItem(
                id: this.Id,
                meetingId: this.MeetingId,
                parentId: this.ParentId,
                text: this.Text,
                elderSiblingId: this.ElderSiblingId,
                agendaItemType: this.AgendaItemType,
                childNumberingType: this.ChildNumberingType);
        }

        #endregion Public Methods
    }
}