namespace HalfBlind.ScriptableVariables {
    using HalfBlind.Attributes;
    using HalfBlind.SaveUtils;
    using UnityEngine;

    public abstract class GlobalSavedFloat : GlobalFloat {
        [UniqueIdentifier]
        [SerializeField]
        private string _saveKey = null;

        public abstract ISave GetSaveHandler();

        public override float Value {
            get {
                if (!Application.isPlaying) {
                    return _initialValue;
                }

                GetSaveHandler()?.Load<float>(_saveKey, out _runtimeValue);
                return _runtimeValue;
            }
            set {
                if (Application.isPlaying) {
                    var _saveSystem = GetSaveHandler();
                    if(_saveSystem != null) {
                        _saveSystem.Save<float>(_saveKey, value);
                        OnValueChanged?.Invoke();
                    }
                }
            }
        }
    }
}
