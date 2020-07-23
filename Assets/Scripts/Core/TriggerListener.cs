using UnityEngine;
using System;

public class TriggerListener : MonoBehaviour {
	public event Action TriggeredEvent;
  	void OnTriggerEnter2D(Collider2D other) {
  		if(TriggeredEvent!=null){
  			TriggeredEvent();
  		}
    }
}
