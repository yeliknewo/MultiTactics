using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using Rusty;

[RequireComponent(typeof(StartClientCallback))]
public class Tile : NetworkBehaviour {

	[SyncVar]
	private int _x = 0;
	private Vital<int> vitX {
		get {
			return Rustify.ToVital (_x);
		}
		set {
			_x = value.Get();
		}
	}

	[SyncVar]
	private int _y = 0;
	private Vital<int> vitY {
		get {
			return Rustify.ToVital (_y);
		}
		set {
			_y = value.Get();
		}
	}

	[SyncVar]
	private NetworkInstanceId _unitNetId = NetworkInstanceId.Invalid;
	private Option<NetworkInstanceId> optUnitNetId {
		get {
			return Rustify.NetId (_unitNetId);
		}
		set {
			if (value.IsSome ()) {
				_unitNetId = value.Unwrap ();
			} else {
				_unitNetId = NetworkInstanceId.Invalid;
			}
		}
	}

	public void SetXY(Vital<int> newVitX, Vital<int> newVitY) {
		this.vitX = newVitX;
		this.vitY = newVitY;
		transform.position = new Vector2 (this.vitX.Get(), this.vitY.Get());
	}

	public Vital<int> GetX() {
		return this.vitX;
	}

	public Vital<int> GetY() {
		return this.vitY;
	}

	public Option<NetworkInstanceId> GetUnitNetId() {
		return this.optUnitNetId;
	}
		
	public void SetUnitNetId(Option<NetworkInstanceId> newOptUnitNetId) {
		this.optUnitNetId = newOptUnitNetId;
	}
}
