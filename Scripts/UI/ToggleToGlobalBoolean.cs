using UnityEngine;
using UnityEngine.UI;

namespace HalfBlind.ScriptableVariables {
    public class ToggleToGlobalBoolean : MonoBehaviour {
        [SerializeField] private Toggle _toggle;
        [SerializeField] private GlobalBoolean _globalBoolean;
        
        private void Awake() {
            _toggle.isOn = _globalBoolean.Value;
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDestroy() => _toggle.onValueChanged.RemoveListener(OnValueChanged);

        private void Reset() => _toggle = GetComponent<Toggle>();

        private void OnValueChanged(bool isOn) {
            if (_globalBoolean.Value != isOn) {
                _globalBoolean.Value = isOn;
            }
        }
    }
}
