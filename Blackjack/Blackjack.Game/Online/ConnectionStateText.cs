using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;

namespace Blackjack.Game.Online;

public partial class ConnectionStateText : Container
{
    [BackgroundDependencyLoader]
    private void load()
    {
        Anchor = Anchor.BottomRight;
        Origin = Anchor.BottomRight;
        Y = -5;
        X = -5;
        AutoSizeAxes = Axes.Both;
        SpriteText text = new SpriteText
        {
            Anchor = Anchor.BottomCentre,
            Origin = Anchor.BottomCentre,
            Font = FontUsage.Default.With(size: 24),
            Text = "Connecting...",
        };
        Bindable<string> textBindable = new Bindable<string>();
        textBindable.BindValueChanged(e =>
        {
            text.Text = e.NewValue;
        });
        StatefulSignalRClient.Instance.ApiState.BindValueChanged(e =>
        {
            textBindable.Value = e.NewValue.ToString();
        });
        Add(text);
    }
}
