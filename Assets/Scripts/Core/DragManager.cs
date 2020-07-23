using UnityEngine;
using UnityEngine.EventSystems;

public class DragManager : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public static DragManager Instance;
    public static float minDistanceInCm = 0.2f;
    public static float maxDistanceInCm = 1.2f;
    public static float minDistance;
    public static float maxDistance;
    [SerializeField] float minForce = 10;
    [SerializeField] float maxForce = 200;
    [SerializeField] GameObject arrowView;
    Rigidbody2D targetBody;
    Vector2 start;
    public static System.Action<BaseCard> OnPlacementDone;

    void OnEndTurn() {
        placementData = null;
    }


    void Awake() {
        Instance = this;
        minDistance = minDistanceInCm / Screen.height * Screen.dpi / 2.54f;
        maxDistance = maxDistanceInCm / Screen.height * Screen.dpi / 2.54f;
        Application.targetFrameRate = 100;
        SelectionManager.selectedEvent += OnSelected;
        SelectionManager.deselectedEvent += OnDeselected;
        MatchManager.OnEndTurn+=OnEndTurn;
    }

    public void OnSelected(ISelectable obj) {
        targetBody = (obj as MonoBehaviour).GetComponent<Rigidbody2D>();
    }

    public void OnDeselected() {
        targetBody = null;
    }

    static BaseCard placementData;

    public static void SetPlacementData(BaseCard data){
        placementData  = data;
    }

    public void OnPointerClick(PointerEventData eventData) {
        if(placementData!=null && MatchManager.IsMyTurn(placementData.teamId) && MatchManager.TryPayMana(placementData.summonCost, placementData.teamId)){
            MatchManager.Instance.CreatePlayer(placementData,Camera.main.ScreenToWorldPoint(eventData.position));
            if(OnPlacementDone!=null){
                OnPlacementDone(placementData);
            }
            placementData = null;
        }
    }

    public virtual void OnBeginDrag(PointerEventData eventData) {
        if (targetBody != null) {
            start = eventData.position;
            arrowView.transform.position = targetBody.transform.position;
        }
    }

    public virtual void OnEndDrag(PointerEventData eventData) {
        if (targetBody != null) {
            var distance = Vector2.Distance(start, eventData.position);
            if (distance > minDistance * Screen.height) {
                targetBody.GetComponent<Creature>().Action((start - eventData.position).normalized,Mathf.Lerp(FieldSettingsWindow.minForce, FieldSettingsWindow.maxForce, distance / (maxDistance * Screen.height)));
                arrowView.transform.localScale = Vector3.zero;
                targetBody = null;
                SelectionManager.ResetCurrentSelection();
            }

        }
    }

    public virtual void OnDrag(PointerEventData eventData) {
        if (targetBody != null) {
            var distance = Vector2.Distance(start, eventData.position);
            if (distance > minDistance * Screen.height) {
                arrowView.transform.localScale = Vector3.one * Mathf.Lerp(0.2f, 1.2f, distance / (Screen.height * maxDistance));
                var totalDelta = (start - eventData.position).normalized;
                arrowView.transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(-totalDelta.x, totalDelta.y));
            }else{
                arrowView.transform.localScale = Vector3.zero;
            }
        }
    }

}
