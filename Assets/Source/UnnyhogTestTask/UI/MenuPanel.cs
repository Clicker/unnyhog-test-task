using UnityEngine.UI;

namespace UnnyhogTestTask.UI
{
    public class MenuPanel : APanel
    {
        public Button StartServerButton;
        public Button ConnectButton;
        public InputField AddressField;
        public InputField PortField;
        public Text ErrorText;

        protected override void Start()
        {
            base.Start();

            StartServerButton.onClick.AddListener(OnStartServer);
            ConnectButton.onClick.AddListener(OnConnect);

            NetworkService.OnServerStarted += OnServerStarted;
            NetworkService.OnConnectedToServer += OnConnectedToServer;
            NetworkService.OnConnectionFail += OnConnectionFail;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            StartServerButton.onClick.RemoveListener(OnStartServer);
            ConnectButton.onClick.RemoveListener(OnConnect);

            NetworkService.OnServerStarted -= OnServerStarted;
            NetworkService.OnConnectedToServer -= OnConnectedToServer;
            NetworkService.OnConnectionFail -= OnConnectionFail;
        }

        public override void Hide()
        {
            base.Hide();

            ErrorText.text = string.Empty;
        }

        public void OnStartServer()
        {
            NetworkService.StartServer();
        }

        public void OnConnect()
        {
            string address = AddressField.text;
            int port;

            if (int.TryParse(PortField.text, out port))
            {
                NetworkService.Connect(address, port);
            }
        }

        private void OnServerStarted()
        {
            Hide();
        }

        private void OnConnectedToServer()
        {
            Hide();

            NetworkService.SendSocketMessage();
        }

        private void OnConnectionFail(string error)
        {
            ErrorText.text = "Connection failed: " + error;
        }
    }
}
