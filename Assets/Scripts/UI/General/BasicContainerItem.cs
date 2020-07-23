using UnityEngine;

public class BasicContainerItem : MonoBehaviour, IUIContainerItem {

    public void Setup(object itemData, IUIContainer container) {}
    public void UpdateState() {}
    public GameObject GetGameObject() {
        return gameObject;
    }
}
