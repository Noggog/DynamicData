// Copyright (c) 2011-2019 Roland Pheasant. All rights reserved.
// Roland Pheasant licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DynamicData
{
    public interface ICache<TObject, TKey> : IReadOnlyDictionary<TKey, TObject>, IReadOnlyCache<TObject, TKey>
    {
        /// <inheritdoc />
        new IEnumerable<TKey> Keys { get; }

        /// <inheritdoc />
        new int Count { get; }

        /// <summary>
        /// Adds or updates the item using the specified key
        /// </summary>
        /// <param name="item">The item.</param>
        void Set(TObject item);

        /// <summary>
        /// Adds or updates the item using the specified key
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="equalityComparer">The equality comparer used to determine whether a new item is the same as an existing cached item</param>
        void Set(TObject item, IEqualityComparer<TObject> equalityComparer);

        /// <summary>
        /// Adds or updates the item using the specified key
        /// </summary>
        /// <param name="items">The items.</param>
        void Set(IEnumerable<TObject> items);

        /// <summary>
        /// Removes the item matching the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(TKey key);

        /// <summary>
        /// Removes the specified key retrieved from the given item.
        /// </summary>
        /// <param name="obj">An object to retrieve a key from to remove</param>
        void Remove(TObject obj);

        /// <summary>
        /// Removes all items matching the specified keys
        /// </summary>
        /// <param name="objects">The items.</param>
        void Remove(IEnumerable<TObject> objects);

        /// <summary>
        /// Removes all items matching the specified keys
        /// </summary>
        /// <param name="keys">The keys.</param>
        void Remove(IEnumerable<TKey> keys);

        /// <summary>
        /// Clears all items
        /// </summary>
        void Clear();

        new bool ContainsKey(TKey key);

        TObject TryCreateValue(TKey key, Func<TKey, TObject> createFunc);

        new IEnumerator<IKeyValue<TObject, TKey>> GetEnumerator();
    }
}
