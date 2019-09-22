﻿// Copyright (c) 2011-2019 Roland Pheasant. All rights reserved.
// Roland Pheasant licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using DynamicData.Annotations;
using DynamicData.Kernel;

namespace DynamicData.Cache.Internal
{
    internal class EditDiff<TObject, TKey>
    {
        private readonly ISourceCache<TObject, TKey> _source;
        private readonly Func<TObject, TObject, bool> _areEqual;
        private readonly IEqualityComparer<IKeyValue<TObject, TKey>> _keyComparer = new KeyComparer<TObject, TKey>();

        public EditDiff([NotNull] ISourceCache<TObject, TKey> source, [NotNull] Func<TObject, TObject, bool> areEqual)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _areEqual = areEqual ?? throw new ArgumentNullException(nameof(areEqual));
        }

        public void Edit(IEnumerable<TObject> items)
        {
            _source.Edit(innerCache =>
            {
                var originalItems = innerCache.KeyValues.AsArray();
                var newItems = innerCache.GetKeyValues(items).AsArray();

                var removes = originalItems.Except(newItems, _keyComparer).ToArray();
                var adds = newItems.Except(originalItems, _keyComparer).ToArray();

                //calculate intersect where the item has changed.
                var intersect = newItems
                        .Select(kvp => new { Original = innerCache.Lookup(kvp.Key), NewItem = kvp })
                        .Where(x => x.Original.HasValue && !_areEqual(x.Original.Value, x.NewItem.Value))
                        .Select(x => new KeyValue<TObject, TKey>(x.NewItem.Key, x.NewItem.Value))
                        .Select<KeyValue<TObject, TKey>, IKeyValue<TObject, TKey>>(i => i)
                        .ToArray();

                innerCache.Remove(removes.Select(kvp => kvp.Key));
                innerCache.AddOrUpdate(adds.Union(intersect));
            });
        }
    }
}
