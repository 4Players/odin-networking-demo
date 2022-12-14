using UnityEngine;

namespace Odin.OdinNetworking
{
    /// <summary>
    /// Start position for player spawning, automatically registers itself in the OdinNetworkManager.
    /// </summary>
    [DisallowMultipleComponent]
    public class OdinNetworkSpawnPosition : MonoBehaviour
    {
        void Awake()
        {
            OdinNetworkManager.RegisterSpawnPosition(this.transform);
        }

        private void OnDestroy()
        {
            OdinNetworkManager.UnregisterSpawnPosition(this.transform);
        }
    }
}
