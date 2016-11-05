using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Rusty;

public class CameraController : NetworkBehaviour {

	private Is<float> isScrollSpeed;
	private Is<NetworkInstanceId> isInputNetId;

	void Start() {
		isInputNetId = Is<NetworkInstanceId>.Wrap(Helpers.GetNetId(Object.FindObjectOfType<InputManager> ().gameObject));
	}

	void Update () {
		transform.position = transform.position + (Vector3)Helpers.GetInputManager(isInputNetId.Get()).GetMapScrollVector () * Time.deltaTime * isScrollSpeed.Get();
	}
}
