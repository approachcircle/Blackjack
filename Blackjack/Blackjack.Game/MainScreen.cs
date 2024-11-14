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
        private CardHand playerHand;
        private CardHand dealerHand;
        private Button hitButton;
        private Button standButton;
        private SpriteText score;
        private Bindable<int> bindableScore = new();

        [BackgroundDependencyLoader]
        private void load()
        {
            playerHand = new CardHand(HandOwner.Player);
            dealerHand = new CardHand(HandOwner.Dealer);
            hitButton = new BasicButton
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                Text = "Hit",
                Action = () => onCardDrawRequest(HandOwner.Player),
                Width = 200,
                Height = 100,
                // Y = -100,
                X = 20
            };
            standButton = new BasicButton
            {
                Anchor = Anchor.CentreRight,
                Origin = Anchor.CentreRight,
                Text = "Stand",
                // Action = onCardDrawRequest,
                Width = 200,
                Height = 100,
                // Y = -100,
                X = -20
            };
            score = new SpriteText
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                // Y = 20,
                Font = FontUsage.Default.With(size: 48),
            };
            bindableScore.BindValueChanged(e =>
            {
                score.Text = "Your hand: " + e.NewValue;
            }, true);
            InternalChildren =
            [
                hitButton,
                playerHand,
                dealerHand,
                score,
                standButton
            ];
            onCardDrawRequest(HandOwner.Player);
            onCardDrawRequest(HandOwner.Player);
            onCardDrawRequest(HandOwner.Dealer);
            onCardDrawRequest(HandOwner.Dealer, true);
        }

        private void onCardDrawRequest(HandOwner handOwner, bool isCardFlipped = false)
        {
            // TODO: not sure if this impl works correctly, plus the player wins by '5-Card charlie' if they have not
            // TODO: busted with 5 cards in the deck, therefore that can be the limit
            bool deckEmpty = true;
            foreach (int quantity in CardDeck.CardQuantities.Values)
            {
                if (quantity > 0) deckEmpty = false; break;
            }
            if (deckEmpty)
            {
                hitButton
                    .FadeColour(Color4.DarkRed)
                    .Delay(250)
                    .FadeColour(Color4.Gray, 250);
                return;
            }

            var cardDrawn = drawCard();
            var cardModel = new CardModel(cardDrawn, handOwner);
            if (isCardFlipped) cardModel.ToggleCardFlipped();
            if (handOwner == HandOwner.Player) playerHand.Add(cardModel); else dealerHand.Add(cardModel);
            if (handOwner == HandOwner.Player) bindableScore.Value += CardDeck.CardValues[cardDrawn];
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
