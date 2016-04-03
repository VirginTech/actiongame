using UnityEngine;
using System.Collections;

public class Player : Token {

	public enum eGameState{
		None,
		StageClear,
		GameOver,
	}
	eGameState _gameState=eGameState.None;

	public void SetGameState(eGameState s){
		_gameState = s;
	}
	public eGameState GetGameState(){
		return _gameState;
	}

	bool _bFacingLeft=false;
	enum eState{
		Idle,
		Run,
		Jump,
	}
	eState _state=eState.Idle;
	int _tAnim=0;

	public Sprite Sprite0;
	public Sprite Sprite1;
	public Sprite Sprite2;
	public Sprite Sprite3;

	[SerializeField]
	float _RunSpeed=2;
	[SerializeField]
	float _JumpSpeed=4;

	bool _bGround=false;

	bool CheckGround(){
		int mask = 1 << LayerMask.NameToLayer ("Ground");
		float distance = SpriteHeight * 0.6f;
		float width = BoxColliderWidth * 0.6f;
		float[] xList = { X-width,X,X+width };
		foreach(float px in xList){
			RaycastHit2D hit = Physics2D.Raycast (new Vector2(px,Y),-Vector2.up,distance,mask);
			if(hit.collider!=null){
				return true;
			}
		}
		return false;
	}

	void Update(){

		Vector2 v = Util.GetInputVector ();
		VX = v.x * _RunSpeed;
		_bGround = CheckGround ();

		if(VX<=-1.0f){
			_bFacingLeft = true;
		}
		if(VX>=1.0f){
			_bFacingLeft = false;
		}

		if(Input.GetKeyDown(KeyCode.Space)){
			if(_bGround){
				VY = _JumpSpeed;
			}
		}
	}

	void FixedUpdate(){
		_tAnim++;
		if (_bFacingLeft) {
			ScaleX = -1.0f;
		} else {
			ScaleX = 1.0f;
		}

		if (_bGround == false) {
			_state = eState.Jump;
		} else if (Mathf.Abs (VX) >= 1.0f) {
			_state = eState.Run;
		} else {
			_state = eState.Idle;
		}
		switch (_state) {
		case eState.Idle:
			if (_tAnim % 200 < 10) {
				SetSprite (Sprite1);
			} else {
				SetSprite (Sprite0);
			}
			break;
		case eState.Run:
			if (_tAnim % 12 < 6) {
				SetSprite (Sprite2);
			} else {
				SetSprite (Sprite3);
			}
			break;
		case eState.Jump:
			SetSprite (Sprite2);
			break;
		}
	}

	public override void Vanish(){
		for(int i=0;i<32;i++){
			Particle.Add (X,Y);
		}
		base.Vanish ();
	}
}
