using Blackjack.Game.Tests.Visual;
using NUnit.Framework;
using osu.Framework.Screens;

namespace Blackjack.Game.Tests.Logic;

[TestFixture]
public partial class CardValueTest : BlackjackTestScene
{
    private ScreenStack screenStack;
    private MainScreen mainScreen;

    public CardValueTest()
    {
        screenStack = new ScreenStack();
        Add(screenStack);
    }

    [Test]
    public void TestLowAceDoesNotCauseBust()
    {
        AddStep("exit screen if one is present", () =>
        {
            if (screenStack.CurrentScreen is not null) screenStack.Exit();
        });
        AddStep("push new screen", () =>
        {
            screenStack.Push(mainScreen = new MainScreen());
        });
        AddUntilStep("wait for screen to load", () => mainScreen.IsLoaded);
        AddAssert("ensure player is not bust", () => mainScreen.BindableScore.Value < 22);
        AddStep("add ace to player hand", () =>
        {
            mainScreen.OnCardDrawRequest(HandOwner.Player, card: "Ace");
        });
        AddAssert("ensure player is not bust", () => mainScreen.BindableScore.Value < 22);
    }
}
