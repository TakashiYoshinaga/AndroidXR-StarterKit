using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.XR.ARFoundation;

namespace AndroidXRStarterKit
{
    /// <summary>
    /// Accesses the device's cameras through WebCamTexture.
    /// Requests the CAMERA permission, lists the available cameras, shows the one selected in the Inspector
    /// on a renderer that follows the image's aspect ratio, and reports whether AR Foundation exposes camera images.
    /// Results go to Debug.Log and an optional label.
    /// </summary>
    public class CameraAccess : MonoBehaviour
    {
        const string LogTag = "[CameraAccess]";
        const int RequestedFps = 30;

        /// <summary>
        /// Which camera to open. Each value is the index into WebCamTexture.devices.
        /// </summary>
        public enum CameraSource
        {
            /// <summary>Index 0: the normal camera.</summary>
            Normal = 0,

            /// <summary>Index 1: the Android XR avatar camera.</summary>
            Avatar = 1,
        }

        [Tooltip("Normal = devices[0] (default). Avatar = devices[1] (Android XR avatar camera).")]
        [SerializeField] CameraSource _cameraSource = CameraSource.Normal;
        [Tooltip("Requested capture size. The device picks the closest size it supports; the actual size is logged.")]
        [SerializeField] Vector2Int _requestedSize = new Vector2Int(1280, 720);
        [Tooltip("Optional. Used only to report whether the AR Foundation camera subsystem supports camera images.")]
        [SerializeField] ARCameraManager _arCameraManager;
        [Tooltip("Renderer (for example the Quad) that shows the WebCamTexture. Use an Unlit material. Its width is adjusted to the image's aspect ratio.")]
        [SerializeField] Renderer _targetRenderer;
        [Tooltip("Optional. Shows the log inside the headset.")]
        [SerializeField] TMP_Text _logLabel;

        readonly StringBuilder _log = new StringBuilder();
        WebCamTexture _webCam;

        IEnumerator Start()
        {
            Log($"Platform={Application.platform}, Unity={Application.unityVersion}");

            if (Application.platform == RuntimePlatform.Android && !Permission.HasUserAuthorizedPermission(Permission.Camera))
            {
                Log("Requesting android.permission.CAMERA...");
                var finished = false;
                var callbacks = new PermissionCallbacks();
                callbacks.PermissionGranted += _ => finished = true;
                // PermissionDenied also covers "Deny and don't ask again" and a dismissed dialog,
                // because we do not subscribe to the obsolete PermissionDeniedAndDontAskAgain event.
                callbacks.PermissionDenied += _ => finished = true;
                Permission.RequestUserPermission(Permission.Camera, callbacks);
                yield return new WaitUntil(() => finished);
            }

            if (Application.platform == RuntimePlatform.Android)
            {
                Log($"CAMERA permission granted = {Permission.HasUserAuthorizedPermission(Permission.Camera)}");
            }

            // Wait a frame so the AR Foundation subsystem has been created before we inspect it.
            yield return null;
            ReportARFoundation();

            var devices = WebCamTexture.devices;
            Log($"WebCamTexture.devices = {devices.Length}");
            foreach (var device in devices)
            {
                var resolutions = device.availableResolutions?.Length ?? 0;
                Log($"  name='{device.name}' frontFacing={device.isFrontFacing} resolutions={resolutions}");
            }

            var index = (int)_cameraSource;
            if (index >= devices.Length)
            {
                Log($"{_cameraSource} (devices[{index}]) is not available. Only {devices.Length} device(s) found.");
                yield break;
            }

            var selected = devices[index];
            _webCam = new WebCamTexture(selected.name, _requestedSize.x, _requestedSize.y, RequestedFps);
            _webCam.Play();
            Log($"Selected {_cameraSource} (devices[{index}]): '{selected.name}', requested {_requestedSize.x}x{_requestedSize.y}");

            // WebCamTexture reports a 16x16 size until the first frame arrives.
            var timeout = Time.realtimeSinceStartup + 5f;
            yield return new WaitUntil(() => _webCam.width > 16 || Time.realtimeSinceStartup > timeout);
            if (_webCam.width <= 16)
            {
                Log("No image received within 5 seconds.");
                yield break;
            }

            Log($"Image size: {_webCam.width}x{_webCam.height}");

            if (_targetRenderer != null)
            {
                _targetRenderer.material.mainTexture = _webCam;
                FitAspectRatio(_targetRenderer.transform, _webCam.width, _webCam.height);
            }
        }

        void OnDestroy()
        {
            if (_webCam != null)
            {
                _webCam.Stop();
                Destroy(_webCam);
            }
        }

        /// <summary>
        /// Keeps the target's height (local scale Y) and sets its width (local scale X) to match the image's aspect ratio.
        /// </summary>
        static void FitAspectRatio(Transform target, int imageWidth, int imageHeight)
        {
            var scale = target.localScale;
            scale.x = scale.y * imageWidth / imageHeight;
            target.localScale = scale;
        }

        void ReportARFoundation()
        {
            if (_arCameraManager == null)
            {
                Log("ARCameraManager is not assigned (skipped).");
                return;
            }

            var descriptor = _arCameraManager.descriptor;
            Log(descriptor == null
                ? "ARCameraManager: XRCameraSubsystem is not running."
                : $"ARCameraManager: supportsCameraImage={descriptor.supportsCameraImage}");
        }

        void Log(string message)
        {
            Debug.Log($"{LogTag} {message}");
            if (_logLabel == null)
            {
                return;
            }

            _log.AppendLine(message);
            _logLabel.text = _log.ToString();
        }
    }
}
