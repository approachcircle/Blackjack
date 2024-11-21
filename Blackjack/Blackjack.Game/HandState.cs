namespace Blackjack.Game;

public enum HandState
{
    NotReady,
    Active,
    Standing,
    Pushed,
    PlayerTwentyOne,
    DealerTwentyOne,
    PlayerBlackjack,
    DealerBlackjack,
    Bust,
    BeatDealer,
    BeatByDealer,
    DealerBust,
    PlayerFiveCardCharlie,
    DealerFiveCardCharlie
}
