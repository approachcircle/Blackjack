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
}
