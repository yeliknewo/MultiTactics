using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TileDriver : NetworkBehaviour {

	private const string LAYER_TILE = "Tile";
	
	private InputManager input;

	void Start () {
		input = Object.FindObjectOfType<InputManager> ();
	}

	void Update () {
		if (input.GetMouseClicked ()) {
			GameObject tile = GetTile(input.GetMouseWorldPos ());
			if (tile != null) {
				tile.GetComponent<SpriteRenderer> ().color = Color.white;
			}
		}
	}

	GameObject GetTile(Vector2 mousePos) {
		foreach (Collider2D coll in Physics2D.OverlapPointAll (mousePos, LayerMask.NameToLayer (LAYER_TILE))) {
			return coll.gameObject;
		}
		return null;
	}
}
