using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleTextItem : MonoBehaviour,IUIContainerItem {
	[SerializeField]Text label;
	IUIContainer container;
	BaseCard data;
    public void Setup(object itemData, IUIContainer c) {
    	data = itemData as BaseCard;
    	container = c;
    	UpdateState();
    }
    public void UpdateState() {
    	label.text = data.key;
    }
    public GameObject GetGameObject() {
        return gameObject;
    }

	public void OnClick(){
		container.MarkItemClicked(data);
	}
}
