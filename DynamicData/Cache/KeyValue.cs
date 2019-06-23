using System;
using System.Collections.Generic;
using System.Text;

namespace DynamicData
{
    public class KeyValue<TValue, TKey> : IKeyValue<TValue, TKey>
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
    }
}
