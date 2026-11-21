using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace AndroidXRStarterKit
{
    /// <summary>
    /// Keeps the object from rotating (and, optionally, from moving) while it is grabbed with both hands,
    /// so that a two-handed grab only scales it.
    /// With one hand, the object is moved and rotated as usual.
    /// </summary>
    /// <remarks>
    /// Requires the XR Grab Interactable to use Select Mode = Multiple, and an XR General Grab Transformer
    /// with Allow Two Handed Scaling turned on. This component only switches
    /// <see cref="XRGrabInteractable.trackRotation"/> and <see cref="XRGrabInteractable.trackPosition"/>
    /// while two or more interactors are selecting the object, and restores them afterwards.
    /// </remarks>
    [RequireComponent(typeof(XRGrabInteractable))]
    public class TwoHandsScaleController : MonoBehaviour
    {
        [SerializeField, Tooltip("The grab interactable to control. Taken from this GameObject if left empty.")]
        XRGrabInteractable _grabInteractable;

        [SerializeField, Tooltip("Also stop following the hands' position while grabbed with both hands. " +
                                 "If off, the object keeps moving, but its position can jump slightly (a few centimeters) " +
                                 "at the moment the second hand grabs it.")]
        bool _freezePosition = true;

        bool _frozen;
        bool _positionFrozen;
        bool _savedTrackRotation;
        bool _savedTrackPosition;

        void Awake()
        {
            if (_grabInteractable == null)
                _grabInteractable = GetComponent<XRGrabInteractable>();
        }

        void OnEnable()
        {
            _grabInteractable.selectEntered.AddListener(OnSelectEntered);
            _grabInteractable.selectExited.AddListener(OnSelectExited);
            Apply();
        }

        void OnDisable()
        {
            _grabInteractable.selectEntered.RemoveListener(OnSelectEntered);
            _grabInteractable.selectExited.RemoveListener(OnSelectExited);
            Restore();
        }

        void OnSelectEntered(SelectEnterEventArgs args) => Apply();

        void OnSelectExited(SelectExitEventArgs args) => Apply();

        void Apply()
        {
            if (_grabInteractable.interactorsSelecting.Count >= 2)
                Freeze();
            else
                Restore();
        }

        void Freeze()
        {
            if (_frozen)
                return;

            _frozen = true;
            _savedTrackRotation = _grabInteractable.trackRotation;
            _savedTrackPosition = _grabInteractable.trackPosition;

            _grabInteractable.trackRotation = false;

            _positionFrozen = _freezePosition;
            if (_positionFrozen)
                _grabInteractable.trackPosition = false;
        }

        void Restore()
        {
            if (!_frozen)
                return;

            _frozen = false;
            _grabInteractable.trackRotation = _savedTrackRotation;

            if (_positionFrozen)
                _grabInteractable.trackPosition = _savedTrackPosition;
            _positionFrozen = false;
        }
    }
}
