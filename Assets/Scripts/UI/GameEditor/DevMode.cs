using UnityEngine;
using System.Collections;

public class DevMode : MonoBehaviour {
	public static bool isLive;
	[SerializeField] GameObject view;
	public void ToggleDevScreen(bool on){
		view.SetActive(on);
		isLive = on;
	}

	public void EditClicked(){
		if(SelectionManager.GetCurrentSelection()!=null){
			UIManager.ToggleWindow<PlayerSettingsWindow>();
		}
	}

	public void AddClicked(){
		UIManager.ToggleWindow<AddPlayerWindow>();
	}

	public void RemoveClicked(){
		if(SelectionManager.GetCurrentSelection()!=null){
			Destroy((SelectionManager.GetCurrentSelection() as Component).gameObject);
		}
	}

	public void FieldClicked(){
		UIManager.ToggleWindow<FieldSettingsWindow>();
	}
}
