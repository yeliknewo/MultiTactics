using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Rusty
{
	public static Option<InputManager> GetInputManager(NetworkInstanceId inputNetId, bool log1 = true, bool log2 = true, bool log3 = true, bool log4 = true) {
		return InternalValidateNotNull(InternalValidateNotNull(ClientScene.FindLocalObject (InternalValidateNetId(inputNetId, log1, log2)), log3).GetComponent<InputManager> (), log4);
	}

	public static Option<Unit> GetUnit(NetworkInstanceId unitNetId, bool log1 = true, bool log2 = true, bool log3 = true, bool log4 = true) {
		Option<NetworkInstanceId> unitNetIdOpt = InternalValidateNetId (unitNetId, log1, log2);
		if (unitNetIdOpt.IsSome ()) {
			return GetUnit (ClientScene.FindLocalObject (unitNetIdOpt.Unwrap()), log3, log4);
		}
		return Option<Unit>.None ();
	}

	public static Option<Unit> GetUnit(GameObject unitObj, bool log1 = true, bool log2 = true) {
		Option<GameObject> unitObjOpt = InternalValidateNotNull(unitObj, log1);
		if (unitObjOpt.IsSome ()) {
			return InternalValidateNotNull(unitObjOpt.Unwrap().GetComponent<Unit> (), log2);
		}
		return Option<Unit>.None ();
	}

	public static Option<Tile> GetTile(NetworkInstanceId tileNetId, bool log1 = true, bool log2 = true, bool log3 = true, bool log4 = true) {
		Option<NetworkInstanceId> tileNetIdOpt = InternalValidateNetId (tileNetId, log1, log2);
		if (tileNetIdOpt.IsSome ()) {
			return GetTile(ClientScene.FindLocalObject (tileNetIdOpt.Unwrap()), log3, log4);
		}
		return Option<Tile>.None ();
	}

	public static Option<Tile> GetTile(GameObject tileObj, bool log1 = true, bool log2 = true) {
		Option<GameObject> tileObjOpt = InternalValidateNotNull (tileObj, log1);
		if (tileObjOpt.IsSome ()) {
			return InternalValidateNotNull (tileObjOpt.Unwrap().GetComponent<Tile> (), log2);
		}
		return Option<Tile>.None ();
	}

	public static Option<NetworkInstanceId> GetNetId(GameObject obj, bool log1 = true, bool log2 = true, bool log3 = true, bool log4 = true) {
		Option<GameObject> objOpt = InternalValidateNotNull (obj, log1);
		if (objOpt.IsSome ()) {
			Option<NetworkIdentity> networkIdentityOpt = InternalValidateNotNull (objOpt.Unwrap ().GetComponent<NetworkIdentity> (), log2);
			if (networkIdentityOpt.IsSome ()) {
				return InternalValidateNetId (networkIdentityOpt.Unwrap ().netId, log3, log4);
			}
		}
		return Option<NetworkInstanceId>.None();
	}

	public static Option<NetworkIdentity> GetNetworkIndentity(GameObject obj, bool log1 = true) {
		Option<GameObject> objOpt = InternalValidateNotNull(obj, log1);
		if (objOpt.IsSome ()) {
			return InternalValidateNotNull (objOpt.Unwrap ().GetComponent<NetworkIdentity> ());
		}
		return Option<NetworkIdentity>.None ();
	}

	public static Option<Camera> GetCamera(CameraController cameraController, bool log1 = true, bool log2 = true) {
		Option<CameraController> cameraControllerOpt = InternalValidateNotNull (cameraController, log1);
		if (cameraControllerOpt.IsSome ()) {
			return InternalValidateNotNull (cameraControllerOpt.Unwrap ().GetComponent<Camera> (), log2);
		}
		return Option<Camera>.None ();
	}

	public static Option<StartClientListener> GetStartClientListener(NetworkInstanceId netId, bool log1 = true, bool log2 = true, bool log3 = true, bool log4 = true) {
		Option<NetworkInstanceId> netIdOpt = InternalValidateNetId (netId, log1, log2);
		if (netIdOpt.IsSome ()) {
			return GetStartClientListener (ClientScene.FindLocalObject (netIdOpt.Unwrap()), log3, log4);
		}
		return Option<StartClientListener>.None ();
	}

	public static Option<StartClientListener> GetStartClientListener(GameObject startClientListenerObj, bool log1 = true, bool log2 = true) {
		Option<GameObject> startClientListenerObjOpt = InternalValidateNotNull(startClientListenerObj, log1);
		if (startClientListenerObjOpt.IsSome ()) {
			return InternalValidateNotNull (startClientListenerObjOpt.Unwrap ().GetComponent<StartClientListener> (), log2);
		}
		return Option<StartClientListener>.None ();
	}

	public static Option<StartClientCallback> GetStartClientCallback(NetworkInstanceId netId, bool log1 = true, bool log2 = true, bool log3 = true, bool log4 = true) {
		Option<NetworkInstanceId> netIdOpt = InternalValidateNetId (netId, log1, log2);
		if (netIdOpt.IsSome ()) {
			return GetStartClientCallback (ClientScene.FindLocalObject (netIdOpt.Unwrap ()), log3, log4);
		}
		return Option<StartClientCallback>.None ();
	}

	public static Option<StartClientCallback> GetStartClientCallback (GameObject startClientCallbackObj, bool log1 = true, bool log2 = true) {
		Option<GameObject> startClientCallbackObjOpt = InternalValidateNotNull (startClientCallbackObj, log1);
		if (startClientCallbackObjOpt.IsSome ()) {
			return InternalValidateNotNull (startClientCallbackObjOpt.Unwrap().GetComponent<StartClientCallback> (), log2);
		}
		return Option<StartClientCallback>.None ();
	}

	public static Option<T> ValidateNotNull<T>(T obj, bool log1 = true) {
		return InternalValidateNotNull (obj, log1);
	}

	public static Option<NetworkInstanceId> ValidateNetId(NetworkInstanceId netId, bool log1 = true, bool log2 = true) {
		return InternalValidateNetId (netId, log1, log2);
	}

	private static Option<T> InternalValidateNotNull<T>(T obj, bool log1) {
		if (obj == null) {
			if (log1) {
				Debug.LogError ("Obj is null");
			}
			return Option<T>.None();
		}
		return Option<T>.Some(obj);
	}

	private static Option<NetworkInstanceId> InternalValidateNetId(NetworkInstanceId netId, bool log1, bool log2) {
		if (netId == NetworkInstanceId.Invalid) {
			if (log1) {
				Debug.LogError ("NetId is Invalid");
			}
			return Option<NetworkInstanceId>.None();
		}
		if (netId.IsEmpty ()) {
			if (log2) {
				Debug.LogError ("NetId is Empty");
			}
			return Option<NetworkInstanceId>.None();
		}
		return Option<NetworkInstanceId>.Some(netId);
	}

}
