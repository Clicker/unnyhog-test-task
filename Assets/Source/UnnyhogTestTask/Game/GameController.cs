using UnityEngine;
using UnnyhogTestTask.Core;
using UnnyhogTestTask.Network;

namespace UnnyhogTestTask.Game
{
    public class GameController : MonoBehaviour, IGameService
    {
        private void Awake()
        {
            ServiceLocator.AddService<IGameService>(this);
        }

        private void OnDestroy()
        {
            ServiceLocator.RemoveService<IGameService>();
        }
    }
}
