using UnityEngine;

public class RailShot : MonoBehaviour, IActionReceiver {
	public float width;
	public AffectType affectType;
	public float damage;

    public void Action(Vector3 dir, float val) {
    	foreach(var o in Physics2D.CircleCastAll(transform.position, width, dir, 100f,LayerMask.GetMask("Default"))){
    		var creature = o.collider.gameObject.GetComponent<HitBody>();
    		if(creature!=null){
    			switch (affectType) {
                    case AffectType.Self:
                    	if(creature.teamId==GetComponent<HitBody>().teamId){
                    		creature.Hit(damage);
                    	}
                        break;
                    case AffectType.Enemy:
                    	if(creature.teamId!=GetComponent<HitBody>().teamId){
                    		creature.Hit(damage);
                    	}
                        break;
                    case AffectType.All:
                    	creature.Hit(damage);
                        break;
                }
    		}
    	}
    	//draw ray
    }

    public void Setup(IActionReceiver other){
    	var pref = other as RailShot;
    	width = pref.width;
    	affectType = pref.affectType;
    	damage = pref.damage;
    }
}
