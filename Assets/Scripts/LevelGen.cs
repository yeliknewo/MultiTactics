using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LevelGen : NetworkBehaviour {
	
	public int minY, maxY, minX, maxX;

	public GameObject tileRockPrefab;
	public GameObject tileGrassPrefab;
	public GameObject unitAxemanPrefab;
	public GameObject unitSpearmanPrefab;

	[Command]
	void CmdGenerateLevel() {
		for (int y = minY; y < maxY; y++) {
			for (int x = minX; x < maxX; x++) {
				GameObject tileObj = Instantiate<GameObject> (GetTilePrefab (x, y));
				tileObj.transform.position = new Vector2 (x, y);
				NetworkServer.Spawn (tileObj);

				GameObject unitPrefab = GetUnitPrefab (x, y);
				if (unitPrefab == null) {
					continue;
				}
				GameObject unitObj = Instantiate<GameObject> (unitPrefab);
				unitObj.GetComponent<Unit> ().SetTileObj (tileObj);
				unitObj.GetComponent<Unit> ().MoveToTile ();
				NetworkServer.Spawn (unitObj);
			}
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
		base.OnStartAuthority ();
		CmdGenerateLevel ();
	}
}
