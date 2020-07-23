using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Construction : HitBody{
    float currentHp;
    public float hp;
    public Image progressBar;

    void Awake(){
        currentHp = hp;
    }

    public override void Hit(float dmg){
        Debug.Log("hit wall" + dmg + " dmg");
        currentHp -= dmg;
        progressBar.fillAmount = currentHp/hp;
        if(currentHp<=0){
            Destroy(gameObject);
        }
    }
}
