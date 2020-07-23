using UnityEngine;
using System.Collections;

public enum AffectType {
    Self,
    Enemy,
    All
}

public class AreaBuff : MonoBehaviour, IActionReceiver {
    public float radius;
    public AffectType affectType;
    public PropertyModifier[] modifiers;

    public void Action(Vector3 dir, float val) {
        foreach (var o in Physics2D.OverlapCircleAll(transform.position, radius,LayerMask.GetMask("Default"))) {
            if (o.GetComponent<Rigidbody2D>() && o.GetComponent<Creature>()) {
                switch (affectType) {
                    case AffectType.Self:
                        if (MatchManager.IsMyTurn(o.GetComponent<Creature>().teamId)) {
                            foreach (var p in modifiers) {
                                o.GetComponent<Creature>().ApplyPropertyModifier(p);
                            }
                        }
                        break;
                    case AffectType.Enemy:
                        if (!MatchManager.IsMyTurn(o.GetComponent<Creature>().teamId)) {
                            foreach (var p in modifiers) {
                                o.GetComponent<Creature>().ApplyPropertyModifier(p);
                            }
                        }
                        break;
                    case AffectType.All:
                        foreach (var p in modifiers) {
                            o.GetComponent<Creature>().ApplyPropertyModifier(p);
                        }
                        break;

                }

            }
        }
        gameObject.AddComponent<SpriteRenderer>().sprite = SpriteBase.GetSprite("circular");
        gameObject.transform.localScale = Vector3.one * radius * 0.2f;
        Destroy(gameObject, 0.2f);
    }

    public void Setup(IActionReceiver other) {
        var pref = other as AreaBuff;
        radius = pref.radius;
        affectType = pref.affectType;
        modifiers = new PropertyModifier[pref.modifiers.Length];
        for (int i = 0; i < pref.modifiers.Length; i++) {
            modifiers[i] = pref.modifiers[i].Clone();
        }

    }
}
