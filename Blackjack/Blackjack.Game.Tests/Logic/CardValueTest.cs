using Blackjack.Game.Tests.Visual;
using NUnit.Framework;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics;

namespace Blackjack.Game.Tests.Logic;

[TestFixture]
public sealed partial class CardValueTest : BlackjackTestScene
{
    private CardHand cardHand;
    private SpriteText handScore;

    [SetUp]
    public void Setup()
    {
        cardHand?.Expire();
        cardHand = new CardHand(HandOwner.Player);
        Add(cardHand);
        handScore?.Expire();
        handScore = new SpriteText
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Font = FontUsage.Default.With(size: 40),
        };
        cardHand.HandScore.BindValueChanged((e) =>
        {
            handScore.Text = "Hand score: " + e.NewValue;
        }, true);
        Add(handScore);
    }

    [Test]
    public void TestHighAceBehaviour()
    {
        AddStep("add low card", () =>
        {
            cardHand.DrawCard("King");
        });
        AddStep("add ace to player hand", () =>
        {
            cardHand.DrawCard("Ace");
        });
        AddAssert("ensure hand equals prediction", () => cardHand.HandScore.Value == 21);
    }

    [Test]
    public void TestLowAceBehaviour()
    {
        AddStep("add high cards", () =>
        {
            cardHand.DrawCard("King");
            cardHand.DrawCard("King");
        });
        AddStep("add ace to player hand", () =>
        {
            cardHand.DrawCard("Ace");
        });
        AddAssert("ensure hand equals prediction", () => cardHand.HandScore.Value == 21);
    }

    [Test]
    public void TestDynamicAceBehaviour()
    {
        CardHand dealerHand = new CardHand(HandOwner.Dealer);
        dealerHand.DrawCard("Ten");
        dealerHand.DrawCard("Five");
        dealerHand.DrawCard("Four");
        AddStep("check values on each card draw", () =>
        {
            cardHand.OnCardDrawn = () => GameWatcher.Update(cardHand, dealerHand);
        });
        AddStep("add ace", () => cardHand.DrawCard("Ace"));
        AddStep("add nine", () => cardHand.DrawCard("Nine"));
        AddStep("add nine", () => cardHand.DrawCard("Nine"));
        AddAssert("check hand is 19", () => cardHand.HandScore.Value == 19);
    }


    [Test]
    public void TestFiveCardCharlie()
    {
        CardHand dealerHand = new CardHand(HandOwner.Dealer);
        dealerHand.DrawCard("Ten");
        dealerHand.DrawCard("Five");
        dealerHand.DrawCard("Four");
        AddStep("check values on each card draw", () =>
        {
            cardHand.OnCardDrawn = () => GameWatcher.Update(cardHand, dealerHand);
        });
        AddStep("add low cards", () =>
        {
            cardHand.DrawCard("Two");
            cardHand.DrawCard("Two");
            cardHand.DrawCard("Two");
            cardHand.DrawCard("Two");
            cardHand.DrawCard("Two");
        });
        AddAssert("assert player hand state is five card charlie",
            () => cardHand.HandState.Value == HandState.PlayerFiveCardCharlie);
    }

    [Test]
    public void TestFiveCardCharlieWithAces()
    {
        CardHand dealerHand = new CardHand(HandOwner.Dealer);
        dealerHand.DrawCard("Two");
        dealerHand.DrawCard("Two");
        dealerHand.DrawCard("Two");
        AddStep("check values on each card draw", () =>
        {
            cardHand.OnCardDrawn = () => GameWatcher.Update(cardHand, dealerHand);
        });
        AddStep("add low cards and aces", () =>
        {
            cardHand.DrawCard("Two");
            cardHand.DrawCard("Ace");
            cardHand.DrawCard("Three");
            cardHand.DrawCard("Ace");
            cardHand.DrawCard("Two");
        });
        AddAssert("assert player hand state is five card charlie",
            () => cardHand.HandState.Value == HandState.PlayerFiveCardCharlie);
    }
}
