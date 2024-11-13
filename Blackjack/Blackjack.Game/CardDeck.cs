using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Game;

public static class CardDeck
{
    //TODO: need to add quantities to each card, and subtract them once added to a hand
    public static Dictionary<string, int> Cards =>
        new()
        {
            { "One", 1 },
            { "Two", 2 },
            { "Three", 3 },
            { "Four", 4 },
            { "Five", 5 },
            { "Six", 6 },
            { "Seven", 7 },
            { "Eight", 8 },
            { "Nine", 9 },
            { "Ten", 10 },
            { "King", 10 },
            { "Queen", 10 },
            { "Jack", 10 },
            { "Ace", 11 }
        };

    private static Dictionary<string, string> cardModelSymbol;

    public static Dictionary<string, string> CardModelSymbol
    {
        get
        {
            if (cardModelSymbol != null) return cardModelSymbol;
            var symbols = new Dictionary<string, string>();
            for (int i = 0; i < 10; i++)
            {
                symbols.Add(Cards.Keys.ElementAt(i), (i + 1).ToString());
            }
            symbols.Add("King", "K");
            symbols.Add("Queen", "Q");
            symbols.Add("Jack", "J");
            symbols.Add("Ace", "A");
            cardModelSymbol = symbols;
            return cardModelSymbol;
        }
    }

    // { "One", "1" },
    // { "Two", "2" },
    // { "Three", "3" },
    // { "Four", "4" },
    // { "Five", "5" },
    // { "Six", "6" },
    // { "Seven", "7" },
    // { "Eight", "8" },
    // { "Nine", "9" },
    // { "Ten", "10" },
    // { "King", "K" },
    // { "Queen", "Q" },
    // { "Jack", "J" },
    // { "Ace", "A" }
}
