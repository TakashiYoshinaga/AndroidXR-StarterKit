using TMPro;
using UnityEngine;

namespace AndroidXRStarterKit
{
    /// <summary>
    /// Shows the text typed into an input field in a text label.
    /// </summary>
    public class ShowInputText : MonoBehaviour
    {
        [SerializeField] TMP_InputField _inputField;
        [SerializeField] TMP_Text _label;

        void OnEnable()
        {
            _inputField.onValueChanged.AddListener(OnValueChanged);
            OnValueChanged(_inputField.text);
        }

        void OnDisable()
        {
            _inputField.onValueChanged.RemoveListener(OnValueChanged);
        }

        void OnValueChanged(string text)
        {
            _label.text = text;
        }
    }
}
