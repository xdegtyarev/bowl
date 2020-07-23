using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerUIController : MonoBehaviour {
	[SerializeField] Text manaLabel;
	[SerializeField] BaseContainer container;
	[SerializeField] GameObject cardPrefab;
	[SerializeField] int id;
	Player player;

	BaseCard selectedUnit;

	public void Start(){
		player = new Player();
		player.id = id;
		player.FillHand();
		MatchManager.Instance.AddPlayer(player);
		UpdateView();
		container.OnItemClickAction+=OnCardSelected;
		MatchManager.OnEndTurn+=OnEndTurn;
	}

	void OnDestroy(){
		MatchManager.OnEndTurn-=OnEndTurn;
	}

	public void OnEndTurnClicked(){
		MatchManager.Instance.EndTurn();
	}

	public void Update(){
		manaLabel.text = player.mana + "/" + Player.maxMana;
	}

	public void UpdateView(){
		container.Clear();
		foreach(var o in player.hand){
			container.CreateItem(o, cardPrefab);
		}
	}


    void OnEndTurn() {
    	if(selectedUnit!=null){
    		DragManager.OnPlacementDone-=OnPlacementDone;
    		selectedUnit = null;
    	}
    	if(MatchManager.IsMyTurn(player.id)){
	    	player.mana = Mathf.Min(Player.maxMana,player.mana+MatchManager.turn/2*2);
    	}
    }

    void OnPlacementDone(BaseCard obj) {
        DragManager.OnPlacementDone-=OnPlacementDone;
        player.RemoveFromHand(obj);
        selectedUnit = null;
        UpdateView();
    }

	void OnCardSelected(object obj) {
		selectedUnit = obj as BaseCard;
		if(MatchManager.IsMyTurn(id) && player.mana >= selectedUnit.summonCost){
	        DragManager.SetPlacementData(selectedUnit);
	        DragManager.OnPlacementDone+=OnPlacementDone;
    	}else{
    		selectedUnit = null;
    	}
    }
}
