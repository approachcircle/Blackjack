using NUnit.Framework;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;

namespace Blackjack.Game.Tests.Visual;

[TestFixture]
public sealed partial class BlackjackButtonTest : BlackjackTestScene
{
    private BlackjackButton button;
    private FlashingButton flashingButton;
    private SpriteText text;

    [SetUp]
    public void Setup()
    {
        text?.Expire();
        flashingButton?.Expire();
        button?.Expire();
        text = new SpriteText
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Y = 150,
            Text = "test text",
            Font = FontUsage.Default.With(size: 50),
            Alpha = 0f
        };
        button = new BlackjackButton
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Text = "test button",
            Action = () =>
            {
                text.Alpha = text.Alpha.Equals(0) ? 1 : 0;
            }
        };
        flashingButton = new FlashingButton()
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Text = "test button",
            Action = () =>
            {
                text.Alpha = text.Alpha.Equals(0) ? 1 : 0;
            }
        };
        Add(text);
    }

    [Test]
    public void TestButtonBehaviour()
    {
        AddStep("add button", () => Add(button));
        AddAssert("check button is present", () => button.IsPresent);
        AddStep("make text is not present", () => text.Alpha = 0f);
        AddStep("click button", () => button.TriggerClick());
        AddAssert("check text is now present", () => text.IsPresent);
    }

    [Test]
    public void TestFlashingButton()
    {
        AddStep("add button", () => Add(flashingButton));
        AddStep("start flashing button", () => flashingButton.StartFlashing());
        AddAssert("check button is present", () => flashingButton.IsPresent);
        AddStep("make text not present", () => text.Alpha = 0f);
        AddStep("click button", () => flashingButton.TriggerClick());
        AddAssert("check text is now present", () => text.IsPresent);
    }
}
