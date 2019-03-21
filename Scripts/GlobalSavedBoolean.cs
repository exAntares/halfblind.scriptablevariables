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
#if UNITY_EDITOR
                if (!Application.isPlaying) {
                    return _initialValue;
                }
#endif
                GetSaveHandler().Load<bool>(_saveKey, out _runtimeInstance._initialValue);
                return _runtimeInstance._initialValue;
            }
            set {
#if UNITY_EDITOR
                if (!Application.isPlaying) {
                    return;
                }
#endif
                var _saveSystem = GetSaveHandler();
                if (_saveSystem != null) {
                    _saveSystem.Save<bool>(_saveKey, value);
                    OnValueChanged?.Invoke();
                }
            }
        }
    }
}
