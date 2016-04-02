using UnityEngine;
using System.Collections;

public class Particle : Token {
	public static TokenMgr<Particle> parent=null;
	public static Particle Add (float x,float y){
		Particle p = parent.Add (x,y);
		p.Scale = Random.Range (0.5f,1.0f);
		float dir = Random.Range (0,359);
		float spd = Random.Range (3.0f,6.0f);
		p.SetVelocity (dir,spd);
		p.Timer = Random.Range (40,60);
		return p;
	}
	public int Timer;
	void FixedUpdate(){
		MulVelocity (0.97f);
		MulScale (0.97f);
		Timer--;
		if(Timer<1){
			Vanish ();
		}
	}
}
