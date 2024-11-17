using System;
using JetBrains.Annotations;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Blackjack.Game;

public partial class CardHand(HandOwner handOwner) : FillFlowContainer
{
    private static float containerPadding => -20;

    [BackgroundDependencyLoader]
    private void load()
    {
        Direction = FillDirection.Horizontal;
        AutoSizeAxes = Axes.Both;
        Anchor = handOwner == HandOwner.Player ? Anchor.BottomCentre : Anchor.TopCentre;
        Origin = handOwner == HandOwner.Player ? Anchor.BottomCentre : Anchor.TopCentre;
        Spacing = new Vector2(10, 0);
        MaximumSize = new Vector2(CardModel.CardSize.X + 70, CardModel.CardSize.Y);
        Y = handOwner == HandOwner.Player ? containerPadding : -containerPadding;
    }

    public override void Add([NotNull] Drawable drawable)
    {
        base.Add(drawable);
        Console.WriteLine("card added to hand");
    }
}
