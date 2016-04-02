using UnityEngine;
using System.Collections;

public class FloorMove : Token {

	public static TokenMgr<FloorMove> parent = null;
	public static FloorMove Add(float x,float y){
		FloorMove floor = parent.Add (x,y);
		floor.Init ();
		return floor;
	}

	float _xprevious=0;
	Player _target=null;

	public float StartSpeed = 0.5f;

	// Use this for initialization
	public void Init () {
		SetVelocityXY (StartSpeed,0);
		_xprevious = X;
	}

	void OnTriggerEnter2D(Collider2D other){
		string name = LayerMask.LayerToName (other.gameObject.layer);
		if(name=="Ground"){
			if(X!=_xprevious){
				VX *= -1;
				X = _xprevious;
			}
		}else if(name=="Player"){
			_target = other.gameObject.GetComponent<Player> ();
		}
	}
	void OnTriggerExit2D(Collider2D other){
		string name = LayerMask.LayerToName (other.gameObject.layer);
		if(name=="Player"){
			_target = null;
		}
	}
	void Update(){
		float dx = X - _xprevious;
		if(_target!=null){
			_target.X += dx;
		}
		_xprevious = X;
	}
	public override void Vanish(){
		_target = null;
		base.Vanish ();
	}
}
