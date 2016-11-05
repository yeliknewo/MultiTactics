using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Rusty;

[RequireComponent(typeof(StartClientListener))]
public class LevelGen : NetworkBehaviour {
	
	public Vital<int> vitMinY, vitMaxY, vitMinX, vitMaxX;

	public GameObject tileRockPrefab;
	public GameObject tileGrassPrefab;
	public GameObject unitAxemanPrefab;
	public GameObject unitSpearmanPrefab;

	private Vital<int> vitTilesToReady = Rustify.ToVital(0);

	[Command]
	void CmdGenerateLevel() {
		for (Vital<int> vitY = vitMinY; vitY.Get() < vitMaxY.Get(); vitY = Rustify.ToVital(vitY.Get() + 1)) {
			for (Vital<int> vitX = vitMinX; vitX.Get() < vitMaxX.Get(); vitX = Rustify.ToVital(vitX.Get() + 1)) {
				Option<GameObject> optTileObjPrefab = GetTilePrefab (vitX, vitY);
				if (optTileObjPrefab.IsSome ()) {
					Vital<GameObject> vitTileObj = RGameObject.Instantiate(optTileObjPrefab.ToVital());
					RComponent.Get<Tile>(vitTileObj).Unwrap().SetXY (vitX, vitY);
					RComponent.Get<StartClientCallback>(vitTileObj).Unwrap().SetCallbackNetId (RNetId.Get(Rustify.NotNull(this)).ToVital());
					vitTilesToReady = Rustify.ToVital(vitTilesToReady.Get() + 1);
					NetworkServer.Spawn (vitTileObj.Get());
				}
			}
		}
	}

	[Command]
	void CmdFinishGenLevel() {
		Vital<StartClientListener> vitListener = RComponent.Get<StartClientListener>(Rustify.NotNull(gameObject)).ToVital();
		Option<NetworkInstanceId> optTileNetId = vitListener.Get().GetNextReady ();
		while (optTileNetId.IsSome()) {
			Vital<NetworkInstanceId> vitTileNetId = optTileNetId.ToVital (); //safe

			this.vitTilesToReady = Rustify.ToVital(vitTilesToReady.Get() - 1);
			Vital<Tile> vitTile = RComponent.Get<Tile>(vitTileNetId).ToVital ();
			Vital<int> vitX = vitTile.Get().GetX ();
			Vital<int> vitY = vitTile.Get().GetY ();

			Option<GameObject> optUnitPrefab = GetUnitPrefab (vitX, vitY);
			if (optUnitPrefab.IsSome()) {
				Vital<GameObject> vitUnitObj = RGameObject.Instantiate (optUnitPrefab.ToVital ()); //safe
				Vital<Unit> vitUnit = RComponent.Get<Unit> (vitUnitObj).ToVital();
				vitUnit.Get ().SetTileNetId (vitTileNetId);
				vitUnit.Get ().MoveToTile ();
				optTileNetId = vitListener.Get ().GetNextReady();
			}
			optTileNetId = vitListener.Get ().GetNextReady ();
			continue;
		}
	}

	void Update() {
		if (vitTilesToReady.Get() > 0) {
			CmdFinishGenLevel ();
		}
	}

	Option<GameObject> GetTilePrefab(Vital<int> vitX, Vital<int> vitY) {
		if (vitX == vitY) {
			return Rustify.NotNull(tileRockPrefab);
		} else {
			return Rustify.NotNull(tileGrassPrefab);
		}
	}

	Option<GameObject> GetUnitPrefab(Vital<int> vitX, Vital<int> vitY) {
		if (vitX == vitY) {
			return Rustify.NotNull(unitAxemanPrefab);
		} else if (vitX.Get() == -vitY.Get()) {
			return Rustify.NotNull(unitSpearmanPrefab);
		} else {
			return Rustify.None<GameObject> ();
		}
	}

	public override void OnStartAuthority ()
	{
		base.OnStartAuthority ();
		CmdGenerateLevel ();
	}
}
