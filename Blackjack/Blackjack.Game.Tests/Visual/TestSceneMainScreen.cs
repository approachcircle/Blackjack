using osu.Framework.Graphics;
using osu.Framework.Screens;
using NUnit.Framework;

namespace Blackjack.Game.Tests.Visual
{
    [TestFixture]
    public sealed partial class TestSceneMainScreen : BlackjackTestScene
    {
        public TestSceneMainScreen()
        {
            Add(new ScreenStack(new MainScreen()) { RelativeSizeAxes = Axes.Both });
        }
    }
}
