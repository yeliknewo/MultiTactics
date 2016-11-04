using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Unit : NetworkBehaviour {

	[SyncVar]
	private NetworkInstanceId tileNetId = NetworkInstanceId.Invalid;

	public void MoveToTile() {
		MoveToTile (Helpers.GetTile(tileNetId));
	}

	public void MoveToTile(Tile tile) {
		transform.position = Helpers.ValidateNotNull(tile).transform.position + Vector3.back;
	}

	void ClearOldTile() {
		Tile tile = Helpers.GetTile (this.tileNetId, log1: false, log2: false, log3: false, log4: false);
		if(tile != null) {
			tile.SetUnitNetId (NetworkInstanceId.Invalid);
		}
	}

	public void SetTileNetId(NetworkInstanceId newTileNetId) {
		ClearOldTile ();
		this.tileNetId = newTileNetId;
		Helpers.GetTile (this.tileNetId).SetUnitNetId (Helpers.GetNetId (gameObject));
	}

	public NetworkInstanceId GetTileNetId() {
		return tileNetId;
	}
}
