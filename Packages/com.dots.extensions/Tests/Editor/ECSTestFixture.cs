using NUnit.Framework;
using Unity.Entities;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine.LowLevel;

namespace DOTS.Extensions.Tests.Editor
{
    [TestFixture]
    public abstract class ECSTestFixture
    {
        [SetUp]
        public virtual void Setup()
        {
            // unit tests preserve the current player loop to restore later, and start from a blank slate.
            _previousPlayerLoop = PlayerLoop.GetCurrentPlayerLoop();
            PlayerLoop.SetPlayerLoop(PlayerLoop.GetDefaultPlayerLoop());

            _previousWorld = World.DefaultGameObjectInjectionWorld;
            World = World.DefaultGameObjectInjectionWorld = DefaultWorldInitialization.Initialize("Test World", true);
            EntityManager = World.EntityManager;

            // Many ECS tests will only pass if the Jobs Debugger enabled;
            // force it enabled for all tests, and restore the original value at teardown.
            _jobsDebuggerWasEnabled = JobsUtility.JobDebuggerEnabled;
            JobsUtility.JobDebuggerEnabled = true;
        }

        [TearDown]
        public virtual void TearDown()
        {
            if (World != null && World.IsCreated)
            {
                EntityManager.CompleteAllJobs();

                // Clean up systems before calling CheckInternalConsistency because we might have filters etc
                // holding on SharedComponentData making checks fail
                while (World.Systems.Count > 0) World.DestroySystem(World.Systems[0]);

                World.Dispose();
                World = null;

                World.DefaultGameObjectInjectionWorld = _previousWorld;
                _previousWorld = null;
                EntityManager = default;
            }

            JobsUtility.JobDebuggerEnabled = _jobsDebuggerWasEnabled;

            PlayerLoop.SetPlayerLoop(_previousPlayerLoop);
        }

        private World _previousWorld;
        private PlayerLoopSystem _previousPlayerLoop;
        private bool _jobsDebuggerWasEnabled;

        protected World World { get; private set; }

        protected EntityManager EntityManager { get; private set; }
    }
}