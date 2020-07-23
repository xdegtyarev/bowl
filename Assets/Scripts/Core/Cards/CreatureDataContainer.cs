using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BaseCreature:BaseCard{
	public float hp;
	[HideInInspector]
    public float currentHp;
    [SerializeField]float damage;
    public float Damage{get{return GetAffectedValue(damage,"damage");}}
    public float mass;
    public float drag;
    public float angularDrag;
    public float bounciness;
    public float friction;
    public float scale;
    public float speedCutOff;
    public float cutOffTime;
    public List<PropertyModifier> activePropertyModifiers;//should be cloned specifically
    public MonoBehaviour Action;
    public MonoBehaviour HitAction;


    float GetAffectedValue(float initVal, string key){
        float val = initVal;
        foreach(var o in activePropertyModifiers){
            if(o.propName == "damage"){
                if(o.mul){
                    val*=o.valueChange;
                }else{
                    val+=o.valueChange;
                }
            }
        }
        return val;
    }

    public override void Setup(GameObject go){
        activePropertyModifiers = new List<PropertyModifier>();
        go.GetComponent<Creature>().Setup(this);
    }
}

public class CreatureDataContainer : DataContainer<BaseCard> {
    [SerializeField] BaseCreature card;
    public override BaseCard GetData() {
        card.key = name;
        return card;
    }


}