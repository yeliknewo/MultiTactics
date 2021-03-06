﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Rusty;

public class InputManager : NetworkBehaviour {
	
	private const string MAP_SCROLL_X_AXIS = "Horizontal";
	private const string MAP_SCROLL_Y_AXIS = "Vertical";
	private const int LEFT_MOUSE_BUTTON = 0;
	private const int RIGHT_MOUSE_BUTTON = 1;
	private const int MIDDLE_MOUSE_BUTTON = 2;

	public Vital<Vector2> GetMapScrollVector() {
		return new Vector2 (GetMapScrollX (), GetMapScrollY ());
	}

	public Vital<float> GetMapScrollX() {
		return Input.GetAxis (MAP_SCROLL_X_AXIS);
	}

	public Vital<float> GetMapScrollY() {
		return Input.GetAxis (MAP_SCROLL_Y_AXIS);
	}

	public Option<Vector2> GetMouseWorldPos() {
		return RComponent.Get<Camera>(RGameObject.Get(RComponent.Find<CameraController>())).Unwrap().ScreenToWorldPoint (Input.mousePosition);
	}

	public Vital<bool> GetMouseClicked() {
		return Input.GetMouseButton (LEFT_MOUSE_BUTTON);
	}

	public Vital<bool> GetMouseDown() {
		return Input.GetMouseButtonDown (LEFT_MOUSE_BUTTON);
	}
}
