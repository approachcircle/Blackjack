using System;
using Blackjack.Game.Online;
using osu.Framework.Allocation;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace Blackjack.Game
{
    public partial class BlackjackGame : BlackjackGameBase
    {
        private ScreenStack screenStack;
        // private ServerConnectionOverlay serverConnectionOverlay;

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both });
            // serverConnectionOverlay = new ServerConnectionOverlay("connecting...");
            // Add(serverConnectionOverlay);
            Add(new ConnectionStateText());
            _ = StatefulSignalRClient.Instance.Connect();
            StatefulSignalRClient.Instance.ApiState.BindValueChanged(e =>
            {
                Console.WriteLine(e.NewValue);
                // if (e.NewValue == ApiState.Online)
                // {
                //     serverConnectionOverlay.Complete();
                // }
                // else
                // {
                //     serverConnectionOverlay.StateChanged(e.NewValue.ToString());
                // }
            }, true);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            screenStack.Push(new MainMenuScreen());
        }

        protected override bool OnExiting()
        {
            Scheduler.Add(async void () =>
            {
                int attempts = 0;
                do
                {
                    if (attempts >= 5)
                        break; // if we can't disconnect after 5 tries for some reason, just bail and forget about it
                    await StatefulSignalRClient.Instance.Disconnect();
                    attempts++;
                }
                while (StatefulSignalRClient.Instance.ApiState.Value == ApiState.Online);
            });
            return base.OnExiting();
        }
    }
}
