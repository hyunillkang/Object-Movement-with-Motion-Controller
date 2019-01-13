using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour {

	private Transform startPoint;

	private Rigidbody rb;
	public static float score;
	private bool scoreCalculated = false;
	// Update is called once per frame
	void Start() {
		rb = GetComponent<Rigidbody> (); 
	}

	void Update () {
		StatusCheck ();
	}

	void StatusCheck() {
		if (scoreCalculated == true)
			return;


		if (rb.velocity.magnitude < 0.5) {
			score +=  (int)transform.position.z;
			scoreCalculated = true;
		}
	}

}
