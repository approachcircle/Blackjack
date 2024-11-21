using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace Blackjack.Game;

public partial class FlashingButton(Vector2? buttonSize = null) : BlackjackButton(buttonSize)
{
    private Box flashingBox;

    [BackgroundDependencyLoader]
    private void load()
    {
        Add(flashingBox = new Box
        {
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Colour = BackgroundColour,
            Depth = 1.0f,
            Size = buttonSize ?? DefaultButtonSize
        });
    }

    public void StartFlashing()
    {
        if (!Settings.FlashingButtons.Value) return;
        Scheduler.Add(() =>
        {
            flashingBox.ScaleTo(1.5f, 500, Easing.Out)
                .FadeOut(500, Easing.Out)
                .Then(e => e.Delay(500).ScaleTo(1.0f).FadeIn())
                .Finally(_ => StartFlashing());
        });
    }
}
