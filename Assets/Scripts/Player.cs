using UnityEngine;
using System.Collections;

public class Player : Token {

	[SerializeField]
	float _RunSpeed=2;
	[SerializeField]
	float _JumpSpeed=4;

	void Update(){

		Vector2 v = Util.GetInputVector ();
		VX = v.x * _RunSpeed;

		if(Input.GetKeyDown(KeyCode.Space)){
			VY = _JumpSpeed;
		}
	}

}
