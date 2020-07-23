using System.Collections;
using UnityEngine;

public class Shield : MonoBehaviour,IActionReceiver {
    public float radius;
    public float force;
    public int liveLengthInTurns;
    public BaseCreature shield;

    GameObject shieldGo;
    int turn;

    void OnEndTurn() {
        turn++;
        if (turn == liveLengthInTurns) {
            Destroy(shieldGo);
            shieldGo = null;
            MatchManager.OnEndTurn -= OnEndTurn;
        }
    }

    public void Action(Vector3 dir, float val) {
        if (shieldGo == null) {
            foreach (var o in Physics2D.OverlapCircleAll(transform.position, radius,LayerMask.GetMask("Default"))) {
                if (gameObject != o.gameObject && o.GetComponent<Creature>() && o.GetComponent<Rigidbody2D>()) {
                    o.GetComponent<Rigidbody2D>().isKinematic = false;
                    o.GetComponent<Rigidbody2D>().AddForce((o.GetComponent<Creature>().transform.position - transform.position).normalized * force, ForceMode2D.Impulse);
                }
            }
            StartCoroutine(SpawnCoroutine());
        }
    }

    IEnumerator SpawnCoroutine() {
        yield return new WaitForSeconds(0.5f);
        shield.teamId = gameObject.GetComponent<Creature>().data.teamId;
        shieldGo = MatchManager.Instance.CreatePlayer(shield, transform.position);
        turn = 0;
        MatchManager.OnEndTurn += OnEndTurn;
    }

    public void Setup(IActionReceiver other) {
        var pref = other as Shield;
        radius = pref.radius;
        shield = pref.shield;
        force = pref.force;
        liveLengthInTurns = pref.liveLengthInTurns;
    }

}
