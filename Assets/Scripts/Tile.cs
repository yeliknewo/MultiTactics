using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Tile : NetworkBehaviour {

	[SyncVar]
	private NetworkInstanceId unitNetId = NetworkInstanceId.Invalid;

	public NetworkInstanceId GetUnitNetId() {
		return this.unitNetId;
	}
		
	public void SetUnitNetId(NetworkInstanceId newUnitNetId) {
		this.unitNetId = newUnitNetId;
	}
}
