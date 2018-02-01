using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLife : MonoBehaviour {

	public float startHealth = 10;
    public int moneyValue = 1;
    public float health;
	public Image healthBar;

    private bool _damageTaken;
    // Use this for initialization
	void Start () {
        health = startHealth;
	}

    private void Update()
    {
        if (_damageTaken)
        {
            _damageTaken = false;
        }
    }

    public void EnemyTakesDamage(float damage)
    {
//        print("enemy takes damage");
        _damageTaken = true;
        health -= damage;
		healthBar.fillAmount = health / startHealth;
//        print("Enemy health = " + health);
        if (health <= 0)
        {
            Destroy(gameObject);
//            print("Enemy destroyed !");
        }
    }
}
