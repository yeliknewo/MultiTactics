using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Tile : NetworkBehaviour {
	private GameObject unit;

	public GameObject GetUnit() {
		return unit;
	}

	public void SetUnit(GameObject unit) {
		this.unit = unit;
	}
}
