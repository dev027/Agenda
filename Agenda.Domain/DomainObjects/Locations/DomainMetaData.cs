// <copyright file="DomainMetaData.cs" company="Do It Wright">
// Copyright (c) Do It Wright. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

namespace Agenda.Domain.DomainObjects.Locations
{
    /// <summary>
    /// Metadata for the <see cref="ILocation"/>.
    /// </summary>
    public static class DomainMetadata
    {
        /// <summary>
        /// Metadata for the <see cref="ILocation.Name"></see> property.
        /// </summary>
        public static class Name
        {
            /// <summary>
            /// The minimum length.
            /// </summary>
            public const int MinLength = 1;

            /// <summary>
            /// The maximum length.
            /// </summary>
            public const int MaxLength = 50;
        }

        /// <summary>
        /// Metadata for the <see cref="ILocation.Address"></see> property.
        /// </summary>
        public static class Address
        {
            /// <summary>
            /// The minimum length.
            /// </summary>
            public const int MinLength = 1;

            /// <summary>
            /// The maximum length.
            /// </summary>
            public const int MaxLength = 100;
        }

        /// <summary>
        /// Metadata for the <see cref="ILocation.What3Words"></see> property.
        /// </summary>
        public static class What3Words
        {
            /// <summary>
            /// The minimum length.
            /// </summary>
            public const int MinLength = 1;

            /// <summary>
            /// The maximum length.
            /// </summary>
            public const int MaxLength = (3 * Part.MaxLength) + 2;

            /// <summary>
            /// The Prefix for W3W addresses.
            /// </summary>
            public const string Prefix = "///";

            /// <summary>
            /// The separator for W3W addresses.
            /// </summary>
            public const string Separator = ".";

            /// <summary>
            /// The W3W part RegEx.
            /// </summary>
            public const string PartRegEx = "^[a-z]{1,10}$";

            /// <summary>
            /// The Part of the W3W address, of which there are three.
            /// </summary>
            [SuppressMessage(
                "ReSharper",
                "MemberHidesStaticFromOuterClass",
                Justification = "Stylecop")]
            public static class Part
            {
                /// <summary>
                /// The minimum length.
                /// </summary>
                public const int MinLength = 1;

                /// <summary>
                /// The maximum length.
                /// </summary>
                public const int MaxLength = 10;
            }
        }

        /// <summary>
        /// Latitude.
        /// </summary>
        public static class Latitude
        {
            /// <summary>
            /// The minimum value.
            /// </summary>
            public const double MinValue = -90;

            /// <summary>
            /// The maximum value.
            /// </summary>
            public const double MaxValue = 90;
        }

        /// <summary>
        /// Longitude.
        /// </summary>
        public static class Longitude
        {
            /// <summary>
            /// The minimum value.
            /// </summary>
            public const double MinValue = -180;

            /// <summary>
            /// The maximum value.
            /// </summary>
            public const double MaxValue = 180;
        }
    }
}