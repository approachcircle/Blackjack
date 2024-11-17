using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK.Graphics;

namespace Blackjack.Game;

public partial class BlackjackButton : ClickableContainer
{
    public string Text { get; set; } = string.Empty;

    [BackgroundDependencyLoader]
    private void load()
    {
        Masking = true;
        CornerRadius = 5;
        AutoSizeAxes = Axes.Both;
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        InternalChildren =
        [
            new Box
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Width = 100,
                Height = 50,
                Colour = Color4.DimGray
            },
            new SpriteText
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Text = Text,
                Font = FontUsage.Default.With(size: 20),
            }
        ];
    }

    protected override bool OnMouseDown(MouseDownEvent e)
    {
        this.ScaleTo(0.8f);
        return true;
    }

    protected override void OnMouseUp(MouseUpEvent e)
    {
        this.ScaleTo(1);
    }

    protected override bool OnHover(HoverEvent e)
    {
        this.FadeTo(0.75f);
        return true;
    }

    protected override void OnHoverLost(HoverLostEvent e)
    {
        this.FadeTo(1f);
    }
}
