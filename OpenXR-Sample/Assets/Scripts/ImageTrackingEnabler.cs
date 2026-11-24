// Copyright (c) 2026 Takashi Yoshinaga. Licensed under the MIT License.

using UnityEngine;
using UnityEngine.Android;
using UnityEngine.XR.ARFoundation;

namespace AndroidXRStarterKit
{
    /// <summary>
    /// Turns on the AR Tracked Image Manager after the user grants the permission Android XR requires for image tracking.
    /// Keep the AR Tracked Image Manager component disabled in the scene; this script enables it.
    /// Put one in the scene, however many TrackedImageFollower components there are.
    /// </summary>
    public class ImageTrackingEnabler : MonoBehaviour
    {
        const string LogTag = "[ImageTrackingEnabler]";
        const string ImageTrackingPermission = "android.permission.SCENE_UNDERSTANDING_COARSE";

        [Tooltip("The AR Tracked Image Manager on the XR Origin. Keep that component disabled in the scene.")]
        [SerializeField] ARTrackedImageManager _imageManager;

        void Start()
        {
            if (Application.platform != RuntimePlatform.Android || Permission.HasUserAuthorizedPermission(ImageTrackingPermission))
            {
                _imageManager.enabled = true;
                return;
            }

            // If the user denies the permission, the AR Tracked Image Manager simply stays disabled.
            var callbacks = new PermissionCallbacks();
            callbacks.PermissionGranted += _ => _imageManager.enabled = true;
            callbacks.PermissionDenied += _ => Debug.LogWarning($"{LogTag} {ImageTrackingPermission} was denied. Image tracking stays off.");
            Permission.RequestUserPermission(ImageTrackingPermission, callbacks);
        }
    }
}
