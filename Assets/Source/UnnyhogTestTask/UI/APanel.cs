using UnityEngine;
using UnnyhogTestTask.Core;
using UnnyhogTestTask.Network;

namespace UnnyhogTestTask.UI
{
    public abstract class APanel : MonoBehaviour
    {
        protected INetworkService NetworkService { get; private set; }

        protected virtual void Start()
        {
            NetworkService = ServiceLocator.GetService<INetworkService>();
        }

        protected virtual void OnDestroy()
        {
            
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
