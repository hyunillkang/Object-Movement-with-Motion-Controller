using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class NetworkServerUI : MonoBehaviour {

	public static StringMessage msg;
	public static Quaternion rot;
	public static Vector3 accel;


	void OnGUI() {
		string ipaddress = Network.player.ipAddress;
		GUI.Box (new Rect (10, Screen.height - 50, 100, 50), ipaddress);
		GUI.Label (new Rect (20, Screen.height - 35, 100, 20), "Status:" + NetworkServer.active);
		GUI.Label (new Rect (20, Screen.height - 20, 100, 20), "Connected:" + NetworkServer.connections.Count);
	}
	// Use this for initialization
	void Start () {
		NetworkServer.Listen (50412);
		NetworkServer.RegisterHandler (888, ServerReceiveMessage);
	}

	private void ServerReceiveMessage(NetworkMessage message) {
		msg = new StringMessage ();
		msg.value = message.ReadMessage<StringMessage> ().value;

		string[] deltas = msg.value.Split ('|');
		float rotX = Convert.ToSingle(deltas[0]);
		float rotY = Convert.ToSingle(deltas[1]);
		float rotZ = Convert.ToSingle(deltas[2]);
		float rotW = Convert.ToSingle(deltas[3]);

		float accelX = Convert.ToSingle (deltas [4]);
		float accelY = Convert.ToSingle (deltas [5]);
		float accelZ = Convert.ToSingle (deltas [6]);

		rot = new Quaternion (rotX, rotY, rotZ, rotW);
		accel = new Vector3 (accelX, accelY, accelZ);
	}


}