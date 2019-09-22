using DynamicData.Cache.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicData
{
    public class KeyValue<TValue, TKey> :
        IKeyValue<TValue, TKey>,
        IEquatable<KeyValue<TValue, TKey>>,
        IEquatable<IKeyValue<TValue, TKey>>,
        IComparer<KeyValue<TValue, TKey>>,
        IComparer<IKeyValue<TValue, TKey>>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public KeyValue()
        {
        }

        public KeyValue(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }

        public bool Equals(KeyValue<TValue, TKey> other)
        {
            if (!EqualityComparer<TKey>.Default.Equals(this.Key, other.Key)) return false;
            if (!EqualityComparer<TValue>.Default.Equals(this.Value, other.Value)) return false;
            return true;
        }

        public bool Equals(IKeyValue<TValue, TKey> other) => KeyValueComparer<TValue, TKey>.Instance.Equals(this, other);

        public int Compare(KeyValue<TValue, TKey> x, KeyValue<TValue, TKey> y)
        {
            return Comparer<TKey>.Default.Compare(x.Key, y.Key);
        }

        public int Compare(IKeyValue<TValue, TKey> x, IKeyValue<TValue, TKey> y)
        {
            return Comparer<TKey>.Default.Compare(x.Key, y.Key);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is KeyValue<TValue, TKey> rhs)) return false;
            return Equals(rhs);
        }

        public override int GetHashCode() => KeyValueComparer<TValue, TKey>.Instance.GetHashCode(this);
    }
}
