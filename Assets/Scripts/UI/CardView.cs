using UnityEngine;
using UnityEngine.UI;

public class CardView : MonoBehaviour, IUIContainerItem {
	[SerializeField] Image view;
	[SerializeField] Text summonCostLabel;
    [SerializeField] Text cardNameLabel;
	IUIContainer container;
	BaseCard data;
    public void Setup(object itemData, IUIContainer c) {
    	data = itemData as BaseCard;
    	container = c;
        cardNameLabel.text = data.key;
    	view.sprite = SpriteBase.GetSprite(data.view+data.teamId.ToString());
    	summonCostLabel.text = data.summonCost.ToString();
    }
    public void UpdateState() {

    }
    public GameObject GetGameObject() {
        return gameObject;
    }

    public void OnSelect(){
    	container.MarkItemClicked(data);
    }
}
