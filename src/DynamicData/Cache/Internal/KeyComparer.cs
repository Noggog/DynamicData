// Copyright (c) 2011-2019 Roland Pheasant. All rights reserved.
// Roland Pheasant licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace DynamicData.Cache.Internal
{
    internal sealed class KeyComparer<TObject, TKey> : IEqualityComparer<IKeyValue<TObject, TKey>>
    {
        public bool Equals(IKeyValue<TObject, TKey> x, IKeyValue<TObject, TKey> y)
        {
            return x.Key.Equals(y.Key);
        }

        public int GetHashCode(IKeyValue<TObject, TKey> obj)
        {
            return obj.Key.GetHashCode();
        }
    }
}
