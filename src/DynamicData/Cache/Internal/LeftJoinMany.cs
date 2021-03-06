// Copyright (c) 2011-2019 Roland Pheasant. All rights reserved.
// Roland Pheasant licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using DynamicData.Kernel;

namespace DynamicData.Cache.Internal
{
    internal class LeftJoinMany<TLeft, TLeftKey, TRight, TRightKey, TDestination>
    {
        private readonly IObservable<IChangeSet<TLeft, TLeftKey>> _left;
        private readonly IObservable<IChangeSet<TRight, TRightKey>> _right;
        private readonly Func<TRight, TLeftKey> _rightKeySelector;
        private readonly Func<TLeftKey, TLeft, IGrouping<TRight, TRightKey, TLeftKey>, TDestination> _resultSelector;

        public LeftJoinMany(IObservable<IChangeSet<TLeft, TLeftKey>> left,
            IObservable<IChangeSet<TRight, TRightKey>> right,
            Func<TRight, TLeftKey> rightKeySelector,
            Func<TLeftKey, TLeft, IGrouping<TRight, TRightKey, TLeftKey>, TDestination> resultSelector)
        {
            _left = left ?? throw new ArgumentNullException(nameof(left));
            _right = right ?? throw new ArgumentNullException(nameof(right));
            _rightKeySelector = rightKeySelector ?? throw new ArgumentNullException(nameof(rightKeySelector));
            _resultSelector = resultSelector ?? throw new ArgumentNullException(nameof(resultSelector));
        }

        public IObservable<IChangeSet<TDestination, TLeftKey>> Run()
        {
            var emptyCache = InternalCache<TRight, TRightKey>.Empty;
            var rightGrouped = _right.GroupWithImmutableState(_rightKeySelector);
            return _left.LeftJoin(rightGrouped, grouping => grouping.Key,
                (leftKey, left, grouping) => _resultSelector(leftKey, left, grouping.ValueOr(() =>
                {
                    return new ImmutableGroup<TRight, TRightKey, TLeftKey>(leftKey, emptyCache);
                })));
        }

    }
}