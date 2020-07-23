using UnityEngine;

public class Shoot : MonoBehaviour, IActionReceiver {
    public float quanity;
    public float coneAngle;
    public BaseCreature bolid;
    public void Action(Vector3 dir, float val) {
        float angle = -coneAngle * 0.5f;
        for (int i = 0; i < quanity; i++, angle += (coneAngle / quanity)) {
            var bolidGo = MatchManager.Instance.CreatePlayer(bolid.Clone(), transform.position + dir);
            bolidGo.GetComponent<Rigidbody2D>().isKinematic = false;
            Debug.Log((Quaternion.Euler(0,0,angle) * (-dir)));
            bolidGo.GetComponent<Rigidbody2D>().AddForce((Quaternion.Euler(0,0,angle) * dir) *val, ForceMode2D.Impulse);
            Destroy(bolidGo, 2f);
            GetComponent<Rigidbody2D>().isKinematic = false;
        }
        GetComponent<Rigidbody2D>().AddForce((Quaternion.Euler(0, 0, angle) * (-dir)) * val * 0.1f, ForceMode2D.Impulse);
    }

    public void Setup(IActionReceiver other) {
        var pref = other as Shoot;
        quanity = pref.quanity;
        coneAngle = pref.coneAngle;
        bolid = pref.bolid;
        bolid.teamId = GetComponent<Creature>().data.teamId;
    }
}


