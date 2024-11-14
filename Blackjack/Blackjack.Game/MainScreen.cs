using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;
using osuTK.Graphics;

namespace Blackjack.Game
{
    public partial class MainScreen : Screen
    {
        private CardHand cardHand;
        private Button drawCardButton;
        private SpriteText score;
        private Bindable<int> bindableScore = new(0);

        [BackgroundDependencyLoader]
        private void load()
        {
            cardHand = new CardHand();
            drawCardButton = new BasicButton
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Text = "Draw card",
                Action = onCardDrawn,
                Width = 200,
                Height = 100,
                Y = -100
            };
            score = new SpriteText
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Y = 20,
                Font = FontUsage.Default.With(size: 48),
            };
            bindableScore.BindValueChanged(e =>
            {
                score.Text = "Your hand: " + e.NewValue;
            }, true);
            InternalChildren =
            [
                drawCardButton,
                cardHand,
                score
            ];
        }

        private void onCardDrawn()
        {
            bool deckEmpty = true;
            foreach (int quantity in CardDeck.CardQuantities.Values)
            {
                if (quantity > 0) deckEmpty = false; break;
            }
            if (deckEmpty)
            {
                drawCardButton
                    .FadeColour(Color4.DarkRed)
                    .Delay(250)
                    .FadeColour(Color4.Gray, 250);
                return;
            }

            var cardDrawn = drawCard();
            cardHand.Add(new CardModel(cardDrawn));
            bindableScore.Value += CardDeck.CardValues[cardDrawn];
        }

        private static string drawCard()
        {
            for (;;)
            {
                string drawnCard = CardDeck.CardValues.Keys.ElementAt(new Random().Next(0, CardDeck.CardValues.Keys.Count));
                if (CardDeck.CardQuantities[drawnCard] <= 0) continue;
                CardDeck.CardQuantities[drawnCard]--;
                return drawnCard;
            }
        }
    }
}
