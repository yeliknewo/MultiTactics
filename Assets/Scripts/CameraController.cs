using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Rusty;

public class CameraController : NetworkBehaviour {

	private Vital<float> vitScrollSpeed;
	private Vital<NetworkInstanceId> vitInputNetId;

	void Start() {
		vitScrollSpeed = Rustify.NotNull (10.0f).ToVital ();
		vitInputNetId = RNetId.Get(RComponent.Find<InputManager>()).ToVital();
	}

	void Update () {
		transform.position = transform.position + (Vector3)RComponent.Get<InputManager>(vitInputNetId).Unwrap().GetMapScrollVector () * Time.deltaTime * vitScrollSpeed.Get();
	}
}
