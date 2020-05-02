// <copyright file="MeetingDto.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Agenda.Data.DbContexts;
using Agenda.Data.Resources;
using Agenda.Domain.DomainObjects.Meetings;

namespace Agenda.Data.Dtos
{
    /// <summary>
    /// Organiser DTO.
    /// </summary>
    [Table(nameof(DataContext.Meetings))]
    public class MeetingDto
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingDto"/> class.
        /// </summary>
        public MeetingDto()
        {
            this.Committee = null!;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingDto"/> class.
        /// </summary>
        /// <param name="id">Meeting Id.</param>
        /// <param name="committeeId">Committee Id.</param>
        /// <param name="locationId">Location.</param>
        /// <param name="meetingDateTime">Date and Time of the Meeting.</param>
        public MeetingDto(
            Guid id,
            Guid committeeId,
            Guid locationId,
            DateTime meetingDateTime)
        {
            this.Id = id;
            this.CommitteeId = committeeId;
            this.LocationId = locationId;
            this.MeetingDateTime = meetingDateTime;

            this.Committee = null!;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MeetingDto"/> class.
        /// </summary>
        /// <param name="id">Meeting Id.</param>
        /// <param name="committeeId">Committee Id.</param>
        /// <param name="locationId">Location Id.</param>
        /// <param name="meetingDateTime">Date and Time of the Meeting.</param>
        /// <param name="committee">Committee.</param>
        /// <param name="location">Location.</param>
        public MeetingDto(
            Guid id,
            Guid committeeId,
            Guid locationId,
            DateTime meetingDateTime,
            CommitteeDto committee,
            LocationDto location)
        {
            this.Id = id;
            this.CommitteeId = committeeId;
            this.MeetingDateTime = meetingDateTime;
            this.LocationId = locationId;
            this.Committee = committee;
            this.Location = location;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the Meeting Id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets the Committee Id.
        /// </summary>
        public Guid CommitteeId { get; private set; }

        /// <summary>
        /// Gets the Date and Time of the Meeting.
        /// </summary>
        [Required]
        public DateTime MeetingDateTime { get; private set; }

        /// <summary>
        /// Gets the Location.
        /// </summary>
        [Required]
        public Guid LocationId { get; private set; }

        #endregion Properties

        #region Parent Properties

        /// <summary>
        /// Gets the Committee.
        /// </summary>
        [ForeignKey(nameof(CommitteeId))]
        public CommitteeDto? Committee { get; private set; } = null!;

        /// <summary>
        /// Gets the Location.
        /// </summary>
        [ForeignKey(nameof(LocationId))]
        public LocationDto? Location { get; private set; } = null!;

        #endregion Parent Properties

        #region Public Properties

        /// <summary>
        /// Converts domain object to DTO.
        /// </summary>
        /// <param name="meeting">Meeting.</param>
        /// <returns>Organisation DTO.</returns>
        public static MeetingDto ToDto(IMeeting meeting)
        {
            if (meeting == null)
            {
                throw new ArgumentNullException(nameof(meeting));
            }

            return new MeetingDto(
                id: meeting.Id,
                committeeId: meeting.Committee.Id,
                locationId: meeting.Location.Id,
                meetingDateTime: meeting.MeetingDateTime);
        }

        /// <summary>
        /// Converts instance to domain object.
        /// </summary>
        /// <returns>Meeting.</returns>
        public IMeeting ToDomain()
        {
            if (this.Committee == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ExceptionResource.CannotConvertTo___If___IsNull,
                        nameof(IMeeting),
                        nameof(this.Committee)));
            }

            if (this.Location == null)
            {
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.InvariantCulture,
                        ExceptionResource.CannotConvertTo___If___IsNull,
                        nameof(IMeeting),
                        nameof(this.Location)));
            }

            return new Meeting(
                id: this.Id,
                committee: this.Committee.ToDomain(),
                location: this.Location.ToDomain(),
                meetingDateTime: this.MeetingDateTime);
        }

        #endregion Public Properties
    }
}