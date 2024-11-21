using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Input.Events;
using osuTK;
using osuTK.Graphics;

namespace Blackjack.Game;

public partial class BlackjackButton(Vector2? buttonSize = null) : ClickableContainer
{
    public string Text { get; set; } = string.Empty;
    private Colour4 disabledColour;
    protected Colour4 BackgroundColour { get; } = Color4.DimGray;
    protected Vector2 DefaultButtonSize { get; } = new(200, 100);

    [BackgroundDependencyLoader]
    private void load()
    {
        disabledColour = Colour4.DarkGray.Darken(1);
        Size = buttonSize ?? DefaultButtonSize;
        InternalChild = new Container
        {
            RelativeSizeAxes = Axes.Both,
            Masking = true,
            CornerRadius = 5,
            Children = [
                new Box
                {
                    RelativeSizeAxes = Axes.Both,
                    Colour = BackgroundColour
                },
                new SpriteText
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                    Text = Text,
                    Font = FontUsage.Default.With(size: 20)
                }
            ]
        };
        Enabled.BindValueChanged(e =>
        {
            Colour = e.NewValue ? Colour4.White : disabledColour;
        }, true);
    }

    protected override bool OnMouseDown(MouseDownEvent e)
    {
        if (Enabled.Value)
        {
            Colour = Colour4.Aquamarine;
        }
        else
        {
            this.FadeColour(Colour4.Red)
                .Delay(200)
                .FadeColour(disabledColour, 200, Easing.Out);
        }
        return true;
    }

    protected override void OnMouseUp(MouseUpEvent e)
    {
        base.OnMouseUp(e);
        if (Enabled.Value) Colour = Colour4.White;
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
