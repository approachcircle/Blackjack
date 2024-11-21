using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace Blackjack.Game;

public partial class FlashingButton : BlackjackButton
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
            Size = ButtonSize,
            Depth = 1.0f
        });
    }

    public void StartFlashing()
    {
        Scheduler.Add(() =>
        {
            flashingBox.ScaleTo(1.5f, 500, Easing.Out)
                .FadeOut(500, Easing.Out)
                .Then(e => e.Delay(500).ScaleTo(1.0f).FadeIn())
                .Finally(_ => StartFlashing());
        });
    }
}
