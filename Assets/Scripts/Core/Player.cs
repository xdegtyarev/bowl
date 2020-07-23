using UnityEngine;
using System.Collections.Generic;

public class Player {
    public int id;
    public List<BaseCard> hand = new List<BaseCard>();
    public const int maxMana = 10;
    public int mana = 4;

    public void FillHand() {
        for (int i = hand.Count; i < MatchManager.Instance.handSize; i++) {
            BaseCard data;
            int attempts = 0;
            do{
                data = Resources.LoadAll<DataContainer<BaseCard>>("Settings/").Random().GetData().Clone();
                attempts++;
            }while(HasSuchCardInHand(data.key) && attempts < 50);
            data.teamId = id;
            hand.Add(data);
        }
    }

    bool HasSuchCardInHand(string key){
        foreach(var o in hand){
            if(o.key == key){
                return true;
            }
        }
        return false;
    }

    public void RemoveFromHand(BaseCard data) {
        hand.Remove(data);
        FillHand();
    }

}
