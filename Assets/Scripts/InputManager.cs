using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class InputManager : NetworkBehaviour {
	private const string MAP_SCROLL_X_AXIS = "Horizontal";
	private const string MAP_SCROLL_Y_AXIS = "Vertical";

	public Vector2 GetMapScrollVector() {
		return new Vector2 (GetMapScrollX (), GetMapScrollY ());
	}

	public float GetMapScrollX() {
		return Input.GetAxis (MAP_SCROLL_X_AXIS);
	}

	public float GetMapScrollY() {
		return Input.GetAxis (MAP_SCROLL_Y_AXIS);
	}
}
