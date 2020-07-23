using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour, IActionReceiver {
    public void Action(UnityEngine.Vector3 dir, float val) {
        GetComponent<Rigidbody2D>().isKinematic = false;
        GetComponent<Rigidbody2D>().AddForce(dir*val, ForceMode2D.Impulse);
    }

    public void Setup(IActionReceiver other){
    }
}
