using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Game;

public static class CardDeck
{
    public static Dictionary<string, int> CardValues =>
        new()
        {
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

    private static Dictionary<string, string> cardModelSymbol; // luh calm bit of caching

    public static Dictionary<string, string> CardModelSymbol
    {
        get
        {
            if (cardModelSymbol != null) return cardModelSymbol;
            var symbols = new Dictionary<string, string>();
            for (int i = 0; i < CardValues.Count - 4; i++)
            {
                symbols.Add(CardValues.Keys.ElementAt(i), CardValues.Values.ElementAt(i).ToString());
            }
            symbols.Add("King", "K");
            symbols.Add("Queen", "Q");
            symbols.Add("Jack", "J");
            symbols.Add("Ace", "A");
            cardModelSymbol = symbols;
            return cardModelSymbol;
        }
    }

    private static Dictionary<string, int> cardQuantities;
    public static bool IsQuantitiesValid { get; set; }

    public static Dictionary<string, int> CardQuantities
    {
        get
        {
            if (IsQuantitiesValid && cardQuantities != null) return cardQuantities;
            var quantities = new Dictionary<string, int>();
            for (int i = 0; i < CardValues.Count; i++)
            {
                quantities.Add(CardValues.Keys.ElementAt(i), 4);
            }
            cardQuantities = quantities;
            IsQuantitiesValid = true;
            return cardQuantities;
        }
    }
}
