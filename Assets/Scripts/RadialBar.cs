using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialBar : MonoBehaviour {
	public Image _bar;
	public RectTransform button;

	public float _rotationValue = 0;

	void Update () {
		RotationChange (_rotationValue);
	}

	void RotationChange(float rotationValue){
		float amount = (rotationValue / 100.0f) * 180.0f / 360;
		_bar.fillAmount = amount;
		float buttonAngle = amount * 360;
		button.localEulerAngles = new Vector3 (0, 0, -buttonAngle);
	}
}
