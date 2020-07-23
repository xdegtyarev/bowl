using UnityEngine;
using System.Collections;

public class Spell : MonoBehaviour {
	public IActionReceiver actionReceiver;
	public void Setup(BaseSpell data){
		if(data.Action!=null){
            actionReceiver = gameObject.AddComponent(data.Action.GetType()) as IActionReceiver;
            actionReceiver.Setup(data.Action as IActionReceiver);
            actionReceiver.Action(transform.position, 0f);
        }
	}
}
