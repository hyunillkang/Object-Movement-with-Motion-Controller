using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotatesByMouse_ : MonoBehaviour {


	Quaternion fromRotation;
	Quaternion toRotation;
	public float moveSpeed = 50;
	public float wheelSpeed = 10;
	public float lerpSpeed = 10;

	private float rotX;
	private float rotZ;

	/*
	void Update() {
		RotatesByMouse ();
	}
*/
	void RotatesByMouse() {
		Cursor.lockState = CursorLockMode.Locked;
		rotX += Input.GetAxis ("Mouse ScrollWheel") * wheelSpeed * 5 * Mathf.Deg2Rad;
		rotZ += Input.GetAxis ("Mouse X") *moveSpeed  * Mathf.Deg2Rad;

		fromRotation = transform.rotation;
		toRotation = Quaternion.Euler (rotX, 0, -rotZ);
		transform.rotation = Quaternion.Lerp (fromRotation, toRotation, Time.deltaTime * lerpSpeed);
	}


}
