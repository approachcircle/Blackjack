﻿namespace Blackjack.Game;

public static class GameWatcher
{
    public static void Update(CardHand playerHand, CardHand dealerHand)
    {
        if (playerHand.HandScore.Value >= 17 && playerHand.HandScore.Value == dealerHand.HandScore.Value)
        {
            playerHand.HandState.Value = HandState.Pushed;
            return;
        }

        if (playerHand.HandScore.Value == 21)
        {
            playerHand.HandState.Value = playerHand.CardCount == 2 ? HandState.PlayerBlackjack : HandState.PlayerTwentyOne;
            return;
        }

        if (playerHand.HandScore.Value > 21)
        {
            if (playerHand.HasHighAce)
            {
                playerHand.HandScore.Value -= 10;
                playerHand.HasHighAce = false;
                return;
            }
            playerHand.HandState.Value = HandState.Bust;
            return;
        }

        checkFiveCardCharlie(playerHand, dealerHand);

        if (playerHand.HandScore.Value > dealerHand.HandScore.Value && dealerHand.HandScore.Value == 17)
        {
            playerHand.HandState.Value = HandState.BeatDealer;
            return;
        }

        if (playerHand.HandState.Value == HandState.Standing)
        {
            while (dealerHand.HandScore.Value < 17)
            {
                dealerHand.DrawCard();
            }

            if (dealerHand.HandScore.Value == 21)
            {
                playerHand.HandState.Value = dealerHand.CardCount == 2 ? HandState.DealerBlackjack : HandState.DealerTwentyOne;
                return;
            }

            if (dealerHand.HandScore.Value > 21)
            {
                if (dealerHand.HasHighAce)
                {
                    dealerHand.HandScore.Value -= 10;
                    dealerHand.HasHighAce = false;
                    return;
                }
                playerHand.HandState.Value = HandState.DealerBust;
                return;
            }

            checkFiveCardCharlie(playerHand, dealerHand);

            if (playerHand.HandScore.Value > dealerHand.HandScore.Value)
            {
                playerHand.HandState.Value = HandState.BeatDealer;
                return;
            }
            if (dealerHand.HandScore.Value > playerHand.HandScore.Value)
            {
                playerHand.HandState.Value = HandState.BeatByDealer;
                return;
            }
        }
    }

    private static void checkFiveCardCharlie(CardHand playerHand, CardHand dealerHand)
    {
        if (!Settings.FiveCardCharlie.Value) return;
        if (playerHand.CardCount >= 5 && dealerHand.CardCount >= 5)
        {
            playerHand.HandState.Value = HandState.Pushed;
            return;
        }

        if (playerHand.CardCount >= 5)
        {
            playerHand.HandState.Value = HandState.PlayerFiveCardCharlie;
            return;
        }

        if (dealerHand.CardCount >= 5)
        {
            dealerHand.HandState.Value = HandState.DealerFiveCardCharlie;
            return;
        }
    }
}
