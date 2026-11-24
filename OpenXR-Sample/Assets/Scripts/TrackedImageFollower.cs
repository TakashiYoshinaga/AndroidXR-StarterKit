// Copyright (c) 2026 Takashi Yoshinaga. Licensed under the MIT License.

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace AndroidXRStarterKit
{
    /// <summary>
    /// Makes a target object follow one tracked image, chosen by its name in the reference image library.
    /// The target is placed at an offset from the image and takes the image's orientation; its size is left to the
    /// target itself. Add one TrackedImageFollower per marker.
    /// The scene also needs an ImageTrackingEnabler, which starts the AR Tracked Image Manager.
    /// </summary>
    public class TrackedImageFollower : MonoBehaviour
    {
        /// <summary>
        /// What the target does while the image is not being tracked.
        /// </summary>
        public enum LostBehavior
        {
            /// <summary>Hide the target until the image is tracked again.</summary>
            Hide,

            /// <summary>Keep the target visible where it was last placed.</summary>
            Stay,
        }

        const string LogTag = "[TrackedImageFollower]";

        [Tooltip("The AR Tracked Image Manager on the XR Origin.")]
        [SerializeField] ARTrackedImageManager _imageManager;
        [Tooltip("Name of the reference image to follow, as written in the reference image library. " +
                 "Leave it empty to follow any image.")]
        [SerializeField] string _imageName;
        [Tooltip("The object that follows the tracked image, for example the Cube. Its size is not changed.")]
        [SerializeField] Transform _target;
        [Tooltip("What the target does while the image is not being tracked.")]
        [SerializeField] LostBehavior _whenLost = LostBehavior.Hide;
        [Tooltip("Where the target sits relative to the image's center, in meters, in the image's local space. " +
                 "Y points out of the image's surface; X and Z lie in the image's plane.")]
        [SerializeField] Vector3 _offset;

        TrackableId _lastLoggedId;
        TrackingState? _lastLoggedState;

        void Start()
        {
            _target.gameObject.SetActive(false);
        }

        void Update()
        {
            var trackedImage = FindTrackedImage();
            LogChange(trackedImage);

            if (trackedImage == null || trackedImage.trackingState != TrackingState.Tracking)
            {
                // The runtime lost sight of the image. A lost image keeps its last pose, so doing nothing here
                // leaves the target where it was.
                if (_whenLost == LostBehavior.Hide)
                {
                    _target.gameObject.SetActive(false);
                }

                return;
            }

            // The tracked image lies in its local XZ plane and its local Y axis is the image's normal.
            var imageTransform = trackedImage.transform;
            _target.SetPositionAndRotation(
                imageTransform.position + imageTransform.rotation * _offset,
                imageTransform.rotation);
            _target.gameObject.SetActive(true);
        }

        /// <summary>
        /// Returns the image to follow among the images that match the image name. An image that is being tracked
        /// now wins over one the runtime lost sight of (Limited), which keeps its last pose.
        /// </summary>
        ARTrackedImage FindTrackedImage()
        {
            ARTrackedImage limitedImage = null;
            foreach (var image in _imageManager.trackables)
            {
                if (!string.IsNullOrEmpty(_imageName) && image.referenceImage.name != _imageName)
                {
                    continue;
                }

                switch (image.trackingState)
                {
                    case TrackingState.Tracking:
                        return image;
                    case TrackingState.Limited:
                        limitedImage ??= image;
                        break;
                }
            }

            return limitedImage;
        }

        /// <summary>
        /// Logs when the followed image or its tracking state changes, so a stop in tracking shows up in the log.
        /// </summary>
        void LogChange(ARTrackedImage trackedImage)
        {
            TrackingState? state = trackedImage == null ? null : trackedImage.trackingState;
            var id = trackedImage == null ? default : trackedImage.trackableId;
            if (state == _lastLoggedState && id == _lastLoggedId)
            {
                return;
            }

            _lastLoggedState = state;
            _lastLoggedId = id;
            Debug.Log(trackedImage == null
                ? $"{LogTag} ({name}) No tracked image."
                : $"{LogTag} ({name}) '{trackedImage.referenceImage.name}' ({id}) state={state}, size={trackedImage.size}");
        }
    }
}
