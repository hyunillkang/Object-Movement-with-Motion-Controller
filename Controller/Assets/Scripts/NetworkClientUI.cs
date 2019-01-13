using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

public class NetworkClientUI : MonoBehaviour {

	static NetworkClient client;
	private string ipAddress;
	public InputField inputField;

	void OnGUI(){
		string ipaddress = Network.player.ipAddress;
		GUI.Box (new Rect (10, 10, 110, 50), ipaddress + "  ");
		GUI.Label (new Rect (10, 30, 110, 20), "Connection:" +client.isConnected);

		if (!client.isConnected) {
			if (GUI.Button (new Rect (inputField.transform.position.x + inputField.caretWidth/2 - 30, Screen.height - 50 - 10, 60, 50), "Connect")) {
				Connect ();
			}
		}

	}

	// Use this for initialization
	void Start () {
		client = new NetworkClient ();
	}

	void Connect() {
		ipAddress = inputField.text;
		client.Connect (ipAddress, 50412);
	}

	static public void SendGyroScopeInfo(Gyroscope gyro) {
		if (client.isConnected) {
			StringMessage msg = new StringMessage ();

			msg.value = gyro.attitude.x + "|" + gyro.attitude.y + "|" + gyro.attitude.z + "|" + gyro.attitude.w + "|" + 
				gyro.userAcceleration.x + "|" + gyro.userAcceleration.y + "|" + gyro.userAcceleration.z;
			client.Send (888, msg);
		}
	}

	// Update is called once per frame
	void Update () {
		
	}
}
