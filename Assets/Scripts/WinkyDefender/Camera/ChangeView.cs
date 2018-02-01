using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeView : MonoBehaviour {

	private Vector3 initpos = new Vector3 (8.5f, 13f, 6.5f);
	private Vector3 cam2pos = new Vector3(8.5f, 11f, -3.5f);
	private bool _activate = false;


	void Start () {
	}

	public void InstantReturnToInitPos()
	{
		this.transform.localPosition = initpos;
		this.transform.rotation = Quaternion.Euler (90, 0, 0);
		_activate = false;
	}

	IEnumerator ReturntoInitPos()
	{
		float time = 2f;
		float elapsedTime = 0f;
		while (elapsedTime < time) {
			this.transform.rotation = Quaternion.Slerp (Quaternion.Euler(55, 0, 0), Quaternion.Euler(90, 0, 0), (elapsedTime/time));
			this.transform.localPosition = Vector3.Lerp (cam2pos, initpos, (elapsedTime/time));	
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}

	IEnumerator SwitchToCam2()
	{
		float time = 2f;
		float elapsedTime = 0f;
		while (elapsedTime < time) {
			this.transform.rotation = Quaternion.Slerp (Quaternion.Euler(90, 0, 0), Quaternion.Euler(55, 0, 0), (elapsedTime/time));
			this.transform.localPosition = Vector3.Lerp (initpos, cam2pos, (elapsedTime/time));	
			elapsedTime += Time.deltaTime;

			yield return null;
		}
			
	}

	public IEnumerator ChangeCameraView () {
		if (_activate == false) {
			yield return (StartCoroutine(SwitchToCam2()));
			_activate = true;
		}
		else {
			yield return (StartCoroutine (ReturntoInitPos()));
			_activate = false;
		}
	}

	public void StopChangingView()
	{
		StopAllCoroutines ();
	}
}

