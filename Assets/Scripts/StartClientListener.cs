using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class StartClientListener : NetworkBehaviour {
	private Queue<NetworkInstanceId> readys = new Queue<NetworkInstanceId>();
	private HashSet<NetworkInstanceId> done = new HashSet<NetworkInstanceId>();

	public void Ready(NetworkInstanceId netId) {
		if (!this.readys.Contains(netId) && !this.done.Contains(netId)) {
			this.readys.Enqueue (netId);
		}
	}

	public NetworkInstanceId GetNextReady() {
		if (this.readys.Count > 0) {
			NetworkInstanceId temp = this.readys.Dequeue ();
			if (!done.Add (temp)) {
				return NetworkInstanceId.Invalid;
			}
			return temp;
		}
		return NetworkInstanceId.Invalid;
	}
}
