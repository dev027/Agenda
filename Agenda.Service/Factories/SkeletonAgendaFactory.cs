// <copyright file="SkeletonAgendaFactory.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.Linq;
using Agenda.Domain.DomainObjects.AgendaItems;
using Agenda.Domain.ValueObjects.Enums;
using Agenda.Service.Constants;
using Agenda.Utilities.ExtensionMethods.Integer;
using AgendaItemText = Agenda.Service.Factories.SkeletonAgendaFactoryResource;

namespace Agenda.Service.Factories
{
    /// <summary>
    /// Factory for creating a single Skeleton Agenda.
    /// </summary>
    public class SkeletonAgendaFactory
    {
        private IAgendaItem? root;

        private Guid MeetingId { get; set; }

        private IAgendaItem Root
        {
            get
            {
                return this.root ??= new AgendaItem(
                    id: Guid.NewGuid(),
                    meetingId: this.MeetingId,
                    parentId: null,
                    text: string.Empty,
                    elderSiblingId: null,
                    agendaItemType: AgendaItemType.Root,
                    childNumberingType: NumberingType.Number);
            }
        }

        /// <summary>
        /// Creates a skeleton agenda of the specified type.
        /// </summary>
        /// <param name="meetingId">Meeting Id.</param>
        /// <param name="skeletonAgendaType">Type of the skeleton agenda.</param>
        /// <returns>Skeleton Agenda.</returns>
        public IAgendaItem Create(
            Guid meetingId,
            SkeletonAgendaType skeletonAgendaType)
        {
            this.ResetFactory(meetingId);

            return skeletonAgendaType switch
            {
                SkeletonAgendaType.Empty => this.CreateEmptyAgenda(),
                SkeletonAgendaType.BasicInitial => this.CreateBasicInitialAgenda(),
                SkeletonAgendaType.BasicContinuation => this.CreateBasicContinuationAgenda(),
                _ => throw new NotSupportedException()
            };
        }

        private void ResetFactory(Guid meetingId)
        {
            this.MeetingId = meetingId;
            this.root = null;
        }

        private IAgendaItem CreateEmptyAgenda()
        {
            return this.Root;
        }

        private IAgendaItem CreateBasicInitialAgenda()
        {
            this.CreateItem(AgendaItemText.Apologies, AgendaItemType.Apologies);
            this.CreateItem(AgendaItemText.MattersArising);
            this.CreateItem(AgendaItemText.AnyOtherBusiness);
            this.CreateItem(AgendaItemText.DateNextMeeting, AgendaItemType.NextMeetingDate);

            return this.Root;
        }

        private IAgendaItem CreateBasicContinuationAgenda()
        {
            this.CreateItem(AgendaItemText.Apologies, AgendaItemType.Apologies);
            this.CreateItem(AgendaItemText.ApproveMinutes);
            this.CreateItem(AgendaItemText.MattersArising);
            this.CreateItem(AgendaItemText.ActionsArising);
            this.CreateItem(AgendaItemText.AnyOtherBusiness);
            this.CreateItem(AgendaItemText.DateNextMeeting, AgendaItemType.NextMeetingDate);

            return this.Root;
        }

        private void CreateItem(
            string text,
            AgendaItemType agendaItemType = AgendaItemType.Normal)
        {
            Guid? elderSiblingId = this.Root.ChildAgendaItems.LastOrDefault()?.Id;

            AgendaItem item = new AgendaItem(
                id: Guid.NewGuid(),
                meetingId: this.MeetingId,
                parentId: this.Root.Id,
                text: text,
                elderSiblingId: elderSiblingId,
                agendaItemType: agendaItemType,
                childNumberingType: NumberingType.LetterLower);

            this.Root.AddChild(item);
        }
    }
}