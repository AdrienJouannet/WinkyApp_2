using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Winky : MonoBehaviour {

	public float startHealth = 10;
	private float health;

	private bool damageTaken = false;

	[Header("Unity Stuff")]
	public Image healthBar;

	// Use this for initialization
	void Start () {
		health = startHealth;
	}

	// Update is called once per frame
	void Update () {
		if (damageTaken) {
			damageTaken = false;
//			Debug.Log ("Winky takes damage");
		}
	}

	public  void TakeDamage (float damage) {
		damageTaken = true;
		health -= damage;
		healthBar.fillAmount = health / startHealth;
		if (health <= 0) {
			health = 0;
			NOPE ();
		}
	}

	private void NOPE () {
		Debug.Log ("Winky died in terrible pain!");
//		UnityEditor.EditorApplication.isPlaying = false;
	}
}
