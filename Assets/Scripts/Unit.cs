using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Rusty;

public class Unit : NetworkBehaviour {

	[SyncVar]
	private NetworkInstanceId _tileNetId = NetworkInstanceId.Invalid;
	private Option<NetworkInstanceId> optTileNetId {
		set {
			if (value.IsSome ()) {
				_tileNetId = value.Unwrap ();
			} else {
				_tileNetId = NetworkInstanceId.Invalid;
			}

		}
		get { 
			return Rustify.NetId (_tileNetId);
		}
	}

	public void MoveToTile() {
		MoveToTile (RComponent.Get<Tile> (optTileNetId).ToVital());
	}

	public void MoveToTile(Vital<Tile> vitTile) {
		transform.position = vitTile.Get ().transform.position + Vector3.back;
	}

	void ClearOldTile() {
		if (optTileNetId.IsSome ()) {
			Option<Tile> optTile = RComponent.Get<Tile> (optTileNetId.ToVital());
			if (optTile.IsSome ()) {
				optTile.Unwrap ().SetUnitNetId (Rustify.None<NetworkInstanceId> ());
			}
		}
	}

	void SetNewTile() {
		Option<Tile> optTile = RComponent.Get<Tile> (optTileNetId);
		if (optTile.IsSome ()) {
			optTile.Unwrap ().SetUnitNetId (Rustify.NetId (netId));
		}
	}

	public void SetTileNetId(Vital<NetworkInstanceId> newVitTileNetId) {
		ClearOldTile ();
		this.optTileNetId = newVitTileNetId.ToOpt();
		SetNewTile ();
	}

	public Option<NetworkInstanceId> GetTileNetId() {
		return optTileNetId;
	}
}
