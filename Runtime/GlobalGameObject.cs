using UnityEngine;

namespace HalfBlind.ScriptableVariables {
	[CreateAssetMenu(fileName = nameof(GlobalGameObject), menuName = "Variables/"+nameof(GlobalGameObject))]
	public class GlobalGameObject : ScriptableVariable<GameObject> {
		public override void FromString(string value) {
			Value = GameObject.Find(value);
		}
	}
}
