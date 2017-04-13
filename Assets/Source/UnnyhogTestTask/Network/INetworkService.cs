using System;

namespace UnnyhogTestTask.Network
{
    public interface INetworkService
    {
        event Action OnServerStarted;
        event Action OnClientJoined;
        event Action OnClientLeft;

        event Action OnConnectedToServer;
        event Action<string> OnConnectionFail;
        event Action OnDisconnectedFromServer;

        void StartServer();

        void Connect(string address, int port);

        string GetServerInfo();

        void SendSocketMessage();
    }
}
