using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Unit : NetworkBehaviour {

	[SyncVar]
	private NetworkInstanceId tileNetId = NetworkInstanceId.Invalid;

	private GameObject tileObj;

	void Update() {
		if (tileObj != null) {
			tileNetId = Helpers.GetNetId (tileObj);
			if (tileNetId == NetworkInstanceId.Invalid) {
				return;
			}
			tileObj = null;
		}
	}

	public void MoveToTile() {
		if (tileObj != null) {
			tileNetId = Helpers.GetNetId (tileObj);
			if (tileNetId == NetworkInstanceId.Invalid) {
				return;
			}
			tileObj = null;
		}
		MoveToTile (Helpers.GetTile(tileNetId));
	}

	public void MoveToTile(Tile tile) {
		transform.position = Helpers.GetNotNull(tile).transform.position + Vector3.back;
	}

	void ClearOldTile() {
		Tile tile = Helpers.GetTile (this.tileNetId, log1: false);
		if(tile != null) {
			tile.SetUnitNetId (NetworkInstanceId.Invalid);
		}
	}

	public void SetTileObj(GameObject newTileObj) {
		this.tileObj = newTileObj;
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
