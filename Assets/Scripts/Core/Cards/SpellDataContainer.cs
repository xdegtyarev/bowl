using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PropertyModifier{
	public string propName;
	public float valueChange;
	public bool mul;
	public int turns;
	public PropertyModifier Clone(){
		return MemberwiseClone() as PropertyModifier;
	}
}

[System.Serializable]
public class BaseSpell: BaseCard{
	public MonoBehaviour Action;
	public override void Setup(GameObject go) {
        go.GetComponent<Spell>().Setup(this);
    }
}

public class SpellDataContainer : DataContainer<BaseCard> {
	[SerializeField] BaseSpell card;
	public override BaseCard GetData() {
		card.key = name;
        return card;
    }
}