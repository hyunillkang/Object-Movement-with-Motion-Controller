using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroControl : MonoBehaviour {

	private bool gyroEnabled;
	private Gyroscope gyro;

	private GameObject cameraContainer;
	private Quaternion rot;

	private string rotString = "";

	// Use this for initialization
	void Start () {

		Screen.orientation = ScreenOrientation.Portrait;
		gyroEnabled = EnableGyro ();	
	}
	
	// Update is called once per frame
	void Update () {
		if (gyroEnabled) {
			float rotX = gyro.attitude.x;
			float rotY = gyro.attitude.y;
			float rotZ = gyro.attitude.z;
			float rotW = gyro.attitude.w;

			Quaternion attr = new Quaternion (rotY, -rotX, rotZ, -rotW);

			float x = attr.eulerAngles.x;
			float y = attr.eulerAngles.y;
			float z = attr.eulerAngles.z;

			transform.localRotation = Quaternion.Euler (-y, x, -z);

			NetworkClientUI.SendGyroScopeInfo (gyro);
		}


	}

	private bool EnableGyro() {
		if (SystemInfo.supportsGyroscope) {
			gyro = Input.gyro;
			gyro.enabled = true;

			//cameraContainer.transform.rotation = Quaternion.Euler (90f, 90f, 0f);
			return true;
		}

		return false;
	}

	void OnGUI() {
		GUI.Box (new Rect (Screen.width - 10 - 100, 10, 100, 30), "Gyro: " + SystemInfo.supportsGyroscope);
		if (gyroEnabled) {
			rotString = gyro.attitude.x.ToString ("F2") + ", " + gyro.attitude.y.ToString ("F2") + ", " + gyro.attitude.z.ToString ("F2");
			GUI.Box (new Rect (Screen.width - 10 - 100, 30, 100, 30), rotString);
		}
	}

}
