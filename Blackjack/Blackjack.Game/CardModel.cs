using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osuTK;
using osuTK.Graphics;

namespace Blackjack.Game;

public partial class CardModel(string card, HandOwner handOwner) : Container
{
    public static Vector2 CardSize => new(160, 260);
    private static Vector2 cardSymbolPadding => new(30, 35);
    private Bindable<bool> isCardFlipped = new();
    private SpriteText topLeftSymbol;
    private SpriteText centreSymbol;
    private SpriteText bottomRightSymbol;

    [BackgroundDependencyLoader]
    private void load()
    {
        Anchor = handOwner == HandOwner.Player ? Anchor.BottomCentre : Anchor.TopCentre;
        Origin = handOwner == HandOwner.Player ? Anchor.BottomCentre : Anchor.TopCentre;
        AutoSizeAxes = Axes.Both;
        InternalChildren =
        [
            new Box
            {
                Width = CardSize.X,
                Height = CardSize.Y,
                Anchor = handOwner == HandOwner.Player ? Anchor.BottomCentre : Anchor.TopCentre,
                Origin = handOwner == HandOwner.Player ? Anchor.BottomCentre : Anchor.TopCentre,
                Colour = Color4.White,
            },
            new Box
            {
                  Width = CardSize.X - 20,
                  Height = CardSize.Y - 20,
                  Anchor = handOwner == HandOwner.Player ? Anchor.BottomCentre : Anchor.TopCentre,
                  Origin = handOwner == HandOwner.Player ? Anchor.BottomCentre : Anchor.TopCentre,
                  Y = handOwner == HandOwner.Player ? -10 : 10,
                  Colour = Color4.LightPink,
            },
            topLeftSymbol = new SpriteText
            {
                Anchor = Anchor.TopLeft,
                Origin = Anchor.Centre,
                X = cardSymbolPadding.X,
                Y = cardSymbolPadding.Y,
                Font = FontUsage.Default.With(size: 40),
                Colour = Color4.Black,
                Text = CardDeck.CardModelSymbol[card]
            },
            centreSymbol = new SpriteText
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Font = FontUsage.Default.With(size: 81),
                Colour = Color4.Black,
                Text = CardDeck.CardModelSymbol[card]
            },
            bottomRightSymbol = new SpriteText
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
        isCardFlipped.BindValueChanged(e =>
        {
            if (e.NewValue)
            {
                topLeftSymbol.Hide();
                centreSymbol.Hide();
                bottomRightSymbol.Hide();
            }
            else
            {
                topLeftSymbol.Show();
                centreSymbol.Show();
                bottomRightSymbol.Show();
            }
            // centreSymbol.Alpha = e.NewValue ? 0 : 1;
            // bottomRightSymbol.Alpha = e.NewValue ? 0 : 1;
        }, true);
    }

    public void ToggleCardFlipped()
    {
        isCardFlipped.Value = !isCardFlipped.Value;
    }
}
