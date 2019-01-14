namespace HalfBlind.ScriptableVariables {
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = "OnGameEvent", menuName = "GameEvents/ScriptableEvent")]
    public class ScriptableGameEvent : ScriptableObject {
        private HashSet<Action> _callbacks = new HashSet<Action>();

        private void OnEnable() {
            _callbacks.Clear();
        }

        private void OnDisable() {
            _callbacks.Clear();
        }

        [Sirenix.OdinInspector.HideInEditorMode]
        //[Sirenix.OdinInspector.Button(Sirenix.OdinInspector.ButtonSizes.Gigantic)]
        public virtual void SendEvent() {
            foreach (var callback in _callbacks) {
                callback();
            }
        }

        public void AddListener(Action callback) {
            _callbacks.Add(callback);
        }

        public void RemoveListener(Action callback) {
            _callbacks.Remove(callback);
        }
    }
}
