using UnityEngine;
using System.Collections;

public class HitBody : MonoBehaviour {
	public static float attackThreshold = 1f;
	public Rigidbody2D rb;
	public int teamId;

	public virtual void OnCollisionEnter2D(Collision2D coll) {
		  if(coll.relativeVelocity.magnitude>attackThreshold){
            var creature = coll.gameObject.GetComponent<Creature>();
            if(creature!=null && creature.data.teamId != teamId && !creature.rb.isKinematic){
                Hit(creature.data.Damage);
                if(creature.hitActionReceiver!=null){
                    creature.hitActionReceiver.Action(coll.relativeVelocity.normalized, coll.relativeVelocity.magnitude);
                }
            }
        }
	}

    public virtual void Hit(float dmg){
    }
}
