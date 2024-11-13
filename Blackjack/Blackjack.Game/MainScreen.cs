using System;
using System.Collections.Generic;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Shapes;
using osu.Framework.Graphics.Sprites;
using osu.Framework.Graphics.UserInterface;
using osu.Framework.Screens;
using osuTK.Graphics;

namespace Blackjack.Game
{
    public partial class MainScreen : Screen
    {
        [BackgroundDependencyLoader]
        private void load()
        {
            Button button = new BasicButton
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                Text = "Draw card",
                Action = () => AddInternal(new CardModel(drawCard())),
                Width = 200,
                Height = 100,
                Y = -100
            };
            InternalChildren =
            [
                button
            ];
        }

        private static string drawCard()
        {
            return CardDeck.Cards.Keys.ElementAt(new Random().Next(0, CardDeck.Cards.Keys.Count));
        }
    }
}
