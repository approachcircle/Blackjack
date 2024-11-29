using System;
using Blackjack.Game.Online;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Threading;
using osuTK;

namespace Blackjack.Game;

public partial class ServerConnectionOverlay(string initialText) : OverlayContainer
{
    private SpriteText overlayText;
    public Bindable<string> OverlayText { get; } = new(initialText);
    // private bool suppressAutomaticHide;
    // private ScheduledDelegate hideDelegate;

    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        Anchor = Anchor.BottomCentre;
        Origin = Anchor.BottomCentre;
        Add(new Box
        {
            Scale = new Vector2(220, 30),
            Colour = Colour4.DimGray,
            Alpha = 0.5f,
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre
        });
        Add(overlayText = new SpriteText
        {
            Colour = Colour4.White,
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Font = FontUsage.Default.With(size: 20),
            Text = initialText
        });
        OverlayText.BindValueChanged(e =>
        {
            overlayText.Text = e.NewValue;
        }, true);
        PopIn();
    }

    public void Complete()
    {
        overlayText.Text = "Connected!";
        Scheduler.AddDelayed(PopOut, 2500);
    }

    public void StateChanged(string stateRepresentation)
    {
        OverlayText.Value = stateRepresentation;
        PopIn();
    }

    protected override void PopIn()
    {
        // this.FadeIn(500);
        Alpha = 1.0f;
    }

    protected override void PopOut()
    {
        this.FadeOut(500, Easing.InQuint);
    }
}
