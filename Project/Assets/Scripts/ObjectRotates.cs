using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class ObjectRotates: MonoBehaviour {

	public AudioClip wooshSound;

	string dirString = "";
	string rotString = "";
	string accelString = "";
	private Quaternion rot;
	public static Vector3 accel;

	private bool swingEnable = true;

	private bool gyroController = true;
	private bool mouseController = true;


	void Update () {
		if (NetworkServerUI.msg != null) {
			ObjectRotatesByGyro ();
		} else {
			ObjectRotatesByMouse ();
		}
	}

	void Swing() {
		if (accel.magnitude < 2)
			swingEnable = true;
		else if(swingEnable && accel.magnitude >= 2){
			swingEnable = false;
		}
	}

	void OnCollisionEnter(Collision col) {
		print ("collision");
	}

	void OnGUI() {
		GUI.Box (new Rect (Screen.width - 10 - 300, 10, 300, 30), rotString);
		GUI.Box (new Rect (Screen.width - 10 - 300, 50, 300, 30), accelString);
	}

	void ObjectRotatesByGyro() {
		if (NetworkServer.active) {
			rot = NetworkServerUI.rot;
			accel = NetworkServerUI.accel;
			rotString = rot.x.ToString ("F2") + ", " + rot.y.ToString ("F2") + ", " + rot.z.ToString ("F2") + ", " + rot.w.ToString ("F2");
			accelString = accel.x.ToString ("F2") + ", " + accel.y.ToString ("F2") + ", " + accel.z.ToString ("F2") + ", " + accel.magnitude.ToString ("F2");

			Quaternion attr = new Quaternion (rot.x, rot.y, rot.z, rot.w);


			float x = attr.eulerAngles.x;
			float y = attr.eulerAngles.y;
			float z = attr.eulerAngles.z;

			if (x != 0 && y != 0 && z != 0) {
				transform.localRotation = Quaternion.Euler (-y + 90, 0, -90 + z);
				Swing ();
			}
		}
	}

	Quaternion fromRotation;
	Quaternion toRotation;
	public float moveSpeed = 50;
	public float wheelSpeed = 10;
	public float lerpSpeed = 10;

	private float rotX;
	private float rotZ;

	void ObjectRotatesByMouse() {
		
		rotX += Input.GetAxis ("Mouse ScrollWheel") * wheelSpeed * 5 * Mathf.Deg2Rad;
		rotZ += Input.GetAxis ("Mouse X") *moveSpeed  * Mathf.Deg2Rad;

		fromRotation = transform.rotation;
		toRotation = Quaternion.Euler (rotX, 0, -rotZ);
		transform.rotation = Quaternion.Lerp (fromRotation, toRotation, Time.deltaTime * lerpSpeed);
	}

}