using System;
using UnityEngine;

namespace HalfBlind.ScriptableVariables {
    public class ScriptableEventSender : MonoBehaviour {
        [Flags]
        private enum SendMode {
            Awake = 1 << 0,
            Start = 1 << 1,
            OnEnable = 1 << 2,
            OnDisable = 1 << 3,
            OnDestroy = 1 << 4,
            Update = 1 << 5,
        }
        
        [SerializeField] private ScriptableGameEvent _event = null!;
        [SerializeField] private SendMode _sendMode;

        private void Awake() {
            if (_sendMode.HasFlag(SendMode.Awake)) {
                _event.SendEvent();
            }
        }

        private void Start() {
            if (_sendMode.HasFlag(SendMode.Start)) {
                _event.SendEvent();
            }
        }

        private void OnEnable() {
            if (_sendMode.HasFlag(SendMode.OnEnable)) {
                _event.SendEvent();
            }
        }

        private void OnDisable() {
            if (_sendMode.HasFlag(SendMode.OnDisable)) {
                _event.SendEvent();
            }
        }

        private void OnDestroy() {
            if (_sendMode.HasFlag(SendMode.OnDestroy)) {
                _event.SendEvent();
            }
        }

        private void Update() {
            if (_sendMode.HasFlag(SendMode.Update)) {
                _event.SendEvent();
            }
        }
    }
}
