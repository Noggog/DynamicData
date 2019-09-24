// Copyright (c) 2011-2019 Roland Pheasant. All rights reserved.
// Roland Pheasant licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DynamicData
{
    /// <summary>
    /// An editable observable list, providing  observable methods
    /// as well as data access methods
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISourceList<T> : IObservableList<T>, IList<T>
    {
        /// <summary>
        /// Edit the inner list within the list's internal locking mechanism
        /// </summary>
        /// <param name="updateAction">The update action.</param>
        void Edit(Action<IExtendedList<T>> updateAction);

        /// <summary>
        /// Gets the count.
        /// </summary>
        new int Count { get; }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">index is not a valid index in the ISourceList`1.</exception>
        /// <exception cref="System.NotSupportedException">The property is set and the ISourceList`1 is read-only.</exception>
        new T this[int index] { get; set; }
    }
}
