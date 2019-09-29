// Copyright (c) 2011-2019 Roland Pheasant. All rights reserved.
// Roland Pheasant licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DynamicData
{
    public interface ICache<TObject, TKey> : IQuery<TObject, TKey>, IReadOnlyDictionary<TKey, TObject>, IReadOnlyCache<TObject, TKey>
    {
        new IEnumerable<TKey> Keys { get; }
        new int Count { get; }
        new IEnumerable<TObject> Items { get; }

        /// <summary>
        /// Clones the cache from the specified changes
        /// </summary>
        /// <param name="changes">The changes.</param>
        void Clone(IChangeSet<TObject, TKey> changes);

        /// <summary>
        /// Adds or updates the item using the specified key
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="key">The key.</param>
        void AddOrUpdate(TObject item, TKey key);

        /// <summary>
        /// Removes the item matching the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        void Remove(TKey key);

        /// <summary>
        /// Removes all items matching the specified keys
        /// </summary>
        void Remove(IEnumerable<TKey> keys);

        /// <summary>
        /// Clears all items
        /// </summary>
        void Clear();

        /// <summary>
        /// Sends a signal for operators to recalculate it's state 
        /// </summary>
        void Refresh();

        /// <summary>
        /// Refreshes the items matching the specified keys
        /// </summary>
        /// <param name="keys">The keys.</param>
        void Refresh(IEnumerable<TKey> keys);

        /// <summary>
        /// Refreshes the item matching the specified key
        /// </summary>
        void Refresh(TKey key);

        new bool ContainsKey(TKey key);

        new IEnumerator<IKeyValue<TObject, TKey>> GetEnumerator();
    }
}
