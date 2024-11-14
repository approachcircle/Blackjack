﻿using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace Blackjack.Game;

public partial class CardModel(string card) : Container
{
    public static Vector2 CardSize => new(160, 260);
    private static Vector2 cardSymbolPadding => new(30, 35);

    [BackgroundDependencyLoader]
    private void load()
    {
        Anchor = Anchor.BottomCentre;
        Origin = Anchor.BottomCentre;
        AutoSizeAxes = Axes.Both;
        InternalChildren =
        [
            new Box
            {
                Width = CardSize.X,
                Height = CardSize.Y,
                Anchor = Anchor.BottomCentre,
                Origin = Anchor.BottomCentre,
                Colour = Color4.White,
            },
            new Box
            {
                  Width = CardSize.X - 20,
                  Height = CardSize.Y - 20,
                  Anchor = Anchor.BottomCentre,
                  Origin = Anchor.BottomCentre,
                  Y = -10,
                  Colour = Color4.LightPink,
            },
            new SpriteText // symbol in top left
            {
                Anchor = Anchor.TopLeft,
                Origin = Anchor.Centre,
                X = cardSymbolPadding.X,
                Y = cardSymbolPadding.Y,
                Font = FontUsage.Default.With(size: 40),
                Colour = Color4.Black,
                Text = CardDeck.CardModelSymbol[card]
            },
            new SpriteText // symbol in centre
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Font = FontUsage.Default.With(size: 81),
                Colour = Color4.Black,
                Text = CardDeck.CardModelSymbol[card]
            },
            new SpriteText // symbol in bottom right
            {
                Anchor = Anchor.BottomRight,
                Origin = Anchor.Centre,
                Font = FontUsage.Default.With(size: 40),
                Colour = Color4.Black,
                Text = CardDeck.CardModelSymbol[card],
                X = -cardSymbolPadding.X,
                Y = -cardSymbolPadding.Y,
                Rotation = -180,
            },
        ];
    }
}
