using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraController : NetworkBehaviour {

	public float scrollSpeed = 10.0f;

	private NetworkInstanceId inputNetId;

	void Start() {
		inputNetId = Helpers.GetNetId(Object.FindObjectOfType<InputManager> ().gameObject);
	}

	void Update () {
		transform.position = transform.position + (Vector3)Helpers.GetInputManager(inputNetId).GetMapScrollVector () * Time.deltaTime * scrollSpeed;
	}
}
