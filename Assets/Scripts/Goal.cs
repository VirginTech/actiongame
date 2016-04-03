using UnityEngine;
using System.Collections;

public class Goal : Token {

	public Sprite Spr0;
	public Sprite Spr1;
	public Sprite Spr2;
	public Sprite Spr3;

	int _tAnim=0;

	void FixedUpdate(){
		_tAnim++;
		Sprite[] sprTbl = { Spr0,Spr1,Spr2,Spr3 };
		const int INTERVAL = 16;
		int SIZE = sprTbl.Length;
		int idx = (_tAnim % (INTERVAL * SIZE)) / INTERVAL;
		SetSprite (sprTbl[idx]);
	}

	public void OnTriggerEnter2D(Collider2D other){
		string name = LayerMask.LayerToName (other.gameObject.layer);
		if(name=="Player"){
			Player p = other.gameObject.GetComponent<Player> ();
			p.SetGameState (Player.eGameState.StageClear);
			p.Vanish ();
		}
	}

}
