using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Audio.Sample;
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
    private readonly Bindable<bool> isCardFlipped = new();
    private SpriteText topLeftSymbol;
    private SpriteText centreSymbol;
    private SpriteText bottomRightSymbol;
    private List<Sample> flipSamples;

    [BackgroundDependencyLoader]
    private void load(ISampleStore samples)
    {
        Anchor = handOwner == HandOwner.Player ? Anchor.BottomCentre : Anchor.TopCentre;
        Origin = handOwner == HandOwner.Player ? Anchor.BottomCentre : Anchor.TopCentre;
        AutoSizeAxes = Axes.Both;
        flipSamples = new List<Sample>();
        foreach (string sampleName in samples.GetAvailableResources())
        {
            if (sampleName.Contains("flip"))
            {
                flipSamples.Add(samples.Get(sampleName));
            }
        }
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
                centreSymbol.Text = "?";
                bottomRightSymbol.Hide();
            }
            else
            {
                topLeftSymbol.Show();
                centreSymbol.Text = CardDeck.CardModelSymbol[card];
                bottomRightSymbol.Show();
            }
        }, true);
    }

    private void playRandomFlipSample()
    {
        if (Settings.SFXEnabled.Value)
        {
            flipSamples.ElementAt(new Random().Next(0, flipSamples.Count)).Play();
        }
    }

    public void RevealCardAnimated(Action onFlipped)
    {
        // if we fade out completely, the fill flow container thinks the card isn't there anymore,
        // so tries to readjust the other cards, causing it to jump around for a few frames
        this.FadeTo(0.01f, 350, Easing.OutQuint).ScaleTo(new Vector2(0.01f, 1), 350, Easing.OutQuint).Finally((_) =>
        {
            revealCard();
            this.FadeIn(250, Easing.InQuint).ScaleTo(Vector2.One, 250, Easing.InQuint).Finally(_ =>
            {
                playRandomFlipSample();
                onFlipped?.Invoke();
            });
        });
    }

    private void revealCard()
    {
        isCardFlipped.Value = false;
    }

    public void FlipCard()
    {
        isCardFlipped.Value = true;
    }
}
