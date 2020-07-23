using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Creature : HitBody,ISelectable,IPointerClickHandler,IPointerDownHandler {
    public static float attackThreshold = 1f;
    public SpriteRenderer view;
    public GameObject bloodLustView;
    public GameObject freezeView;
    public SpriteRenderer selection;
    public BaseCreature data;
    public Image progressBar;
    public IActionReceiver actionReceiver;
    public IActionReceiver hitActionReceiver;
    bool activated;

    public virtual void Setup(BaseCreature creatureData){
        data = creatureData;
        data.currentHp = data.hp;
        rb = GetComponent<Rigidbody2D>();
        rb.angularDrag = data.angularDrag;
        rb.drag = data.drag;
        rb.mass = data.mass;
        var coll = GetComponent<Collider2D>();
        var mat = new PhysicsMaterial2D();
        mat.bounciness = data.bounciness;
        mat.friction = data.friction;
        coll.sharedMaterial = mat;
        transform.localScale = Vector3.one*creatureData.scale;
        view.sprite = SpriteBase.GetSprite(data.view+data.teamId);
        teamId = data.teamId;
        progressBar.gameObject.SetActive(true);
        if(data.Action!=null){
            actionReceiver = gameObject.AddComponent(data.Action.GetType()) as IActionReceiver;
            actionReceiver.Setup(data.Action as IActionReceiver);
        }

        if(data.HitAction!=null){
            hitActionReceiver = gameObject.AddComponent(data.HitAction.GetType()) as IActionReceiver;
            hitActionReceiver.Setup(data.HitAction as IActionReceiver);
        }

        MatchManager.OnEndTurn+=OnEndTurn;
    }

    public void ApplyPropertyModifier(PropertyModifier prop){
        data.activePropertyModifiers.Add(prop.Clone());
    }

    public PropertyModifier GetPropertyModifier(string key){
        foreach(var o in data.activePropertyModifiers){
            if(o.propName == key){
                return o;
            }
        }
        return null;
    }

    public void Action (Vector3 dir, float val){
        if (MatchManager.IsMyTurn(data.teamId) && !activated && GetPropertyModifier("freeze")==null) {
            if(actionReceiver!=null){
                actionReceiver.Action(dir, val);
                activated = true;
                view.color = Color.grey;
            }
        }
    }

    public void OnEndTurn(){
        activated = false;
        view.color = Color.white;
        for (int i = 0; i < data.activePropertyModifiers.Count; i++) {
            data.activePropertyModifiers[i].turns--;
            if(data.activePropertyModifiers[i].turns<0){
                data.activePropertyModifiers.RemoveAt(i);
                i--;
            }
        }
    }

    public void OnDestroy(){
        MatchManager.OnEndTurn-=OnEndTurn;
    }

    public virtual void OnPointerDown(PointerEventData eventData) {
        if (MatchManager.IsMyTurn(data.teamId)) {
            OnPointerClick(eventData);
            DragManager.Instance.OnBeginDrag(eventData);
        }
    }

    public virtual void OnPointerClick(PointerEventData eventData) {
        if (MatchManager.IsMyTurn(data.teamId)) {
            if(SelectionManager.GetCurrentSelection()==this){
                SelectionManager.ResetCurrentSelection();
            }else{
                SelectionManager.RegisterSelection(this);
            }
        }
    }

    void OnDisable(){
        if(SelectionManager.GetCurrentSelection() == this){
            SelectionManager.ResetCurrentSelection();
        }
    }

    void Update(){
        freezeView.SetActive(GetPropertyModifier("freeze")!=null);
        bloodLustView.SetActive(GetPropertyModifier("bloodlust")!=null);

    }


    public virtual void FixedUpdate(){
        if(GetPropertyModifier("freeze")!=null){
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
        }else{
            if(!rb.isKinematic){
                if(rb.velocity.magnitude<=data.speedCutOff){
                    rb.velocity = rb.velocity - (rb.velocity.normalized*data.speedCutOff/data.cutOffTime*Time.fixedDeltaTime);
                    if(rb.velocity.magnitude <= (data.speedCutOff*0.1f)){
                        rb.velocity = Vector2.zero;
                        rb.isKinematic = true;
                    }
                }
            }
        }
    }

    public void OnDeselect() {
        selection.gameObject.SetActive(false);
    }
    public void OnSelect() {
        selection.gameObject.SetActive(true);
    }

    public override void Hit(float dmg){
        Debug.Log("hit with " + dmg + " dmg");
        data.currentHp -= dmg;
        progressBar.fillAmount = ((float)data.currentHp)/data.hp;
        if(data.currentHp<=0){
            Destroy(gameObject);
        }
    }
}
