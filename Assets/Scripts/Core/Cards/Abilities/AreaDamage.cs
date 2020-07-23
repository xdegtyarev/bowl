using UnityEngine;
using System.Collections;

public class AreaDamage : MonoBehaviour, IActionReceiver {
    public AffectType affectType;
    public float radius;
    public float damage;
    public float force;

    public void Action(Vector3 dir, float val) {
        foreach (var o in Physics2D.OverlapCircleAll(transform.position, radius,LayerMask.GetMask("Default"))) {
            if (o.GetComponent<Rigidbody2D>()) {
                o.GetComponent<Rigidbody2D>().isKinematic = false;
                o.GetComponent<Rigidbody2D>().AddForce((o.GetComponent<HitBody>().transform.position - transform.position).normalized * force, ForceMode2D.Impulse);
                switch (affectType) {
                    case AffectType.Self:
                        if (MatchManager.IsMyTurn(o.GetComponent<HitBody>().teamId)) {
                            o.GetComponent<HitBody>().Hit(damage);
                        }
                        break;
                    case AffectType.Enemy:
                        if (!MatchManager.IsMyTurn(o.GetComponent<HitBody>().teamId)) {
                            o.GetComponent<HitBody>().Hit(damage);
                        }
                        break;
                    case AffectType.All:
                        o.GetComponent<HitBody>().Hit(damage);
                        break;
                }
            }
        }

        var go = new GameObject();
        go.transform.position = gameObject.transform.position;
        go.AddComponent<SpriteRenderer>().sprite = SpriteBase.GetSprite("circular");
        go.transform.localScale = Vector3.one * radius * 0.2f;
        Destroy(gameObject);
        Destroy(go, 0.2f);
    }

    public void Setup(IActionReceiver other) {
        var pref = other as AreaDamage;
        affectType = pref.affectType;
        radius = pref.radius;
        damage = pref.damage;
        force = pref.force;
    }
}
