using UnityEngine;
using System.Collections;

public class GameMgr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Wall.parent = new TokenMgr<Wall> ("Wall", 128);

		FloorMove.parent = new TokenMgr<FloorMove> ("FloorMove",32);

		FieldMgr field = new FieldMgr ();
		field.Load (1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
