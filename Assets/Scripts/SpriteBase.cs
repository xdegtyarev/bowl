using UnityEngine;
using System.Collections.Generic;

public class SpriteBase : MonoBehaviour {
	[SerializeField]List<Sprite> sprites;
	static SpriteBase Instance;

	void Awake(){
		Instance = this;
	}

	public static Sprite GetSprite(string n){
		foreach(var o in Instance.sprites){
			if(o.name == n){
				return o;
			}
		}
		Debug.Log("No such sprite: " + n);
		return null;
	}
}
