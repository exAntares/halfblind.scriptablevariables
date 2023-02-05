using UnityEngine;

namespace HalfBlind.ScriptableVariables {
	public class GlobalGameObjectSetter : MonoBehaviour {
		[SerializeField] private GlobalGameObject _globalGameObject;
		public void Awake() {
			_globalGameObject.Value = gameObject;
		}
	}
}
