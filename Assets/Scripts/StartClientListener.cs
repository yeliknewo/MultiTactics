using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using Rusty;

public class StartClientListener : NetworkBehaviour {
	private Vital<Queue<NetworkInstanceId>> vitReadys = Rustify.ToVital(new Queue<NetworkInstanceId>());
	private Vital<HashSet<NetworkInstanceId>> vitDone = Rustify.ToVital(new HashSet<NetworkInstanceId>());

	public void Ready(Vital<NetworkInstanceId> vitNetId) {
		if (!this.vitReadys.Get().Contains(vitNetId.Get()) && !this.vitDone.Get().Contains(vitNetId.Get())) {
			this.vitReadys.Get().Enqueue (vitNetId.Get());
		}
	}

	public Option<NetworkInstanceId> GetNextReady() {
		if (this.vitReadys.Get().Count > 0) {
			Option<NetworkInstanceId> optTemp = Rustify.NetId(this.vitReadys.Get().Dequeue ());
			if (optTemp.IsSome() && !this.vitDone.Get().Add (optTemp.Unwrap())) {
				return Rustify.None<NetworkInstanceId>();
			}
			return optTemp;
		}
		return Rustify.None<NetworkInstanceId> ();
	}
}
