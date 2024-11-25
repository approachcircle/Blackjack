using System;
using Blackjack.Game.Online;
using Microsoft.AspNetCore.SignalR.Client;
using osu.Framework.Allocation;
using osu.Framework.Screens;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics.Sprites;
using osuTK;

namespace Blackjack.Game;

public partial class MainMenuScreen : BlackjackScreen
{
    private FillFlowContainer buttonContainer;

    [BackgroundDependencyLoader]
    private async void load()
    {
        Anchor = Anchor.Centre;
        Origin = Anchor.Centre;
        InternalChildren =
        [
            new SpriteText
            {
                Anchor = Anchor.TopCentre,
                Origin = Anchor.TopCentre,
                Font = FontUsage.Default.With(size: 72),
                Text = "Blackjack",
                Y = 50
            },
            new BlackjackButton
            {
                Text = "Settings",
                Anchor = Anchor.TopRight,
                Origin = Anchor.TopRight,
                X = -20,
                Y = 20,
                Action = () => this.Push(new SettingsScreen())
            },
            buttonContainer = new FillFlowContainer
            {
                Direction = FillDirection.Horizontal,
                Spacing = new Vector2(10, 0),
                Anchor = Anchor.BottomCentre,
                Origin = Anchor.BottomCentre,
                AutoSizeAxes = Axes.Both,
                Y = -50
            }
        ];
        buttonContainer.AddRange([
            new BlackjackButton
            {
                Anchor = Anchor.BottomCentre,
                Origin = Anchor.BottomCentre,
                Action = () => this.Push(new MainScreen()),
                Text = "Play"
            },
            new BlackjackButton
            {
                Anchor = Anchor.BottomCentre,
                Origin = Anchor.BottomCentre,
                Action = () => Game.Exit(),
                Text = "Exit"
            }
        ]);
        try
        {
            await APIAccess.Connection.StartAsync();
        }
        catch (Exception e)
        {
            // ignored
            // for now
        }

        APIAccess.Connection.On<string>("ReceiveMessage", message =>
        {
            Scheduler.Add(() =>
            {
                Console.WriteLine(message);
            });
        });
        await APIAccess.Connection.InvokeAsync("SendMessage", "good morning");
    }

    public override bool OnExiting(ScreenExitEvent e)
    {
        var result = base.OnExiting(e);
        Game.Exit();
        return result;
    }
}
