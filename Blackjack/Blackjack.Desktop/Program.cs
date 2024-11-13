using osu.Framework.Platform;
using osu.Framework;
using Blackjack.Game;

namespace Blackjack.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using (GameHost host = Host.GetSuitableDesktopHost(@"Blackjack"))
            using (osu.Framework.Game game = new BlackjackGame())
                host.Run(game);
        }
    }
}