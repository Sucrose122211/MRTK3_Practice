using UnityEngine;
using UnityEngine.AddressableAssets;

public class SelfCleanup : MonoBehaviour
{
    private void OnDestroy() {
        Addressables.ReleaseInstance(gameObject);
    }
}