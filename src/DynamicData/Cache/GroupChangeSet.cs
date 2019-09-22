﻿// Copyright (c) 2011-2019 Roland Pheasant. All rights reserved.
// Roland Pheasant licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace DynamicData
{
    internal sealed class GroupChangeSet<TObject, TKey, TGroupKey> : ChangeSet<IGroup<TObject, TKey, TGroupKey>, TGroupKey>, IGroupChangeSet<TObject, TKey, TGroupKey>
    {

        public new static readonly IGroupChangeSet<TObject, TKey, TGroupKey> Empty = new GroupChangeSet<TObject, TKey, TGroupKey>();

        private GroupChangeSet()
        {
        }

        public GroupChangeSet(IEnumerable<IChange<IGroup<TObject, TKey, TGroupKey>, TGroupKey>> items)
            : base(items)
        {
        }
    }
}
