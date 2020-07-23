using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseCard{
	[HideInInspector]
	public string key;
	public string view;
	public int summonCost;

	public int teamId;

    public virtual void Setup(GameObject go){

    }

    public virtual BaseCard Clone(){
    	return MemberwiseClone() as BaseCard;
    }
}
