using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using osu.Framework.Bindables;

namespace Blackjack.Game.Online;

public class StatefulSignalRClient
{
    public static StatefulSignalRClient Instance { get; private set; } = new StatefulSignalRClient();
    private HubConnection connection { get; } = new HubConnectionBuilder().WithUrl("http://localhost:5183/online").WithAutomaticReconnect().Build();

    public Bindable<ApiState> ApiState { get; private set; } = new(Online.ApiState.Offline);
    private const int retry_delay = 2500;

    private StatefulSignalRClient()
    {
        connection.Closed += e =>
        {
            ApiState.Value = Online.ApiState.Offline;
            if (e is not null)
            {
                Console.Error.WriteLine(e);
            }
            else
            {
                Console.WriteLine("closed fired, no reason");
            }
            return Task.CompletedTask;
        };
        connection.Reconnecting += e =>
        {
            ApiState.Value = Online.ApiState.Reconnecting;
            if (e is not null)
            {
                Console.Error.WriteLine(e);
            }
            else
            {
                Console.WriteLine("reconnecting fired, no reason");
            }
            return Task.CompletedTask;
        };
        connection.Reconnected += e =>
        {
            ApiState.Value = Online.ApiState.Online;
            if (e is not null)
            {
                Console.Error.WriteLine(e);
            }
            else
            {
                Console.WriteLine("reconnected fired, no reason");
            }
            return Task.CompletedTask;
        };
    }

    public async Task Connect()
    {
        Console.WriteLine("called manual connect");
        if (ApiState.Value == Online.ApiState.Online) return;
        try
        {
            await connection.StartAsync();
            ApiState.Value = Online.ApiState.Online;
        }
        catch (Exception e)
        {
            ApiState.Value = Online.ApiState.Offline;
            await Console.Error.WriteLineAsync("failed to connect: " + e);
            await Reconnect();
        }
    }

    public async Task Reconnect()
    {
        Console.WriteLine("Reconnecting...");
        while (ApiState.Value != Online.ApiState.Online)
        {
            try
            {
                await connection.StartAsync();
                ApiState.Value = Online.ApiState.Online;
            }
            catch (Exception e)
            {
                ApiState.Value = Online.ApiState.Reconnecting;
                await Task.Delay(retry_delay);
            }
        }
    }

    public async Task Disconnect()
    {
        if (ApiState.Value == Online.ApiState.Offline) return;
        Console.WriteLine("called manual disconnect");
        try
        {
            await connection.StopAsync();
            ApiState.Value = Online.ApiState.Offline;
        }
        catch (Exception e)
        {
            await Console.Error.WriteLineAsync("failed to disconnect: " + e);
        }
    }
}
