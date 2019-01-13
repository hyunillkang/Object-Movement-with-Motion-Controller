using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour {

	public float firePower;
	public GameObject cannonBall;
	public Transform spawnPoint;
	public float rotSpeed = 20;
	public float spawnTime = 1;
	public int maxCount = 10;
	public static bool start = false;

	private Rigidbody cannonBallRB;
	private float lastSecond = 0;
	private float deltaAngle = 180;
	private Quaternion fromRotation;
	private Quaternion toRotation;

	private int count = 0;
	// Use this for initialization
	void Start () {
		float randNum = Random.Range (0, 2);
		if (randNum < 1)
			rotSpeed = -rotSpeed;
	}
	
	// Update is called once per frame
	void Update () {
		if (start == false)
			return;
		
		Cursor.lockState = CursorLockMode.Locked;
		lastSecond += Time.deltaTime;
		if (lastSecond - Time.deltaTime > spawnTime) {
			FireCannonBall ();
			lastSecond = 0;
			count++;
			if (count == maxCount) {
				start = false;
				Cursor.lockState = CursorLockMode.None;
			}
		}

		deltaAngle += Time.deltaTime * rotSpeed;
		deltaAngle = deltaAngle % 360;
		if (deltaAngle > 190 || deltaAngle < 170)
			rotSpeed = -rotSpeed;
		transform.rotation = Quaternion.Euler (0, deltaAngle, 0);
	}

	void FireCannonBall() {
		GameObject cannonBallCopy = Instantiate (cannonBall, spawnPoint.position, transform.rotation) as GameObject;
		cannonBallRB = cannonBallCopy.GetComponent<Rigidbody> ();
		cannonBallRB.AddForce (transform.forward * firePower);
	}

	void OnGUI() {
		if (!start) {
			if (GUI.Button (new Rect (Screen.width/2-150, Screen.height/2, 300, 30), "Start")) {
				count = 0;
				CannonBall.score = 0;
				start = true;
			}
		}
		GUI.Box (new Rect (10, 10, 300, 30), "Score: " + CannonBall.score);
	}
}
