// Copyright (c) 2011-2019 Roland Pheasant. All rights reserved.
// Roland Pheasant licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DynamicData.Cache.Internal
{
    internal class KeyValueCollection<TObject, TKey> : IKeyValueCollection<TObject, TKey>
    {
        private readonly IReadOnlyCollection<IKeyValue<TObject, TKey>> _items;

        public KeyValueCollection(IReadOnlyCollection<IKeyValue<TObject, TKey>> items,
                                  IComparer<IKeyValue<TObject, TKey>> comparer,
                                  SortReason sortReason,
                                  SortOptimisations optimisations)
        {
            _items = items ?? throw new ArgumentNullException(nameof(items));
            Comparer = comparer;
            SortReason = sortReason;
            Optimisations = optimisations;
        }

        public KeyValueCollection()
        {
            Optimisations = SortOptimisations.None;
            _items = new List<IKeyValue<TObject, TKey>>();
            Comparer = new KeyValueComparer<TObject, TKey>();
        }

        /// <summary>
        /// Gets the comparer used to peform the sort
        /// </summary>
        /// <value>
        /// The comparer.
        /// </value>
        public IComparer<IKeyValue<TObject, TKey>> Comparer { get; }

        public int Count => _items.Count;

        public IKeyValue<TObject, TKey> this[int index] => _items.ElementAt(index);

        public SortReason SortReason { get; }

        public SortOptimisations Optimisations { get; }

        public IEnumerator<IKeyValue<TObject, TKey>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
