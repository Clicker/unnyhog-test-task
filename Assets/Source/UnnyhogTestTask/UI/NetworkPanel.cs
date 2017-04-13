using UnityEngine.UI;

namespace UnnyhogTestTask.UI
{
    public class NetworkPanel : APanel
    {
        public Text Text;

        protected override void Start()
        {
            base.Start();

            NetworkService.OnServerStarted += OnServerStarted;

            Hide();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            NetworkService.OnServerStarted -= OnServerStarted;
        }

        private void OnServerStarted()
        {
            Text.text = NetworkService.GetServerInfo();

            Show();
        }
    }
}
