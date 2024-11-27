using System;
using System.Threading.Tasks;
using Blackjack.Game.Online;
using Microsoft.AspNetCore.SignalR.Client;
using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Screens;

namespace Blackjack.Game
{
    public partial class BlackjackGame : BlackjackGameBase
    {
        private ScreenStack screenStack;
        private ServerConnectionOverlay serverConnectionOverlay;

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both });
            serverConnectionOverlay = new ServerConnectionOverlay("connecting...");
            Add(serverConnectionOverlay);
            Add(new ConnectionStateText());
            _ = StatefulSignalRClient.Instance.Connect();
            StatefulSignalRClient.Instance.ApiState.BindValueChanged(e =>
            {
                Console.WriteLine(e.NewValue);
                if (e.NewValue == ApiState.Online)
                {
                    serverConnectionOverlay.Complete();
                }
                else
                {
                    serverConnectionOverlay.StateChanged(e.NewValue.ToString());
                }
            }, true);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            screenStack.Push(new MainMenuScreen());
        }
    }
}
