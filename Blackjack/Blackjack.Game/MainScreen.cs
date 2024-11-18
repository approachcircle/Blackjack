using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osuTK;

namespace Blackjack.Game
{
    public partial class MainScreen : BlackjackScreen
    {
        private CardHand playerHand;
        private CardHand dealerHand;
        private BasicButton hitButton;
        private BasicButton standButton;
        private SpriteText playerScore;
        private SpriteText dealerScore;
        private FillFlowContainer scoresContainer;
        private SpriteText handState;
        private readonly Bindable<int> bindableScore = new();

        [BackgroundDependencyLoader]
        private void load()
        {
            Anchor = Anchor.Centre;
            Origin = Anchor.Centre;
            // mostly for use in the test browser, but just ensures that the card
            // quantities are reset after each push of this screen
            CardDeck.IsQuantitiesValid = false;
            playerHand = new CardHand(HandOwner.Player);
            dealerHand = new CardHand(HandOwner.Dealer);
            hitButton = new BasicButton
            {
                Anchor = Anchor.CentreLeft,
                Origin = Anchor.CentreLeft,
                Text = "Hit",
                Action = () => playerHand.DrawCard(),
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
                Action = () => playerHand.HandState.Value = HandState.Standing,
                Width = 200,
                Height = 100,
                // Y = -100,
                X = -20
            };
            scoresContainer = new FillFlowContainer()
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Spacing = new Vector2(0, 20),
                Direction = FillDirection.Vertical
            };
            playerScore = new SpriteText
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Y = 10,
                Font = FontUsage.Default.With(size: 48),
            };
            playerHand.HandScore.BindValueChanged(e =>
            {
                playerScore.Text = "Your hand: " + e.NewValue;
            }, true);
            dealerScore = new SpriteText()
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Y = -10,
                Font = FontUsage.Default.With(size: 48),
            };
            dealerHand.OnCardFlipped = () =>
            {
                dealerHand.HandScore.BindValueChanged((e) =>
                {
                    dealerScore.Text = "Dealer's hand: " + e.NewValue;
                }, true);
            };
            scoresContainer.Add(dealerScore);
            scoresContainer.Add(playerScore);
            handState = new SpriteText
            {
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight,
                Font = FontUsage.Default.With(size: 20)
            };
            playerHand.HandState.BindValueChanged(e =>
            {
                handState.Text = "Hand state: " + e.NewValue;
                if (e.NewValue == HandState.Active) return;
                GameEndOverlay overlay = null;
                int displayDuration = 800;
                if (e.NewValue == HandState.Standing)
                {
                    overlay = new GameEndOverlay(Colour4.Blue, "Standing");
                    displayDuration = 400;
                } else if (e.NewValue == HandState.Bust)
                {
                    overlay = new GameEndOverlay(Colour4.Red, "Bust");
                } else if (e.NewValue is HandState.Blackjack or HandState.Won)
                {
                    overlay = new GameEndOverlay(Colour4.Green, "You win");
                }
                hitButton.Enabled.Value = false;
                standButton.Enabled.Value = false;
                dealerHand.RevealCard();
                if (overlay is not null)
                {
                    AddInternal(overlay);
                    overlay.Show();
                    Scheduler.AddDelayed(() =>
                    {
                        overlay.Hide();
                    }, displayDuration);
                }
            }, true);
            InternalChildren =
            [
                hitButton,
                playerHand,
                dealerHand,
                scoresContainer,
                standButton,
                handState
            ];
            playerHand.DrawCard();
            playerHand.DrawCard();
            dealerHand.DrawCard();
            dealerHand.DrawCard(flipped: true);
        }
    }
}
