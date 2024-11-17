using osu.Framework.Allocation;
using osu.Framework.Platform;
using NUnit.Framework;

namespace Blackjack.Game.Tests.Visual
{
    [TestFixture]
    public partial class TestSceneBlackjackGame : BlackjackTestScene
    {
        private BlackjackGame game;

        [BackgroundDependencyLoader]
        private void load(GameHost host)
        {
            game = new BlackjackGame();
            game.SetHost(host);

            AddGame(game);
        }
    }
}
