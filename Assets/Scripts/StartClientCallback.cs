using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class StartClientCallback : NetworkBehaviour {

	private NetworkInstanceId listenerNetId = NetworkInstanceId.Invalid;

	private bool callbackSet = false;

	public void SetCallbackNetId(NetworkInstanceId newNetId) {
		this.listenerNetId = newNetId;
		callbackSet = true;
	}

	public override void OnStartClient ()
	{
		if (callbackSet) {
			Helpers.GetStartClientListener (listenerNetId).Ready(Helpers.GetNetId(gameObject));
		}
	}
}
