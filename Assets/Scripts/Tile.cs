using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(StartClientCallback))]

public class Tile : NetworkBehaviour {

	[SyncVar]
	private int x = 0;

	[SyncVar]
	private int y = 0;

	[SyncVar]
	private NetworkInstanceId unitNetId = NetworkInstanceId.Invalid;

	public void SetXY(int newX, int newY) {
		this.x = newX;
		this.y = newY;
		transform.position = new Vector2 (this.x, this.y);
	}

	public int GetX() {
		return this.x;
	}

	public int GetY() {
		return this.y;
	}

	public NetworkInstanceId GetUnitNetId() {
		return this.unitNetId;
	}
		
	public void SetUnitNetId(NetworkInstanceId newUnitNetId) {
		this.unitNetId = newUnitNetId;
	}
}
