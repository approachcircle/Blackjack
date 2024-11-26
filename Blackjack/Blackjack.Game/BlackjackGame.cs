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
        private readonly ServerConnectionOverlay serverConnectionOverlay = new();

        [BackgroundDependencyLoader]
        private void load()
        {
            Add(screenStack = new ScreenStack { RelativeSizeAxes = Axes.Both });
            Add(serverConnectionOverlay);
            initServerConnection();
            // Scheduler.AddDelayed(() =>
            // {
            //     Console.WriteLine($"connection state {APIAccess.Connection.State}");
            // }, 500, true);
        }

        private async void initServerConnection()
        {
            while (true)
            {
                try
                {
                    await APIAccess.Connection.StartAsync();
                }
                catch (Exception e)
                {
                    await Console.Error.WriteLineAsync(e.Message);
                    await Task.Delay(5000);
                }
                if (APIAccess.Connection.State == HubConnectionState.Connected)
                {
                    Scheduler.Add(() =>
                    {
                        serverConnectionOverlay.OverlayText.Value = "Connected!";
                    });
                    break;
                }
            }
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            screenStack.Push(new MainMenuScreen());
        }
    }
}
