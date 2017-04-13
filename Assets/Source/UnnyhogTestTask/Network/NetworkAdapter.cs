using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Networking;
using UnnyhogTestTask.Core;

namespace UnnyhogTestTask.Network
{
    public class NetworkAdapter : MonoBehaviour, INetworkService
    {
        private ConnectionConfig _connectionConfig;
        private int _reliableChannelId;

        private int _socketId;
        private int _socketPort = 8888;

        private int _connectionId;

        public event Action OnServerStarted;
        public event Action OnClientJoined;
        public event Action OnClientLeft;

        public event Action OnConnectedToServer;
        public event Action<string> OnConnectionFail;
        public event Action OnDisconnectedFromServer;

        private void Awake()
        {
            ServiceLocator.AddService<INetworkService>(this);
        }

        private void OnDestroy()
        {
            ServiceLocator.RemoveService<INetworkService>();
        }

        private void Start()
        {
            NetworkTransport.Init();

            _connectionConfig = new ConnectionConfig();
            _reliableChannelId = _connectionConfig.AddChannel(QosType.Reliable);
        }

        public void StartServer()
        {
            var topology = new HostTopology(_connectionConfig, 1);

            _socketId = NetworkTransport.AddHost(topology, _socketPort);

            Debug.Log("Socket Open. Socket id: " + _socketId);

            if (OnServerStarted != null)
            {
                OnServerStarted.Invoke();
            }
        }
        
        public void Connect(string address, int port)
        {
            var topology = new HostTopology(_connectionConfig, 1);

            _socketId = NetworkTransport.AddHost(topology);

            byte error;
            _connectionId = NetworkTransport.Connect(_socketId, address, port, 0, out error);

            NetworkError errorType = (NetworkError) error;
            
            if (errorType == NetworkError.Ok)
            {
                Debug.Log("Connected to server. Connection id: " + _connectionId);

                if (OnConnectedToServer != null)
                {
                    OnConnectedToServer.Invoke();
                }
            }
            else
            {
                Debug.LogError("Connection failed with errorType: " + _connectionId);

                if (OnConnectionFail != null)
                {
                    OnConnectionFail.Invoke(errorType.ToString());
                }
            }
        }

        public string GetServerInfo()
        {
            return string.Format("{0} : {1}", UnityEngine.Network.player.ipAddress, UnityEngine.Network.player.port); ;
        }

        public void SendSocketMessage()
        {
            byte error;
            byte[] buffer = new byte[1024];
            Stream stream = new MemoryStream(buffer);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, "HelloServer");

            int bufferSize = 1024;

            NetworkTransport.Send(_socketId, _connectionId, _reliableChannelId, buffer, bufferSize, out error);
        }

        private void Update()
        {
            int recHostId;
            int recConnectionId;
            int recChannelId;
            byte[] recBuffer = new byte[1024];
            int bufferSize = 1024;
            int dataSize;
            byte error;
            NetworkEventType recNetworkEvent = NetworkTransport.Receive(out recHostId, out recConnectionId, out recChannelId, recBuffer, bufferSize, out dataSize, out error);

            switch (recNetworkEvent)
            {
                case NetworkEventType.Nothing:
                    break;
                case NetworkEventType.ConnectEvent:
                    Debug.Log("incoming connection event received");
                    break;
                case NetworkEventType.DataEvent:
                    Stream stream = new MemoryStream(recBuffer);
                    BinaryFormatter formatter = new BinaryFormatter();
                    string message = formatter.Deserialize(stream) as string;
                    Debug.Log("incoming message event received: " + message);
                    break;
                case NetworkEventType.DisconnectEvent:
                    Debug.Log("remote client event disconnected");
                    break;
            }
        }
    }
}
