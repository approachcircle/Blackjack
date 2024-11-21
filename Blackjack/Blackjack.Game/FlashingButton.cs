using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osuTK;

namespace Blackjack.Game;

public partial class FlashingButton(Vector2? buttonSize = null) : BlackjackButton(buttonSize)
{
    private Container flashingContainer;

    [BackgroundDependencyLoader]
    private void load()
    {
        Add(flashingContainer = new Container
        {
            RelativeSizeAxes = Axes.Both,
            Masking = true,
            CornerRadius = 5,
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Depth = 1.0f,
            Child = new Box
            {
                RelativeSizeAxes = Axes.Both,
                Colour = BackgroundColour,
            }
        });
    }

    public void StartFlashing()
    {
        if (!Settings.FlashingButtons.Value) return;
        Scheduler.Add(() =>
        {
            flashingContainer.ScaleTo(1.5f, 500, Easing.Out)
                .FadeOut(500, Easing.Out)
                .Then(e => e.Delay(500).ScaleTo(1.0f).FadeIn())
                .Finally(_ => StartFlashing());
        });
    }
}
