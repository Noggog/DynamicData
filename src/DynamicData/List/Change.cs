// Copyright (c) 2011-2019 Roland Pheasant. All rights reserved.
// Roland Pheasant licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using DynamicData.Kernel;
#pragma warning disable 1591

// ReSharper disable once CheckNamespace
namespace DynamicData
{
    /// <summary>
    ///   Container to describe a single change to a cache
    /// </summary>
    public sealed class Change<T> : IEquatable<Change<T>>
    {
        /// <summary>
        /// The reason for the change
        /// </summary>
        public ListChangeReason Reason { get; }

        /// <summary>
        /// A single item change
        /// </summary>
        public ItemChange<T> Item { get; }

        /// <summary>
        /// A multiple item change
        /// </summary>
        public RangeChange<T> Range { get; }

        /// <summary>
        /// Gets a value indicating whether the change is a single item change or a range change
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public ChangeType Type => Reason.GetChangeType();

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="Change{T}"/> class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <param name="current">The current.</param>
        /// <param name="index">The index.</param>
        public Change(ListChangeReason reason, T current, int index = -1)
            : this(reason, current, Optional.None<T>(), index)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Change{T}"/> class.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <param name="items">The items.</param>
        /// <param name="index">The index.</param>
        public Change(ListChangeReason reason, IEnumerable<T> items, int index = -1)
        {
            if (reason.GetChangeType() == ChangeType.Item)
            {
                throw new IndexOutOfRangeException("ListChangeReason must be a range type for a range change");
            }

            //ignore this case because WhereReasonsAre removes the index 
            //if (reason== ListChangeReason.RemoveRange && index < 0)
            //        throw new UnspecifiedIndexException("ListChangeReason.RemoveRange should not have an index specified index");

            Reason = reason;
            Item = ItemChange<T>.Empty;
            Range = new RangeChange<T>(items, index);
        }

        /// <summary>
        /// Constructor for ChangeReason.Move
        /// </summary>
        /// <param name="current">The current.</param>
        /// <param name="currentIndex">The CurrentIndex.</param>
        /// <param name="previousIndex">CurrentIndex of the previous.</param>
        /// <exception cref="System.ArgumentException">
        /// CurrentIndex must be greater than or equal to zero
        /// or
        /// PreviousIndex must be greater than or equal to zero
        /// </exception>
        public Change(T current, int currentIndex, int previousIndex)
        {
            if (currentIndex < 0)
            {
                throw new ArgumentException("CurrentIndex must be greater than or equal to zero");
            }

            if (previousIndex < 0)
            {
                throw new ArgumentException("PreviousIndex must be greater than or equal to zero");
            }

            Reason = ListChangeReason.Moved;
            Item = new ItemChange<T>(Reason, current, Optional.None<T>(), currentIndex, previousIndex);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Change{TObject, TKey}" /> struct.
        /// </summary>
        /// <param name="reason">The reason.</param>
        /// <param name="current">The current.</param>
        /// <param name="previous">The previous.</param>
        /// <param name="currentIndex">Value of the current.</param>
        /// <param name="previousIndex">Value of the previous.</param>
        /// <exception cref="ArgumentException">
        /// For ChangeReason.Add, a previous value cannot be specified
        /// or
        /// For ChangeReason.Change, must supply previous value
        /// </exception>
        /// <exception cref="System.ArgumentException">For ChangeReason.Add, a previous value cannot be specified
        /// or
        /// For ChangeReason.Change, must supply previous value</exception>
        public Change(ListChangeReason reason, T current, IOptional<T> previous, int currentIndex = -1, int previousIndex = -1)
        {
            if (reason == ListChangeReason.Add && previous.HasValue)
            {
                throw new ArgumentException("For ChangeReason.Add, a previous value cannot be specified");
            }

            if (reason == ListChangeReason.Replace && !previous.HasValue)
            {
                throw new ArgumentException("For ChangeReason.Change, must supply previous value");
            }

            if (reason == ListChangeReason.Refresh && currentIndex < 0)
            {
                throw new ArgumentException("For ChangeReason.Refresh, must supply and index");
            }

            Reason = reason;
            Item = new ItemChange<T>(Reason, current, previous, currentIndex, previousIndex);
        }

        #endregion

        #region Equality

        /// <inheritdoc />
        public bool Equals(Change<T> other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Reason == other.Reason && Item.Equals(other.Item) && Equals(Range, other.Range);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Change<T>)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)Reason;
                hashCode = (hashCode * 397) ^ Item.GetHashCode();
                hashCode = (hashCode * 397) ^ (Range?.GetHashCode() ?? 0);
                return hashCode;
            }
        }

        public static bool operator ==(Change<T> left, Change<T> right) => Equals(left, right);

        public static bool operator !=(Change<T> left, Change<T> right) => !Equals(left, right);

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return Range != null ? $"{Reason}. {Range.Count} changes"
                : $"{Reason}. Current: {Item.Current}, Previous: {Item.Previous}";
        }
    }
}
