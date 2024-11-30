using System;
using osu.Framework.Platform;
using osu.Framework;
using Blackjack.Game;
using Gtk;

namespace Blackjack.Desktop
{
    public static class Program
    {
        public static void Main()
        {
            using GameHost host = Host.GetSuitableDesktopHost(@"Blackjack");
            using osu.Framework.Game game = new BlackjackGame();
            try
            {
                host.Run(game);
            }
            catch (Exception e)
            {
                //TODO: forms is windows only so is incompatible with this project
                //TODO: dotnet just hates PresentationFramework so Wpf is out of the question
                //TODO: GTK looks bad and is pretty much abandoned now
                //TODO: so what next?
                Application.Init();
                Window window = new Window("uh oh...");
                window.DeleteEvent += (_, _) =>
                {
                    Application.Quit();
                    game.RequestExit();
                };
                new Dialog(
                        "blackjack has crashed!",
                        window,
                        DialogFlags.Modal,
                        "OK"
                        ).Show();
                window.ShowAll();
                Application.Run();
            }
        }
    }
}
