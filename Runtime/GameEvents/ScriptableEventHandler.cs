using UnityEngine;
using UnityEngine.Events;

namespace HalfBlind.ScriptableVariables {
    /// <summary>
    /// Quick utility to do things with ScriptableGameEvents
    /// </summary>
    public sealed class ScriptableEventHandler : MonoBehaviour {
        [SerializeField]
        [Tooltip("Event to register with.")]
        private ScriptableGameEvent _event;

        [SerializeField]
        [Tooltip("Response to invoke when Event is raised.")]
        private UnityEvent _onEvent;

        private void Awake() => _event.AddListener(OnEvent);
        private void OnDestroy() => _event.RemoveListener(OnEvent);
        private void OnEvent() => _onEvent.Invoke();
    }
}