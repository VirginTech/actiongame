using UnityEngine;
using System.Collections;

public class FieldMgr {

	const int CHIP_PLAYER = 1;
	const int CHIP_WALL=2;
	const int CHIP_SPIKE = 3;
	const int CHIP_FLOOR_MOVE = 4;
	const int CHIP_GOAL = 5;

	float GetChipX(int i){
		Vector2 min = Camera.main.ViewportToWorldPoint (Vector2.zero);
		var spr = Util.GetSprite ("Levels/tileset","tileset_0");
		var sprW = spr.bounds.size.x;
		return min.x + (sprW * i) + sprW / 2;
	}
	float GetChipY(int j){
		Vector2 max = Camera.main.ViewportToWorldPoint (Vector2.one);
		var spr = Util.GetSprite ("Levels/tileset","tileset_0");
		var sprH = spr.bounds.size.y;
		return max.y - (sprH * j) - sprH / 2;
	}
	public void Load(int stage){
		TMXLoader tmx = new TMXLoader ();
		string path = string.Format ("Levels/{0:D3}",stage);
		tmx.Load (path);
		Layer2D layer = tmx.GetLayer (0);
		//Debug.Log ("幅：" + layer.Width);
		//Debug.Log ("高：" + layer.Height);
		for(int j=0;j<layer.Height;j++){
			for(int i=0;i<layer.Width;i++){
				int v = layer.Get (i,j);
				float x = GetChipX (i);
				float y = GetChipY (j);
				switch(v){
				case CHIP_PLAYER:
					{
						GameObject obj = GameObject.Find ("Player") as GameObject;
						Player player = obj.GetComponent<Player> ();
						player.SetPosition (x, y);
					}
					break;
				case CHIP_WALL:
					Wall.Add (x, y);
					break;
				case CHIP_SPIKE:
					Spike.Add (x,y);
					break;
				case CHIP_FLOOR_MOVE:
					FloorMove.Add (x, y);
					break;
				case CHIP_GOAL:
					{
						GameObject obj=GameObject.Find("Goal") as GameObject;
						Goal goal=obj.GetComponent<Goal>();
						goal.SetPosition(x,y);
					}
					break;
				}
			}
		}
	}
}
