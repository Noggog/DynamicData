// Copyright (c) 2011-2019 Roland Pheasant. All rights reserved.
// Roland Pheasant licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using DynamicData.Kernel;

namespace DynamicData.List.Internal
{
    internal readonly struct UnifiedChange<T> : IEquatable<UnifiedChange<T>>
    {
        public ListChangeReason Reason { get; }
        public T Current { get; }
        public IOptional<T> Previous { get; }

        public UnifiedChange(ListChangeReason reason, T current)
            : this(reason, current, Optional.None<T>())
        {
        }

        public UnifiedChange(ListChangeReason reason, T current, IOptional<T> previous)
        {
            Reason = reason;
            Current = current;
            Previous = previous;
        }

        #region Equality

        public bool Equals(UnifiedChange<T> other)
        {
            return Reason == other.Reason && EqualityComparer<T>.Default.Equals(Current, other.Current) && Previous.Equals(other.Previous);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is UnifiedChange<T> && Equals((UnifiedChange<T>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int)Reason;
                hashCode = (hashCode * 397) ^ EqualityComparer<T>.Default.GetHashCode(Current);
                hashCode = (hashCode * 397) ^ Previous.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(UnifiedChange<T> left, UnifiedChange<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(UnifiedChange<T> left, UnifiedChange<T> right)
        {
            return !left.Equals(right);
        }

        #endregion

        public override string ToString()
        {
            return $"Reason: {Reason}, Current: {Current}, Previous: {Previous}";
        }
    }
}
