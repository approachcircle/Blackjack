using osu.Framework.Testing;

namespace Blackjack.Game.Tests.Visual
{
    public abstract partial class BlackjackTestScene : TestScene
    {
        protected override ITestSceneTestRunner CreateRunner() => new BlackjackTestSceneTestRunner();

        private partial class BlackjackTestSceneTestRunner : BlackjackGameBase, ITestSceneTestRunner
        {
            private TestSceneTestRunner.TestRunner runner;

            protected override void LoadAsyncComplete()
            {
                base.LoadAsyncComplete();
                Add(runner = new TestSceneTestRunner.TestRunner());
            }

            public void RunTestBlocking(TestScene test) => runner.RunTestBlocking(test);
        }
    }
}