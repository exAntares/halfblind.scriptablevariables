using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace HalfBlind.ScriptableVariables {
    using HalfBlind.Attributes;
    using HalfBlind.SaveUtils;

    [CreateAssetMenu(fileName = "Global Cooldown", menuName = "Variables/GlobalCooldown")]
    public abstract class GlobalCooldown : ScriptableObject {
        [UniqueIdentifier]
        [SerializeField]
        private string _saveKey = null;
        [SerializeField, HideInPlayMode]
        private long _InitialCooldownInSeconds;

        [ShowInInspector, HideInEditorMode]
        public long CooldownInSeconds { get; set; }

        private long? SavedStartTicks;

        public Action OnCooldownStarted;

        public string SaveKey { get { return _saveKey; } }

        protected virtual void OnEnable() {
            CooldownInSeconds = _InitialCooldownInSeconds;
        }

        #region Inspector
        private double RuntimeCooldownInSecondsDouble => CooldownInSeconds;
        [ShowInInspector, HideInEditorMode, HideLabel, ProgressBar(0, "RuntimeCooldownInSecondsDouble")]
        private double RemainingSeconds { get { return GetRemainingSecondsTime(); } }
        #endregion

        public abstract ISave GetSaveHandler();

        public double GetRemainingSecondsTime() {
            var saveSystem = GetSaveHandler();
            if (!SavedStartTicks.HasValue) {
                if (saveSystem == null) { return 0; }
                SavedStartTicks = GetSavedStartTimeTicks(saveSystem, _saveKey);
            }

            double elapsedSeconds = (DateTime.Now - new DateTime(SavedStartTicks.Value)).TotalSeconds;
            var diffTime = CooldownInSeconds - elapsedSeconds;
            return diffTime > 0 ? diffTime : 0;
        }

        [Button]
        public void ReduceCooldown(double secondsToReduce) {
#if UNITY_EDITOR
            if (!Application.isPlaying) { return; }
#endif
            if (!IsOnCooldown()) { return; }
            secondsToReduce = secondsToReduce > 0 ? -secondsToReduce : secondsToReduce;
            var savedStartTimeTicks = GetSavedStartTimeTicks(GetSaveHandler(), _saveKey);
            SavedStartTicks = new DateTime(savedStartTimeTicks).AddSeconds(secondsToReduce).Ticks;
            GetSaveHandler().Save<long>(_saveKey, SavedStartTicks.Value);
        }

        [HideInEditorMode]
        [Button]
        public void StartCooldown() {
            if (IsOnCooldown()) {
                Debug.Log($"{nameof(StartCooldown)} cooldown was already started: {GetRemainingSecondsTime()}s ago and lasts {CooldownInSeconds}");
                return;
            }
            SavedStartTicks = SaveStartTimeTicks(GetSaveHandler(), _saveKey, CooldownInSeconds);
            OnCooldownStarted?.Invoke();
            Debug.Log($"{name} Cooldown Started at:{new DateTime(SavedStartTicks.Value)} duration: {CooldownInSeconds}s");
        }

        [HideInEditorMode]
        [Button]
        public bool IsOnCooldown() {
            return GetRemainingSecondsTime() > 0;
        }

        private static long SaveStartTimeTicks(ISave saveSystem, string saveKey, long cooldownSeconds) {
            var startTimeTicks = DateTime.Now.Ticks;
            if(saveSystem != null) {
                saveSystem.Save<long>(saveKey, startTimeTicks);
            } else {
                Debug.LogError($"Missing {nameof(ISave)}");
            }
            
            return startTimeTicks;
        }

        private static long GetSavedStartTimeTicks(ISave saveSystem, string saveKey) {
            long lastTime = 0;
            if (saveSystem != null) {
                saveSystem.Load<long>(saveKey, out lastTime);
            } else {
                Debug.LogError($"Missing {nameof(ISave)}");
            }
            
            return lastTime;
        }
    }
}
