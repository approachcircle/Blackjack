using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osuTK;

namespace Blackjack.Game;

public partial class CardHand(HandOwner handOwner) : FillFlowContainer
{
    private static float containerPadding => -20;
    public Bindable<int> HandScore { get; } = new();
    private Action onCardFlipped;

    public Action OnCardFlipped
    {
        get => onCardFlipped;
        set
        {
            if (handOwner != HandOwner.Dealer)
            {
                throw new InvalidOperationException("this action may only be assigned to on the dealer's hand.");
            }
            onCardFlipped = value;
        }
    }

    public Action OnCardDrawn { get; set; }

    public Bindable<HandState> HandState { get; } = new(handOwner == HandOwner.Player ? Game.HandState.Active : Game.HandState.NotReady);
    private CardModel flippedCard;
    public int CardCount { get; private set; } = 0;

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

    public void DrawCard(string card = null, bool flipped = false)
    {
        string activeCard = card;
        if (activeCard is null)
        {
            for (;;)
            {
                activeCard =
                    CardDeck.CardValues.Keys.ElementAt(new Random().Next(0, CardDeck.CardValues.Keys.Count));
                if (CardDeck.CardQuantities[activeCard] <= 0) continue;
                break;
            }
        }

        var cardModel = new CardModel(activeCard, handOwner);
        if (flipped)
        {
            flippedCard = cardModel;
            flippedCard.FlipCard();
            HandState.Value = Game.HandState.Active;
        }
        Add(cardModel);
        if (activeCard == "Ace")
        {
            if (HandScore.Value + 11 > 21) HandScore.Value++; // Ace low (1)
            else HandScore.Value += 11; // Ace high (11)
        }
        else
        {
            HandScore.Value += CardDeck.CardValues[activeCard];
        }
        CardDeck.CardQuantities[activeCard]--;
        CardCount++;
        OnCardDrawn?.Invoke();
        // if (HandScore.Value == 21)
        // {
        //     HandState.Value = Game.HandState.Blackjack;
        //     return;
        // }
        // if (HandScore.Value > 21)
        // {
        //     HandState.Value = Game.HandState.Bust;
        //     return;
        // }
        // if (HandScore.Value == 17 && handOwner == HandOwner.Dealer)
        // {
        //     HandState.Value = Game.HandState.Standing;
        // }
    }

    public void RevealCard()
    {
        if (handOwner != HandOwner.Dealer)
        {
            throw new InvalidOperationException(
                "cannot reveal a card on a player's hand, this may only be performed on the dealer.");
        }
        flippedCard.RevealCardAnimated(OnCardFlipped);
    }
}
