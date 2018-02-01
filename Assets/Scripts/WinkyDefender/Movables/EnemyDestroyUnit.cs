using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroyUnit : MonoBehaviour {

	public void CheckDestroyUnit (Vector3 position) {
		foreach (Transform unit in this.transform) {
			if (unit.transform.position == position) {
				unit.gameObject.SetActive (false);
				return;
			}
		}
	}
}
