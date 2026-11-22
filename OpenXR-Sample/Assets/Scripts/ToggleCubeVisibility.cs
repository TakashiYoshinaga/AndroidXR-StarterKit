// Copyright (c) 2026 Takashi Yoshinaga. Licensed under the MIT License.

using UnityEngine;

namespace AndroidXRStarterKit
{
    /// <summary>
    /// Shows the target object if it is hidden, and hides it if it is shown.
    /// Hook <see cref="Toggle"/> up to a Button's On Click event.
    /// </summary>
    public class ToggleCubeVisibility : MonoBehaviour
    {
        [SerializeField] GameObject _target;

        public void Toggle()
        {
            _target.SetActive(!_target.activeSelf);
        }
    }
}
