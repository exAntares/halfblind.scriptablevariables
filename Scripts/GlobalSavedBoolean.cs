namespace HalfBlind.ScriptableVariables {
    using HalfBlind.Attributes;
    using HalfBlind.SaveUtils;
    using UnityEngine;

    public abstract class GlobalSavedBoolean : GlobalBoolean {
        [UniqueIdentifier]
        [SerializeField]
        private string _saveKey = null;

        public abstract ISave GetSaveHandler();

        public override bool Value {
            get {
                if (!Application.isPlaying) {
                    return _initialValue;
                }

                GetSaveHandler()?.Load<bool>(_saveKey, out _runtimeInstance._initialValue);
                return _runtimeInstance._initialValue;
            }
            set {
                if (Application.isPlaying) {
                    var _saveSystem = GetSaveHandler();
                    if (_saveSystem != null) {
                        var oldValue = Value;
                        _saveSystem.Save<bool>(_saveKey, value);
                        OnValueChanged?.Invoke();
                    }
                }
            }
        }
    }
}
