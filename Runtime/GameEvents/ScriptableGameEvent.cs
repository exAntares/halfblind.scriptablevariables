﻿using HalfBlind.Attributes;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HalfBlind.ScriptableVariables {
    [CreateAssetMenu(fileName = "OnGameEvent", menuName = "GameEvents/ScriptableEvent")]
    public class ScriptableGameEvent : ScriptableObject {
        private readonly HashSet<Action> _callbacks = new HashSet<Action>();
        [SerializeField, StringButton(nameof(SendEvent), 50, StringButtonAttribute.Visibility.OnlyPlayMode)]
        private string _hiddenButton = "_button";

        private void OnEnable() => _callbacks.Clear();

        private void OnDisable() => _callbacks.Clear();

        public virtual void SendEvent() {
            var callbacks = new HashSet<Action>(_callbacks);
            foreach (var callback in callbacks) {
                callback();
            }
        }

        public void AddListener(Action callback) => _callbacks.Add(callback);

        public void RemoveListener(Action callback) => _callbacks.Remove(callback);
    }
}
