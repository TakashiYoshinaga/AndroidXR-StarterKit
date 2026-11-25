// Copyright (c) 2026 Takashi Yoshinaga. Licensed under the MIT License.

using Google.XR.Extensions;
using UnityEngine;
using UnityEngine.Android;

namespace AndroidXRStarterKit
{
    /// <summary>
    /// Turns on the XR Geospatial Manager after the user grants the location permission the Geospatial API requires.
    /// Keep the XR Geospatial Manager component disabled in the scene; this script enables it.
    /// Google Cloud authentication runs on its own at startup, with the strategy set in the
    /// Android XR: Google Cloud feature (Project Settings > XR Plug-in Management > OpenXR).
    /// </summary>
    public class GeospatialEnabler : MonoBehaviour
    {
        const string LogTag = "[GeospatialEnabler]";

        [Tooltip("The XR Geospatial Manager in the scene. Keep that component disabled in the scene.")]
        [SerializeField] XRGeospatialManager _geospatialManager;

        void OnEnable()
        {
            XRGeospatialManager.OnStateChanged += OnStateChanged;
        }

        void OnDisable()
        {
            XRGeospatialManager.OnStateChanged -= OnStateChanged;
        }

        void Start()
        {
            if (Application.platform != RuntimePlatform.Android || Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                StartGeospatial();
                return;
            }

            // If the user denies the permission, the XR Geospatial Manager simply stays disabled.
            var callbacks = new PermissionCallbacks();
            callbacks.PermissionGranted += _ => StartGeospatial();
            callbacks.PermissionDenied += _ => Debug.LogWarning($"{LogTag} {Permission.FineLocation} was denied. Geospatial stays off.");
            Permission.RequestUserPermission(Permission.FineLocation, callbacks);
        }

        void StartGeospatial()
        {
            // The Geospatial API starts from the device's GPS location, so keep Unity's location service running too.
            if (!Input.location.isEnabledByUser)
            {
                Debug.LogWarning($"{LogTag} Location is turned off in the device settings.");
            }

            Input.location.Start();
            _geospatialManager.enabled = true;
        }

        void OnStateChanged(XRGeospatialStateChangedEventArgs args)
        {
            Debug.Log($"{LogTag} {args}");
            if (args.CurrState == XRGeospatialState.InitializationFailed)
            {
                // For example, a Google Cloud authentication error. See the Android XR: Google Cloud feature settings.
                Debug.LogWarning($"{LogTag} Initialization failed: {XRGeospatialManager.InitializationResult}");
            }
        }
    }
}
