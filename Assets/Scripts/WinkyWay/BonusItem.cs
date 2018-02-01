using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusItem : MonoBehaviour
{

	public Vector3 toScale;
	private WaveSpawner spawn;
	Vector3 _target; // going straight to the right zone
	BoxCollider2D _itemCol;
	public float speed;


	// Start code should be in Awake but because of a bug of Unity2017 I have to use Start instead 
	private void Start()
	{
		_itemCol = GetComponent<BoxCollider2D>(); //disabled at start
		_itemCol.enabled = false;
		spawn = GameObject.Find("SpawnerManager").GetComponent<WaveSpawner>();
		_target = new Vector3(transform.position.x, transform.position.y, 0);
		print("speed  = " + speed);

		StartCoroutine(MoveThisObject(_target, speed));
	}

	IEnumerator MoveThisObject(Vector3 target, float moveDuration)
	{
		// store the starting position of the object this script is attached to as well as the target position
		Vector3 oldPos = transform.position;
		Vector3 newPos = target;
		float moveTime = 0.0f;

		while (moveTime < moveDuration)
		{
			moveTime += Time.deltaTime;
			transform.localScale = Vector3.Lerp(Vector3.zero, toScale, moveTime / (moveDuration - 0.5f));
			transform.position = Vector3.Lerp(oldPos, newPos, moveTime / moveDuration);
			if ((moveTime + 0.5) >= moveDuration)
				_itemCol.enabled = true;
			yield return null;
		}
		Destroy(gameObject);
	}
}
