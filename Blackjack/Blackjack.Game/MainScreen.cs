using osu.Framework.Allocation;
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
        private BasicButton rematchButton;
        private SpriteText playerScore;
        private SpriteText dealerScore;
        private FillFlowContainer scoresContainer;
        private GameEndOverlay currentGameEndOverlay;

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
                Action = () =>
                {
                    playerHand.HandState.Value = HandState.Standing;
                    GameWatcher.Update(playerHand, dealerHand);
                },
                Width = 200,
                Height = 100,
                // Y = -100,
                X = -20
            };
            // TODO: make this button flash to make it more obvious.
            // TODO: first needs to be turned into a 'BlackjackButton'
            // TODO: then specialised into a specific flashing button
            rematchButton = new BasicButton
            {
                Anchor = Anchor.BottomRight,
                Origin = Anchor.BottomRight,
                Text = "Rematch",
                Action = load,
                Width = 200,
                Height = 100,
                Y = -100,
                X = -20,
                Alpha = 0f
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
            playerHand.HandState.BindValueChanged(e =>
            {
                if (e.NewValue is HandState.Active or HandState.NotReady or HandState.Standing) return;
                currentGameEndOverlay?.Expire();
                currentGameEndOverlay = new GameEndOverlay(e.NewValue);
                hitButton.Enabled.Value = false;
                standButton.Enabled.Value = false;
                dealerHand.HandState.BindValueChanged(dealerEvent =>
                {
                    // only reveal card if it is present and the dealer is ready
                    if (dealerEvent.NewValue == HandState.NotReady) return;
                    dealerHand.RevealCard();
                }, true);
                AddInternal(currentGameEndOverlay);
                currentGameEndOverlay.Show();
                rematchButton.Alpha = 1.0f;
            }, true);
            dealerHand.OnCardDrawn = () => GameWatcher.Update(playerHand, dealerHand);
            playerHand.OnCardDrawn = () => GameWatcher.Update(playerHand, dealerHand);
            InternalChildren =
            [
                hitButton,
                playerHand,
                dealerHand,
                scoresContainer,
                standButton,
                rematchButton
            ];
            playerHand.DrawCard();
            playerHand.DrawCard();
            dealerHand.DrawCard();
            dealerHand.DrawCard(flipped: true);
        }
    }
}
