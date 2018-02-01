using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteAction : MonoBehaviour {

	public void RedCross (){
		Image Command = this.transform.GetChild (0).GetComponent<Image> ();
		if (Command) {
			Command.overrideSprite = null;
			Command.name = "Empty";
			this.transform.Find ("RedCross").transform.gameObject.SetActive (false);
		}
	}
}
