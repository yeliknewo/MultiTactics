using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class LevelGen : NetworkBehaviour {
	public int minY, maxY, minX, maxX;

	public GameObject tileRockPrefab;
	public GameObject tileGrassPrefab;

	[Command]
	void CmdGenerateLevel() {
		for (int y = minY; y < maxY; y++) {
			for (int x = minX; x < maxX; x++) {
				GameObject tile = Instantiate<GameObject> (GetTilePrefab (x, y));
				tile.transform.position = new Vector2 (x, y);
				NetworkServer.Spawn (tile);
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

	public override void OnStartAuthority ()
	{
		base.OnStartAuthority ();
		CmdGenerateLevel ();
	}
}
