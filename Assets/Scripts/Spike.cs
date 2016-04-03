using UnityEngine;
using System.Collections;

public class Spike : Token {

	public static TokenMgr<Spike> parent = null;
	public static Spike Add(float x,float y){
		return parent.Add (x, y);
	}

	void FixedUpdate(){
		Angle += 2;
	}

	void OnTriggerEnter2D(Collider2D other){
		string name = LayerMask.LayerToName (other.gameObject.layer);
		if(name=="Player"){
			Player p = other.gameObject.GetComponent<Player> ();
			p.SetGameState (Player.eGameState.GameOver);
			p.Vanish ();
		}
	}
}
