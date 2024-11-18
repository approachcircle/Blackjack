using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Effects;
using osu.Framework.Graphics.Sprites;
using osuTK;
using Box = osu.Framework.Graphics.Shapes.Box;

namespace Blackjack.Game;

public partial class GameEndOverlay(Colour4 overlayColour, string overlayText = "") : OverlayContainer
{
    [BackgroundDependencyLoader]
    private void load()
    {
        Masking = true;
        EdgeEffect = new EdgeEffectParameters
        {
            Type = EdgeEffectType.Glow,
            Colour = overlayColour.Opacity(0.2f),
            Radius = 40f,
        };
        AutoSizeAxes = Axes.Y;
        RelativeSizeAxes = Axes.X;
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        Add(new Box
        {
            RelativeSizeAxes = Axes.X,
            Scale = new Vector2(1.0f, 75),
            Colour = overlayColour,
            Alpha = 0.9f,
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre
        });
        Add(new SpriteText
        {
            Colour = Colour4.White,
            Anchor = Anchor.Centre,
            Origin = Anchor.Centre,
            Text = overlayText,
            Font = FontUsage.Default.With(size: 40),
        });
    }

    protected override void PopIn()
    {
        this.ScaleTo(1f, 400, Easing.OutQuint);
        this.FadeInFromZero(400, Easing.OutQuint);
    }

    protected override void PopOut()
    {
        this.ScaleTo(0f, 400, Easing.InQuint);
        this.FadeOut(400, Easing.InQuint);
    }
}
