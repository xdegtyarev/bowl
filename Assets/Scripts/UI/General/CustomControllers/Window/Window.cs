using UnityEngine;
using UnityEngine.EventSystems;

public class Window : MonoBehaviour {

    private bool IsOpened { get; set; }

    public virtual void Open() {
        if (IsOpened) {
            return;
        }
        IsOpened = true;
    }

    public virtual void Close() {
        if (!IsOpened) {
            return;
        }
        IsOpened = false;
    }

    public GameObject GetFirstSelected() {
        return null;
    }

    public virtual void OnFocusBecome() {
    }
    public virtual void OnFocusLost() {
    }

    protected virtual void OnEnable() {

    }

    protected virtual void OnDisable() {
    }

    protected void SelectFirstControl() {
        EventSystem.current.SetSelectedGameObject(GetFirstSelected());
    }

}


