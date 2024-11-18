using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace Blackjack.Game.Tests.Visual;

[TestFixture]
public sealed partial class BlackjackButtonTest : BlackjackTestScene
{
    private readonly BlackjackButton button;
    private readonly SpriteText text;

    public BlackjackButtonTest()
    {
        button = new BlackjackButton();
        button.Text = "test button";
        text = new SpriteText
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Y = 50,
            Text = "test text",
            Font = FontUsage.Default.With(size: 50),
            Alpha = 0f
        };
        button.Action = () =>
        {
            text.Alpha = text.Alpha.Equals(0) ? 1 : 0;
        };
        Add(button);
        Add(text);
    }

    [Test]
    public void TestButtonBehaviour()
    {
        AddAssert("check button is present", () => button.IsPresent);
        AddStep("make text is not present", () => text.Alpha = 0f);
        AddStep("click button", () => button.TriggerClick());
        AddAssert("check text is now present", () => text.IsPresent);
    }
}
