// Copyright (c) 2026 Takashi Yoshinaga. Licensed under the MIT License.

using Google.XR.Extensions;
using TMPro;
using UnityEngine;

namespace AndroidXRStarterKit
{
    /// <summary>
    /// Shows the state of the Geospatial API and where the device is on the Earth (latitude, longitude, altitude,
    /// and their accuracy) on a label. Use it to check that geospatial tracking works and to find the altitude to
    /// enter in GeospatialObjectPlacer.
    /// </summary>
    public class GeospatialStatusLabel : MonoBehaviour
    {
        [Tooltip("The XR Geospatial Manager in the scene.")]
        [SerializeField] XRGeospatialManager _geospatialManager;
        [Tooltip("The XR Origin's Main Camera, which is where the device is.")]
        [SerializeField] Transform _camera;
        [Tooltip("The label to show the status on.")]
        [SerializeField] TMP_Text _label;

        void Update()
        {
            var text = $"Geospatial: {XRGeospatialManager.State}\nLocation service: {Input.location.status}";

            var cameraPose = new Pose(_camera.position, _camera.rotation);
            if (_geospatialManager.TryLocate(cameraPose, out XRGeospatialPoseResult result) && result.IsPositionValid)
            {
                var pose = result.Pose;
                text += $"\nLat: {pose.Latitude:F6}  Lng: {pose.Longitude:F6}" +
                        $"\nAltitude: {pose.Altitude:F1} m" +
                        $"\nAccuracy: H {result.HorizontalAccuracy:F1} m / V {result.VerticalAccuracy:F1} m" +
                        $" / Yaw {result.OrientationYawAccuracy:F1} deg";
            }

            _label.text = text;
        }
    }
}
