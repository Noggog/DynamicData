// Copyright (c) 2011-2019 Roland Pheasant. All rights reserved.
// Roland Pheasant licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Collections;
using System.Collections.Generic;

namespace DynamicData.List.Internal
{
    internal sealed class AnonymousObservableList<T> : IObservableList<T>
    {
        private readonly ISourceList<T> _sourceList;

        public AnonymousObservableList(IObservable<IChangeSet<T>> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            _sourceList = new SourceList<T>(source);
        }

        public AnonymousObservableList(ISourceList<T> sourceList)
        {
            _sourceList = sourceList ?? throw new ArgumentNullException(nameof(sourceList));
        }

        public T this[int index] => ((IObservableList<T>)_sourceList)[index];

        public IObservable<int> CountChanged => _sourceList.CountChanged;

        public IEnumerable<T> Items => _sourceList.Items;

        public int Count => _sourceList.Count;

        public IObservable<IChangeSet<T>> Connect(Func<T, bool> predicate = null) => _sourceList.Connect(predicate);

        public IObservable<IChangeSet<T>> Preview(Func<T, bool> predicate = null) => _sourceList.Preview(predicate);

        public void Dispose() => _sourceList.Dispose();

        public IEnumerator<T> GetEnumerator() => _sourceList.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _sourceList.GetEnumerator();
    }
}
