using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class InputManager : NetworkBehaviour {
	
	private const string MAP_SCROLL_X_AXIS = "Horizontal";
	private const string MAP_SCROLL_Y_AXIS = "Vertical";
	private const int LEFT_MOUSE_BUTTON = 0;
	private const int RIGHT_MOUSE_BUTTON = 1;
	private const int MIDDLE_MOUSE_BUTTON = 2;

	public Vector2 GetMapScrollVector() {
		return new Vector2 (GetMapScrollX (), GetMapScrollY ());
	}

	public float GetMapScrollX() {
		return Input.GetAxis (MAP_SCROLL_X_AXIS);
	}

	public float GetMapScrollY() {
		return Input.GetAxis (MAP_SCROLL_Y_AXIS);
	}

	public Vector2 GetMouseWorldPos() {
		return Helpers.GetCamera(Object.FindObjectOfType<CameraController>()).ScreenToWorldPoint (Input.mousePosition);
	}

	public bool GetMouseClicked() {
		return Input.GetMouseButton (LEFT_MOUSE_BUTTON);
	}

	public bool GetMouseDown() {
		return Input.GetMouseButtonDown (LEFT_MOUSE_BUTTON);
	}
}
