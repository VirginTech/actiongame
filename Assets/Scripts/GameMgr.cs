using UnityEngine;
using System.Collections;

public class GameMgr : MonoBehaviour {

	int nStage=1;

	void Load(){
		FieldMgr field = new FieldMgr ();
		field.Load (nStage);
	}

	void Restore(){
		Wall.parent.Vanish ();
		FloorMove.parent.Vanish ();
		Particle.parent.Vanish ();
		Spike.parent.Vanish ();
		_player.Revive ();
		_player.SetGameState (Player.eGameState.None);
	}

	Player _player=null;
	void CheckPlayerGameState(){
		if(_player==null){
			GameObject obj = GameObject.Find ("Player") as GameObject;
			_player = obj.GetComponent<Player> ();
		}
		switch(_player.GetGameState()){
		case Player.eGameState.StageClear:
			_state = eState.StageClear;
			break;
		case Player.eGameState.GameOver:
			_state = eState.GameOver;
			break;
		}
	}

	enum eState{
		Main,
		StageClear,
		GameOver,
	}
	eState _state=eState.Main;

	void DrawLabelCenter(string message){
		Util.SetFontSize (32);
		Util.SetFontAlignment (TextAnchor.MiddleCenter);
		float w = 128;
		float h = 32;
		float px = Screen.width / 2 - w / 2;
		float py = Screen.height / 2 - h / 2;
		Util.GUILabel (px,py,w,h,message);
	}

	void OnGUI(){
		switch(_state){
		case eState.StageClear:
			DrawLabelCenter ("Stage Clear!!");
			break;
		case eState.GameOver:
			DrawLabelCenter ("Game Over....");
			break;
		}
	}

	// Use this for initialization
	void Start () {
		Wall.parent = new TokenMgr<Wall> ("Wall", 128);
		FloorMove.parent = new TokenMgr<FloorMove> ("FloorMove",32);
		Particle.parent = new TokenMgr<Particle> ("Particle",32);
		Spike.parent = new TokenMgr<Spike> ("Spike",32);

		Load ();
		//FieldMgr field = new FieldMgr ();
		//field.Load (1);
	}
	
	// Update is called once per frame
	void Update () {
		switch(_state){
		case eState.Main:
			CheckPlayerGameState ();
			break;
		case eState.StageClear:
			if(Input.GetKeyDown(KeyCode.Space)){
				Restore ();
				nStage++;
				if(nStage>3){
					nStage = 1;
				}
				Load ();
				_state = eState.Main;
			}
			break;
		case eState.GameOver:
			if(Input.GetKeyDown(KeyCode.Space)){
				Restore ();
				Load ();
				_state = eState.Main;
			}
			break;
		}
	}
}
