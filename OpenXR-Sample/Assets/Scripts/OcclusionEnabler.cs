using UnityEngine;
using UnityEngine.Android;
using UnityEngine.XR.ARFoundation;

namespace AndroidXRStarterKit
{
    /// <summary>
    /// Turns on the AR Occlusion Manager after the user grants the permission Android XR requires for occlusion.
    /// Keep the AR Occlusion Manager component disabled in the scene; this script enables it.
    /// </summary>
    public class OcclusionEnabler : MonoBehaviour
    {
        const string OcclusionPermission = "android.permission.SCENE_UNDERSTANDING_FINE";

        [Tooltip("The AR Occlusion Manager on the XR Origin's Main Camera. Keep that component disabled in the scene.")]
        [SerializeField] AROcclusionManager _occlusionManager;

        void Start()
        {
            if (Application.platform != RuntimePlatform.Android || Permission.HasUserAuthorizedPermission(OcclusionPermission))
            {
                EnableOcclusion(restartSubsystem: false);
                return;
            }

            // If the user denies the permission, the AR Occlusion Manager simply stays disabled.
            var callbacks = new PermissionCallbacks();
            callbacks.PermissionGranted += _ => EnableOcclusion(restartSubsystem: true);
            Permission.RequestUserPermission(OcclusionPermission, callbacks);
        }

        void EnableOcclusion(bool restartSubsystem)
        {
            _occlusionManager.enabled = true;

            // Same as the Android XR package docs: restart the subsystem when the permission arrived after it was created.
            if (restartSubsystem && _occlusionManager.subsystem != null)
            {
                _occlusionManager.subsystem.Stop();
                _occlusionManager.subsystem.Start();
            }
        }
    }
}
