using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Rusty;

public class TileDriver : NetworkBehaviour {

	private const string LAYER_TILE = "Tile";

	private Option<NetworkInstanceId> optInputNetId = Rustify.None<NetworkInstanceId>();
	private Option<NetworkInstanceId> optSelectedUnitNetId = Rustify.None<NetworkInstanceId>();

	void Start () {
		optInputNetId = RNetId.Get(RComponent.Find<InputManager>().ToVital());
	}

	void Update () {
		HandleInput ();
	}
		
	void HandleInput() {
		Vital<InputManager> vitInput = RComponent.Get<InputManager> (optInputNetId).ToVital();
		if (vitInput.Get().GetMouseDown ()) {
			Vital<Tile> vitTile = this.GetTile (vitInput.Get ().GetMouseWorldPos ());
			Tile tile = GetTile(input.GetMouseWorldPos ());
			NetworkInstanceId tileNetId = Helpers.GetNetId (tile.gameObject);
			if (this.selectedUnitNetId == NetworkInstanceId.Invalid) {
				//Nothing was selected
				NetworkInstanceId unitNetId = tile.GetUnitNetId ();
				if (unitNetId == NetworkInstanceId.Invalid) {
					//Tile had no unit
					return;
				}
				this.selectedUnitNetId = unitNetId;
			} else {
				NetworkInstanceId tempUnitNetId = tile.GetUnitNetId ();
				CmdMoveSelectedUnit (selectedUnitNetId, tileNetId);
				this.selectedUnitNetId = tempUnitNetId;
			}
		}
	}

	[Command]
	void CmdMoveSelectedUnit(NetworkInstanceId selectedUnitId, NetworkInstanceId targetTileId) {
		Unit selectedUnit = Helpers.GetUnit (selectedUnitId);
		selectedUnit.SetTileNetId (targetTileId);
		selectedUnit.MoveToTile ();
	}

	Tile GetTile(Vector2 mousePos) {
		foreach (Collider2D coll in Physics2D.OverlapPointAll (mousePos)) {
			return Helpers.GetTile(coll.gameObject);
		}
		Debug.LogError ("Found No Tile at Mouse");
		return null;
	}
}
