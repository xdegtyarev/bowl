public interface IActionReceiver{
	void Action(UnityEngine.Vector3 dir, float val);
	void Setup(IActionReceiver prefab);
}