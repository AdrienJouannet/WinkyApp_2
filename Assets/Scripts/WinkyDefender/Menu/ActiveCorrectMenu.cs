using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCorrectMenu : MonoBehaviour {

	public void ActiveMenu (string name) {
		if (name != this.transform.name) {
			this.gameObject.SetActive (false);

			Transform[] parent = this.gameObject.transform.parent.GetComponentsInChildren<Transform> (true);

			foreach (Transform panel in parent) {
				if (panel.name == name) {
					panel.gameObject.SetActive (true);
					break;
				}
			}
		}
	}
}
