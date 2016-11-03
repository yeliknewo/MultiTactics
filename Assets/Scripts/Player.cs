using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour {
	
	public GameObject levelGenPrefab;
	public GameObject mainCameraPrefab;
	public GameObject inputManagerPrefab;
	public GameObject tileDriverPrefab;

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		if (Object.FindObjectOfType<LevelGen> () == null) {
			CmdSpawnLevelGen ();
			CmdSpawnInputManager ();
			CmdSpawnMainCamera ();
			CmdSpawnTileDriver ();
		}
	}

	[Command]
	void CmdSpawnLevelGen() {
		NetworkServer.SpawnWithClientAuthority (Instantiate<GameObject> (levelGenPrefab), gameObject);
	}
		
	[Command]
	void CmdSpawnInputManager() {
		NetworkServer.Spawn (Instantiate<GameObject> (inputManagerPrefab));
	}

	[Command]
	void CmdSpawnMainCamera() {
		NetworkServer.Spawn (Instantiate<GameObject> (mainCameraPrefab));
	}

	[Command]
	void CmdSpawnTileDriver() {
		NetworkServer.SpawnWithClientAuthority (Instantiate<GameObject> (tileDriverPrefab), gameObject);
	}
}
