using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(StartClientListener))]
public class LevelGen : NetworkBehaviour {
	
	public int minY, maxY, minX, maxX;

	public GameObject tileRockPrefab;
	public GameObject tileGrassPrefab;
	public GameObject unitAxemanPrefab;
	public GameObject unitSpearmanPrefab;

	private int tilesToReady = 0;

	[Command]
	void CmdGenerateLevel() {
		for (int y = minY; y < maxY; y++) {
			for (int x = minX; x < maxX; x++) {
				GameObject tileObj = Instantiate<GameObject> (GetTilePrefab (x, y));
				Helpers.GetTile (tileObj).SetXY (x, y);
				Helpers.GetStartClientCallback(tileObj).SetCallbackNetId (netId);
				tilesToReady += 1;
				NetworkServer.Spawn (tileObj);
			}
		}
	}

	[Command]
	void CmdFinishGenLevel() {
		StartClientListener listener = Helpers.ValidateNotNull (GetComponent<StartClientListener> ());
		NetworkInstanceId tileNetId = listener.GetNextReady ();
		while (Helpers.ValidateNetId (tileNetId, log1: false, log2: false) != NetworkInstanceId.Invalid) {
			this.tilesToReady -= 1;
			Tile tile = Helpers.GetTile (tileNetId);
			int x = tile.GetX ();
			int y = tile.GetY ();

			GameObject unitPrefab = GetUnitPrefab (x, y);
			if (unitPrefab == null) {
				tileNetId = listener.GetNextReady ();
				continue;
			}
			GameObject unitObj = Instantiate<GameObject> (unitPrefab);
			Unit unit = Helpers.GetUnit (unitObj);
			unit.SetTileNetId(tileNetId);
			unit.MoveToTile ();
			NetworkServer.Spawn (unitObj);
			tileNetId = listener.GetNextReady ();
		}
	}

	void Update() {
		if (tilesToReady > 0) {
			CmdFinishGenLevel ();
			Debug.Log (tilesToReady);
		}
	}

	GameObject GetTilePrefab(int x, int y) {
		if (x == y) {
			return tileRockPrefab;
		} else {
			return tileGrassPrefab;
		}
	}

	GameObject GetUnitPrefab(int x, int y) {
		if (x == y) {
			return unitAxemanPrefab;
		} else if (x == -y) {
			return unitSpearmanPrefab;
		} else {
			return null;
		}
	}

	public override void OnStartAuthority ()
	{
		Debug.Log (NetworkServer.active);
		base.OnStartAuthority ();
		CmdGenerateLevel ();
	}
}
