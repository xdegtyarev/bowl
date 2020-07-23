using UnityEngine;
using System;

public class SelectionManager : MonoBehaviour {
	public static Action<ISelectable> selectedEvent;
	public static Action deselectedEvent;
	static ISelectable currentSelection;

	public void OnDisable(){
		currentSelection = null;
		deselectedEvent = null;
		selectedEvent = null;
	}

	public static ISelectable GetCurrentSelection(){
		return currentSelection;
	}

    public static bool IsCurrentlySelected( ISelectable selecatableObject ) {
        return currentSelection == selecatableObject;
    }

	// public static bool TryGetResponse(ITouchable touchable){
		// return currentSelection!=null ? currentSelection.TryRespond(touchable) : false;
		// return false;
	// }

	public static void ReregisterSelection(){
		var previousSelection = currentSelection;
		ResetCurrentSelection();
		RegisterSelection(previousSelection);
	}

	public static void RegisterSelection(ISelectable selection){
        ResetCurrentSelection();
		currentSelection = selection;
		currentSelection.OnSelect();
		if(selectedEvent!=null){
			selectedEvent(currentSelection);
		}
	}

	public static void ResetCurrentSelection(){
		if(currentSelection!=null){
			if(deselectedEvent!=null){
				deselectedEvent();
			}
			currentSelection.OnDeselect();
			currentSelection = null;
		}
	}
}
