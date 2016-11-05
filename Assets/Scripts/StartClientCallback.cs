using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Rusty;

public class StartClientCallback : NetworkBehaviour {
	[SyncVar]
	private NetworkInstanceId _listenerNetId = NetworkInstanceId.Invalid;
	private Option<NetworkInstanceId> optListenerNetId {
		get {
			return Rustify.NetId (_listenerNetId);
		}
		set {
			if (value.IsSome ()) {
				_listenerNetId = value.Unwrap ();
			} else {
				_listenerNetId = NetworkInstanceId.Invalid;
			}
		}
	}

	private bool callbackSet = false;

	public void SetCallbackNetId(Vital<NetworkInstanceId> newVitNetId) {
		this.optListenerNetId = newVitNetId.ToOpt();
		callbackSet = true;
	}

	public override void OnStartClient ()
	{
		if (optListenerNetId.IsSome()) {
			RComponent.Get<StartClientListener> (optListenerNetId).Unwrap ().Ready (Rustify.NetId (netId).ToVital());
		}
	}
}
