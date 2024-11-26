using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using osu.Framework.Bindables;

namespace Blackjack.Game.Online;

public class APIAccess
{
    public static HubConnection Connection { get; } = new HubConnectionBuilder().WithUrl("http://localhost:5183/online").WithAutomaticReconnect().Build();

    public Task Connect()
    {
        return Connection.StartAsync();
    }

    public Task Disconnect()
    {
        return Connection.StopAsync();
    }
}
