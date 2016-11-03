using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Helpers {
	public static InputManager GetInputManager(NetworkInstanceId inputNetId, bool log1 = true, bool log2 = true, bool log3 = true) {
		if (inputNetId == NetworkInstanceId.Invalid || inputNetId.IsEmpty()) {
			if (log1) {
				Debug.LogError ("InputNetId is Invalid");
			}
			return null;
		}
		GameObject inputObj = ClientScene.FindLocalObject (inputNetId);
		if (inputObj == null) {
			if (log2) {
				Debug.LogError ("InputObj is null");
			}
			return null;
		}
		InputManager input = inputObj.GetComponent<InputManager> ();
		if (input == null) {
			if (log3) {
				Debug.LogError ("InputObj has no component InputManager");
			}
			return null;
		}
		return input;
	}

	public static Unit GetUnit(NetworkInstanceId unitNetId, bool log1 = true, bool log2 = true, bool log3 = true) {
		if (unitNetId == NetworkInstanceId.Invalid || unitNetId.IsEmpty()) {
			if (log1) {
				Debug.LogError ("UnitNetId is Invalid");
			}
			return null;
		}
		return GetUnit (ClientScene.FindLocalObject (unitNetId), log2, log3);
	}

	public static Unit GetUnit(GameObject unitObj, bool log1 = true, bool log2 = true) {
		if (unitObj == null) {
			if (log1) {
				Debug.LogError ("UnitObj is null");
			}
			return null;
		}
		Unit unit = unitObj.GetComponent<Unit> ();
		if (unit == null) {
			if (log2) {
				Debug.LogError ("UnitObj had no component Unit");
			}
			return null;
		}
		return unit;
	}

	public static Tile GetTile(NetworkInstanceId tileNetId, bool log1 = true) {
		if (tileNetId == NetworkInstanceId.Invalid || tileNetId.IsEmpty()) {
			if (log1) {
				Debug.LogError ("TempTileNetId is Invalid");
			}
			return null;
		}
		return GetTile(ClientScene.FindLocalObject (tileNetId));
	}

	public static Tile GetTile(GameObject tileObj, bool log1 = true, bool log2 = true) {
		if (tileObj == null) {
			if (log1) {
				Debug.LogError ("TileObj is null");
			}
			return null;
		}
		Tile tile = tileObj.GetComponent<Tile> ();
		if (tile == null) {
			if (log2) {
				Debug.LogError ("TileObj had no component Tile");
			}
			return null;
		}
		return tile;
	}

	public static NetworkInstanceId GetNetId(GameObject obj, bool log1 = true, bool log2 = true, bool log3 = true, bool log4 = true) {
		if (obj == null) {
			if (log1) {
				Debug.LogError ("Obj is null");
			}
			return NetworkInstanceId.Invalid;
		}
		NetworkIdentity netIdComp = obj.GetComponent<NetworkIdentity> ();
		if (netIdComp == null) {
			if (log2) {
				Debug.LogError ("Obj had no component NetworkIdentity");
			}
			return NetworkInstanceId.Invalid;
		}
		NetworkInstanceId netId = netIdComp.netId;
		if (netId == NetworkInstanceId.Invalid) {
			if (log3) {
				Debug.LogError ("NetId is Invalid");
			}
			return NetworkInstanceId.Invalid;
		}
		if (netId.IsEmpty ()) {
			if (log4) {
				Debug.LogError ("NetId is Empty");
			}
		}
		return netId;
	}

	public static T GetNotNull<T>(T obj, bool log1 = true) {
		if (obj == null) {
			if (log1) {
				Debug.LogError ("Obj is null");
			}
			return default(T);
		}
		return obj;
	}

	public static Camera GetCamera(CameraController cameraController, bool log1 = true, bool log2 = true) {
		if (cameraController == null) {
			if (log1) {
				Debug.Log ("CameraController is null");
			}
			return null;
		}
		Camera camera = cameraController.GetComponent<Camera> ();
		if (camera == null) {
			if (log2) {
				Debug.Log ("CameraController has no component Camera");
			}
			return null;
		}
		return camera;
	}
}
