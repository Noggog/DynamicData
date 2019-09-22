// Copyright (c) 2011-2019 Roland Pheasant. All rights reserved.
// Roland Pheasant licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace DynamicData.Cache.Internal
{
    internal class KeyValueComparer<TObject, TKey> : IComparer<IKeyValue<TObject, TKey>>, IEqualityComparer<IKeyValue<TObject, TKey>>
    {
        private readonly IComparer<TObject> _comparer;
        public static readonly KeyValueComparer<TObject, TKey> Instance = new KeyValueComparer<TObject, TKey>();

        public KeyValueComparer(IComparer<TObject> comparer = null)
        {
            _comparer = comparer;
        }

        public int Compare(IKeyValue<TObject, TKey> x, IKeyValue<TObject, TKey> y)
        {
            if (_comparer != null)
            {
                int result = _comparer.Compare(x.Value, y.Value);

                if (result != 0)
                {
                    return result;
                }
            }

            return x.Key.GetHashCode().CompareTo(y.Key.GetHashCode());
        }

        public bool Equals(IKeyValue<TObject, TKey> x, IKeyValue<TObject, TKey> y)
        {
            if (!EqualityComparer<TKey>.Default.Equals(x.Key, y.Key)) return false;
            if (!EqualityComparer<TObject>.Default.Equals(x.Value, y.Value)) return false;
            return true;
        }

        public int GetHashCode(IKeyValue<TObject, TKey> obj)
        {
            var hashCode = -1719135621;
            hashCode = hashCode * -1521134295 + obj.Key.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<TObject>.Default.GetHashCode(obj.Value);
            return hashCode;
        }
    }
}
