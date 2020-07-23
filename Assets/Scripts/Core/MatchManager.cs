using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {
	public static MatchManager Instance;
	public static event Action OnEndTurn;
	public static int turn;

	[SerializeField] float startTurnTime;
	[SerializeField] public int handSize;
	[SerializeField] Text turnTimerLabel;
	[SerializeField] Image turnTimerBacking;
	[SerializeField] Color team1Color;
	[SerializeField] Color team2Color;
	[SerializeField] GameObject unitPrefab;
	[SerializeField] GameObject spellPrefab;
	[SerializeField] GameObject field;
	[SerializeField] List<Player> players = new List<Player>();

	float currentTurnTime;

	public void AddPlayer(Player player){
		players.Add(player);
	}

	void Awake(){
		turn = 0;
		Instance = this;
		currentTurnTime = startTurnTime;
	}

	public static bool IsMyTurn(int id){
		return turn % 2 == id;
	}

	public static bool TryPayMana(int amount, int id){
		foreach(var o in Instance.players){
			if(o.id == id){
				if(o.mana>=amount){
					o.mana-=amount;
					return true;
				}
			}
		}
		return false;
	}

	void Update () {
		if(!DevMode.isLive){
			if(0f>(currentTurnTime-Time.deltaTime)){
				EndTurn();
			}else{
				currentTurnTime-=Time.deltaTime;
			}
			turnTimerLabel.text = ((int)currentTurnTime.Round(0))/60 + (((int)currentTurnTime.Round(0))%60>9 ? ":" : ":0") + ((int)currentTurnTime.Round(0))%60;
		}
	}

	public void Restart(){
		UnityEngine.SceneManagement.SceneManager.LoadScene(0);
	}

	public GameObject CreatePlayer(BaseCard unit,Vector2 pos){
		var go = Instantiate(unit is BaseCreature?unitPrefab:spellPrefab);
		go.transform.parent = field.transform;
		go.transform.position = pos;
		unit.Setup(go);
		return go;
	}

	public void EndTurn(){
		turn++;
		turnTimerBacking.color = turn%2>0 ? team1Color : team2Color;
		currentTurnTime = startTurnTime;
		SelectionManager.ResetCurrentSelection();
		if(OnEndTurn!=null){
			OnEndTurn();
		}
	}
}
