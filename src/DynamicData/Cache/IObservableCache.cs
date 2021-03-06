// Copyright (c) 2011-2019 Roland Pheasant. All rights reserved.
// Roland Pheasant licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using DynamicData.Kernel;
// ReSharper disable once CheckNamespace
namespace DynamicData
{
    /// <summary>
    /// A cache for observing and querying in memory data
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public interface IConnectableCache<TObject, TKey>
    {
        /// <summary>
        /// Returns an observable of any changes which match the specified key.  The sequence starts with the inital item in the cache (if there is one).
        /// </summary>
        /// <param name="key">The key.</param>
        IObservable<Change<TObject, TKey>> Watch(TKey key);

        /// <summary>
        /// Returns a filtered stream of cache changes preceded with the initial filtered state
        /// </summary>
        /// <param name="predicate">The result will be filtered using the specified predicate.</param>
        IObservable<IChangeSet<TObject, TKey>> Connect(Func<TObject, bool> predicate = null);

        /// <summary>
        /// Returns a filtered stream of cache changes.
        /// Unlike Connect(), the returned observable is not prepended with the caches initial items.
        /// </summary>
        /// <param name="predicate">The result will be filtered using the specified predicate.</param>
        IObservable<IChangeSet<TObject, TKey>> Preview(Func<TObject, bool> predicate = null);

        /// <summary>
        /// A count changed observable starting with the current count
        /// </summary>
        IObservable<int> CountChanged { get; }
    }

    /// <summary>
    ///   /// A cache for observing and querying in memory data. With additional data access operators
    /// </summary>
    /// <typeparam name="TObject">The type of the object.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    public interface IObservableCache<TObject, TKey> : IConnectableCache<TObject, TKey>, IDisposable, IReadOnlyCache<TObject, TKey>
    {
        /// <summary>
        /// Gets the Items
        /// </summary>
        IEnumerable<TObject> Items { get; }

        /// <summary>
        /// Gets the key value pairs
        /// </summary>
        IEnumerable<KeyValuePair<TKey, TObject>> KeyValues { get; }

        /// <summary>
        /// Lookup a single item using the specified key.
        /// </summary>
        /// <remarks>
        /// Fast indexed lookup
        /// </remarks>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        Optional<TObject> Lookup(TKey key);

        /// <summary>
        /// The total count of cached items
        /// </summary>
        new int Count { get; }
    }
}
