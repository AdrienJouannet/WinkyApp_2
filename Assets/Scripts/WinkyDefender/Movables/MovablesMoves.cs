using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablesMoves : MonoBehaviour {

	public float moveSpeed = 10.0f;
	public float time = 0.2f;

	private float _startTime;
	private float _journeyLength;

	public IEnumerator MoveFromTo (Transform toMove, Vector3 startPosition, Vector3 endPosition, MovableScript script) {
		_startTime = Time.time;
		_journeyLength = Vector3.Distance (startPosition, endPosition);

		while (toMove.position != endPosition)
		{ 
			float distCovered = (Time.time - _startTime) * moveSpeed;
			float fracJourney = distCovered / _journeyLength;
			toMove.position = Vector3.Lerp (startPosition, endPosition, fracJourney);
			yield return null;
		}
		if (script)
			script.nbMoves--;
	}

	public IEnumerator RotateFromTo (Transform toMove, Quaternion initialRotation, Quaternion arrivalRotation, MovableScript script) {
		float elapsedTime = 0f;

		while (elapsedTime < time)
		{
			elapsedTime += Time.deltaTime;
			toMove.rotation = Quaternion.Slerp (initialRotation, arrivalRotation, (elapsedTime/time));
			yield return null;
		}

		toMove.rotation = arrivalRotation;
		if (script)
			script.nbMoves--;
	}

	public void StopMovement()
	{
		StopAllCoroutines ();
	}
//
//	public IEnumerator MoveFromTo2 (Transform toMove, Vector3 startPosition, Vector3 endPosition) {
//		_startTime = Time.time;
//		_journeyLength = Vector3.Distance (startPosition, endPosition);
//
//		while (toMove.position != endPosition)
//		{ 
//			float distCovered = (Time.time - _startTime) * moveSpeed;
//			float fracJourney = distCovered / _journeyLength;
//			toMove.position = Vector3.Lerp (startPosition, endPosition, fracJourney);
//			yield return null;
//		}
//		_CallSetInMove (toMove);
//	}
//
//	public IEnumerator RotateFromTo2 (Transform toMove, Quaternion initialRotation, Quaternion arrivalRotation) {
//		float elapsedTime = 0f;
//
//		while (elapsedTime < time)
//		{
//			elapsedTime += Time.deltaTime;
//			toMove.rotation = Quaternion.Slerp (initialRotation, arrivalRotation, (elapsedTime/time));
//			yield return null;
//		}
//		toMove.rotation = arrivalRotation;
//		_CallSetInMove (toMove);
//	}
//
//	private void _CallSetInMove (Transform toMove) {
//		toMove.GetComponent<InMoveState> ().SetInMove (false);
//	}
}
