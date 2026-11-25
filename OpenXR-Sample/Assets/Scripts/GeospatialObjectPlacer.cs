// Copyright (c) 2026 Takashi Yoshinaga. Licensed under the MIT License.

using Google.XR.Extensions;
using UnityEngine;

namespace AndroidXRStarterKit
{
    /// <summary>
    /// Places a target object at a given latitude, longitude, and altitude with the Geospatial API.
    /// The target stays hidden until the device knows where it is on the Earth.
    /// The scene also needs a GeospatialEnabler, which starts the XR Geospatial Manager.
    /// </summary>
    public class GeospatialObjectPlacer : MonoBehaviour
    {
        [Tooltip("The XR Geospatial Manager in the scene.")]
        [SerializeField] XRGeospatialManager _geospatialManager;
        [Tooltip("The object to place, for example the Cube.")]
        [SerializeField] Transform _target;
        [Tooltip("Latitude in degrees, for example from Google Maps.")]
        [SerializeField] double _latitude = 35.681236;
        [Tooltip("Longitude in degrees, for example from Google Maps.")]
        [SerializeField] double _longitude = 139.767125;
        [Tooltip("Altitude in meters above the WGS84 ellipsoid, not above sea level. " +
                 "The easiest way is to stand near the spot, read your altitude on the GeospatialStatusLabel, " +
                 "and use a value about 1.5 m lower so the target sits near the ground.")]
        [SerializeField] double _altitude = 40;

        void Start()
        {
            _target.gameObject.SetActive(false);
        }

        void Update()
        {
            // Converts the geospatial pose to Unity world space every frame, so the target follows
            // as the device's estimate of its own location gets more accurate.
            var geospatialPose = new XRGeospatialPose(Quaternion.identity, _latitude, _longitude, _altitude);
            if (!_geospatialManager.TryLocate(geospatialPose, out XRPoseResult result) || !result.IsPositionValid)
            {
                return;
            }

            _target.position = result.Pose.position;
            _target.gameObject.SetActive(true);
        }
    }
}
