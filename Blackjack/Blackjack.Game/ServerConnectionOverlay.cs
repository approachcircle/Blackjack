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

public partial class ServerConnectionOverlay : OverlayContainer
{
    private SpriteText overlayText;
    public Bindable<string> OverlayText { get; } = new("Nothing to show");
    private bool suppressAutomaticHide;
    private ScheduledDelegate hideDelegate;

    [BackgroundDependencyLoader]
    private void load()
    {
        AutoSizeAxes = Axes.Both;
        Anchor = Anchor.BottomCentre;
        Origin = Anchor.BottomCentre;
        Add(new Box
        {
            Scale = new Vector2(200, 20),
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
        });
        OverlayText.BindValueChanged(e =>
        {
            overlayText.Text = e.NewValue;
            Show();
        });
        APIAccess.Connection.Closed += _ =>
        {
            Scheduler.Add(() =>
            {
                OverlayText.Value = "Disconnected!";
            });
            return null;
        };
        APIAccess.Connection.Reconnecting += _ =>
        {
            Scheduler.Add(() =>
            {
                suppressAutomaticHide = true;
                OverlayText.Value = "Reconnecting...";
            });
            return null;
        };
        APIAccess.Connection.Reconnected += _ =>
        {
            Scheduler.Add(() =>
            {
                OverlayText.Value = "Reconnected!";
            });
            return null;
        };
    }

    public override void Show()
    {
        base.Show();
        if (!suppressAutomaticHide)
        {
            hideDelegate = Scheduler.AddDelayed(Hide, 5000);
        }
        else
        {
            hideDelegate?.Cancel();
        }
        PopIn();
        suppressAutomaticHide = false;
    }

    protected override void PopIn()
    {
        this.FadeInFromZero(1000, Easing.InQuint);
    }

    protected override void PopOut()
    {
        this.FadeOut(1000, Easing.OutQuint);
    }
}
