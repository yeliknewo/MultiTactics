using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CameraController : NetworkBehaviour {

	public float scrollSpeed = 10.0f;

	private InputManager input;

	void Start() {
		input = Object.FindObjectOfType<InputManager> ();
	}

	void Update () {
		transform.position = transform.position + (Vector3)input.GetMapScrollVector () * Time.deltaTime * scrollSpeed;
	}
}
