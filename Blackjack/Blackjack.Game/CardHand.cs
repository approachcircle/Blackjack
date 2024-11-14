using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Blackjack.Game;

public partial class CardHand : FillFlowContainer
{
    private static float containerPadding => -20;

    [BackgroundDependencyLoader]
    private void load()
    {
        Direction = FillDirection.Horizontal;
        AutoSizeAxes = Axes.Y;
        Anchor = Anchor.BottomCentre;
        Origin = Anchor.BottomCentre;
        Spacing = new Vector2(10, 0);
        MaximumSize = new Vector2(CardModel.CardSize.X + 70, CardModel.CardSize.Y);
        Y = containerPadding;
    }
}
