using UnityEngine;
using System.Collections;

public class Wall : Token {

	public static TokenMgr<Wall> parent=null;
	public static Wall Add(float x,float y){
		Wall w = parent.Add (x,y);
		return w;
	}

}
