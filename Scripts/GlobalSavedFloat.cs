namespace HalfBlind.ScriptableVariables {
    using HalfBlind.Attributes;
    using HalfBlind.SaveUtils;
    using UnityEngine;

    public abstract class GlobalSavedFloat : GlobalFloat {
        [UniqueIdentifier]
        [SerializeField]
        private string _saveKey = null;

        protected float _backfield;
        protected bool _isLoaded;
        public abstract ISave GetSaveHandler();

        public override float Value {
            get {
                var saveHandler = GetSaveHandler();
                if (!_isLoaded) {
                    if (saveHandler != null) {
                        _isLoaded = true;
                        if(!saveHandler.Load<float>(_saveKey, out _backfield)) {
                            _backfield = _initialValue;
                        }
                    } else {
                        Debug.LogError($"Fail to find saveHandler");  
                    }
                }
                return _backfield;
            }
            set {
#if UNITY_EDITOR
                if (!Application.isPlaying) {
                    return;
                }
#endif
                var _saveSystem = GetSaveHandler();
                if (_saveSystem != null) {
                    if(_backfield != value) {
                        _backfield = value;
                        _saveSystem.Save<float>(_saveKey, _backfield);
                        TValueChanged(_backfield);
                        ValueChanged();
                    }
                }
            }
        }

        protected override void OnEnable() {
            _isLoaded = false;
            base.OnEnable();
        }
    }
}
