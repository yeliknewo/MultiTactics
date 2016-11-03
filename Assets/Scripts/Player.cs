using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Player : NetworkBehaviour {
	public GameObject levelGenPrefab;
	public GameObject mainCameraPrefab;
	public GameObject inputManagerPrefab;

	public override void OnStartLocalPlayer ()
	{
		base.OnStartLocalPlayer ();
		if (Object.FindObjectOfType<LevelGen> () == null) {
			CmdSpawnLevelGen ();
			CmdSpawnInputManager ();
			CmdSpawnMainCamera ();
		}
	}

	[Command]
	void CmdSpawnLevelGen() {
		NetworkServer.SpawnWithClientAuthority (Instantiate<GameObject> (levelGenPrefab), gameObject);
	}

	[Command]
	void CmdSpawnMainCamera() {
		NetworkServer.Spawn (Instantiate<GameObject> (mainCameraPrefab));
	}

	[Command]
	void CmdSpawnInputManager() {
		NetworkServer.Spawn (Instantiate<GameObject> (inputManagerPrefab));
	}
}
