using osu.Framework.Allocation;
using osu.Framework.Platform;
using NUnit.Framework;

namespace Blackjack.Game.Tests.Visual
{
    [TestFixture]
    public partial class TestSceneBlackjackGame : BlackjackTestScene
    {
        // Add visual tests to ensure correct behaviour of your game: https://github.com/ppy/osu-framework/wiki/Development-and-Testing
        // You can make changes to classes associated with the tests and they will recompile and update immediately.

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