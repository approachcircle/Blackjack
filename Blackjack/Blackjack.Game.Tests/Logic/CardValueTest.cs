using Blackjack.Game.Tests.Visual;
using NUnit.Framework;
using osu.Framework.Screens;

namespace Blackjack.Game.Tests.Logic;

[TestFixture]
public partial class CardValueTest : BlackjackTestScene
{
    private MainScreen mainScreen;

    public CardValueTest()
    {
        mainScreen = new MainScreen();
        Add(new ScreenStack(mainScreen));
    }

    [Test]
    public void TestLowAceDoesNotCauseBust()
    {
        AddUntilStep("wait for screen to load", () => mainScreen.IsLoaded);
        AddAssert("ensure player is not bust", () => mainScreen.BindableScore.Value < 21);
        AddStep("add ace to player hand", () =>
        {
            mainScreen.OnCardDrawRequest(HandOwner.Player, card: "Ace");
        });
        AddAssert("ensure player is not bust", () => mainScreen.BindableScore.Value < 21);
    }
}
