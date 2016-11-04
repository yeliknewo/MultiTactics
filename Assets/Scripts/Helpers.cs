using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Helpers {
	public static InputManager GetInputManager(NetworkInstanceId inputNetId, bool log1 = true, bool log2 = true, bool log3 = true, bool log4 = true) {
		return InternalValidateNotNull(InternalValidateNotNull(ClientScene.FindLocalObject (InternalValidateNetId(inputNetId, log1, log2)), log3).GetComponent<InputManager> (), log4);
	}

	public static Unit GetUnit(NetworkInstanceId unitNetId, bool log1 = true, bool log2 = true, bool log3 = true, bool log4 = true) {
		return GetUnit (ClientScene.FindLocalObject (InternalValidateNetId(unitNetId, log1, log2)), log3, log4);
	}

	public static Unit GetUnit(GameObject unitObj, bool log1 = true, bool log2 = true) {
		return InternalValidateNotNull(InternalValidateNotNull(unitObj, log1).GetComponent<Unit> (), log2);
	}

	public static Tile GetTile(NetworkInstanceId tileNetId, bool log1 = true, bool log2 = true, bool log3 = true, bool log4 = true) {
		return GetTile(ClientScene.FindLocalObject (InternalValidateNetId(tileNetId, log1, log2)), log3, log4);
	}

	public static Tile GetTile(GameObject tileObj, bool log1 = true, bool log2 = true) {
		return InternalValidateNotNull (InternalValidateNotNull (tileObj, log1).GetComponent<Tile> (), log2);
	}

	public static NetworkInstanceId GetNetId(GameObject obj, bool log1 = true, bool log2 = true, bool log3 = true, bool log4 = true) {
		return InternalValidateNetId (InternalValidateNotNull(InternalValidateNotNull(obj, log1).GetComponent<NetworkIdentity> (), log2).netId, log3, log4);
	}
		
	public static Camera GetCamera(CameraController cameraController, bool log1 = true, bool log2 = true) {
		return InternalValidateNotNull(InternalValidateNotNull(cameraController, log1).GetComponent<Camera> (), log2);
	}

	public static StartClientListener GetStartClientListener(NetworkInstanceId netId, bool log1 = true, bool log2 = true, bool log3 = true, bool log4 = true) {
		return GetStartClientListener (ClientScene.FindLocalObject (InternalValidateNetId(netId, log1, log2)), log3, log4);
	}
		
	public static StartClientListener GetStartClientListener(GameObject startClientListenerObj, bool log1 = true, bool log2 = true) {
		return InternalValidateNotNull(InternalValidateNotNull(startClientListenerObj, log1).GetComponent<StartClientListener> (), log2);
	}

	public static StartClientCallback GetStartClientCallback(NetworkInstanceId netId, bool log1 = true, bool log2 = true, bool log3 = true, bool log4 = true) {
		return GetStartClientCallback (ClientScene.FindLocalObject (InternalValidateNetId (netId, log1, log2)), log3, log4);
	}

	public static StartClientCallback GetStartClientCallback (GameObject startClientCallbackObj, bool log1 = true, bool log2 = true) {
		return InternalValidateNotNull (InternalValidateNotNull (startClientCallbackObj, log1).GetComponent<StartClientCallback> (), log2);
	}
		
	public static T ValidateNotNull<T>(T obj, bool log1 = true) {
		if (obj == null) {
			if (log1) {
				Debug.LogError ("Obj is null");
			}
			return default(T);
		}
		return obj;
	}

	public static NetworkInstanceId ValidateNetId(NetworkInstanceId netId, bool log1 = true, bool log2 = true) {
		if (netId == NetworkInstanceId.Invalid) {
			if (log1) {
				Debug.LogError ("NetId is Invalid");
			}
			return NetworkInstanceId.Invalid;
		}
		if (netId.IsEmpty ()) {
			if (log2) {
				Debug.LogError ("NetId is Empty");
			}
			return NetworkInstanceId.Invalid;
		}
		return netId;
	}

	private static T InternalValidateNotNull<T>(T obj, bool log1) {
		if (obj == null) {
			if (log1) {
				Debug.LogError ("Obj is null");
			}
			return default(T);
		}
		return obj;
	}

	private static NetworkInstanceId InternalValidateNetId(NetworkInstanceId netId, bool log1, bool log2) {
		if (netId == NetworkInstanceId.Invalid) {
			if (log1) {
				Debug.LogError ("NetId is Invalid");
			}
			return NetworkInstanceId.Invalid;
		}
		if (netId.IsEmpty ()) {
			if (log2) {
				Debug.LogError ("NetId is Empty");
			}
			return NetworkInstanceId.Invalid;
		}
		return netId;
	}
}
