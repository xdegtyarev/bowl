//ISelectable means that all control is done in object itself
public interface ISelectable
{
	// bool TryRespond( touchable);
	void OnDeselect ();
	void OnSelect();
}