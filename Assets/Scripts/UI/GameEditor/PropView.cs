using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PropView : MonoBehaviour {
	[SerializeField] Text label;
	[SerializeField] public InputField input;

	void Awake(){
		label.text = name;
	}
	public void OnInputEnd(){

	}
}
