using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;
using osuTK;

namespace Blackjack.Game
{
    public partial class MainScreen : BlackjackScreen
    {
        private CardHand playerHand;
        private CardHand dealerHand;
        private BlackjackButton hitButton;
        private BlackjackButton standButton;
        private FlashingButton rematchButton;
        private BlackjackButton quitButton;
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
            hitButton = new BlackjackButton
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
            standButton = new BlackjackButton
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
            rematchButton = new FlashingButton
            {
                Anchor = Anchor.BottomRight,
                Origin = Anchor.Centre,
                Text = "Rematch",
                Action = load,
                Y = -100,
                X = -20,
                Alpha = 0f
            };
            // i hate this so much, there must be a better way
            // please tell me there is
            // i just want transformations to be applied NOT from the origin
            // it works.......... but at what cost.........
            // https://github.com/ppy/osu-framework/discussions/6431
            rematchButton.OnLoadComplete += d =>
            {
                d.X -= d.Width / 2;
                d.Y -= d.Height / 2;
            };
            quitButton = new BlackjackButton
            {
                Anchor = Anchor.BottomLeft,
                Origin = Anchor.BottomLeft,
                Text = "Quit",
                Action = this.Exit,
                Y = -100,
                X = 20,
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
            scoresContainer.Add(dealerScore);
            scoresContainer.Add(playerScore);
            playerHand.HandState.BindValueChanged(e =>
            {
                if (e.NewValue is HandState.Active or HandState.NotReady or HandState.Standing) return;
                currentGameEndOverlay?.Expire();
                currentGameEndOverlay = new GameEndOverlay(e.NewValue);
                AddInternal(currentGameEndOverlay);
                currentGameEndOverlay.OnOverlayPopout = () =>
                {
                    rematchButton.Alpha = 1.0f;
                    quitButton.Alpha = 1.0f;
                    rematchButton.StartFlashing();
                };
                hitButton.Enabled.Value = false;
                standButton.Enabled.Value = false;
                dealerHand.HandState.BindValueChanged(dealerEvent =>
                {
                    // only reveal card if it is present and the dealer is ready
                    if (dealerEvent.NewValue == HandState.NotReady) return;
                    dealerHand.RevealCard();
                    // show the overlay straight away if we're bust as the player can immediately see
                    // if their score is above 21, therefore no suspense needed
                    if (e.NewValue is HandState.Bust)
                        currentGameEndOverlay.Show();
                }, true);
            }, true);
            dealerHand.OnCardFlipped = () =>
            {
                dealerHand.HandScore.BindValueChanged((e) =>
                {
                    dealerScore.Text = "Dealer's hand: " + e.NewValue;
                }, true);
                if (playerHand.HandState.Value is not HandState.Bust)
                    currentGameEndOverlay.Show();
            };
            dealerHand.OnCardDrawn = () => GameWatcher.Update(playerHand, dealerHand);
            playerHand.OnCardDrawn = () => GameWatcher.Update(playerHand, dealerHand);
            InternalChildren =
            [
                hitButton,
                playerHand,
                dealerHand,
                scoresContainer,
                standButton,
                rematchButton,
                quitButton
            ];
            // cards should be logically dealt in the order they are in the real game
            dealerHand.DrawCard();
            playerHand.DrawCard();
            dealerHand.DrawCard(flipped: true);
            playerHand.DrawCard();
        }
    }
}
