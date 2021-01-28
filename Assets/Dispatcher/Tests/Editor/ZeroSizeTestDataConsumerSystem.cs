﻿using Unity.Entities;

namespace Dispatcher.Tests.Editor
{
    [DisableAutoCreation]
    internal class ZeroSizeTestDataConsumerSystem : SystemBase
    {
        EntityQuery _query;

        public EntityQuery Query => _query;

        public int Count { get; private set; }

        protected override void OnCreate()
        {
            base.OnCreate();

            _query = GetEntityQuery(ComponentType.ReadOnly<ZeroSizeTestData>());
        }

        protected override void OnStopRunning()
        {
            base.OnStopRunning();

            Count = 0;
        }

        protected override void OnUpdate()
        {
            Count = _query.CalculateEntityCount();
        }
    }
}