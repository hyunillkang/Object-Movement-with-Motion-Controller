using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketCollision : MonoBehaviour {

	public AudioClip bounce;

	private float racketPower;
	private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
	}
	
	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "Ball") {
			audioSource.PlayOneShot (bounce);
			Rigidbody rb = collision.gameObject.GetComponent<Rigidbody> ();
			racketPower = 10 + 2 * Mathf.Abs (Input.GetAxis ("Mouse ScrollWheel")) + 20 * ObjectRotates.accel.magnitude;
			print (racketPower);
			rb.velocity = GetReflected (collision) * racketPower;
		}
	}

	private Vector3 GetReflected(Collision collision) {
		Vector3 cannonBallVector = transform.position - collision.gameObject.transform.position;
		Vector3 planeTangent = Vector3.Cross (cannonBallVector, transform.forward);
		Vector3 planeNormal = Vector3.Cross (planeTangent, cannonBallVector);
		Vector3 reflected = Vector3.Reflect (transform.forward, planeNormal);
		return reflected.normalized;
	}

}
