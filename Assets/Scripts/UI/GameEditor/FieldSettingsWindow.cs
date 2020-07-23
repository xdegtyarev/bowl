using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class FieldSettingsWindow : Window {
	[SerializeField] PropView[] settings;
	Dictionary<string,InputField> editFields;
	public static float minForce = 25;
	public static float maxForce = 300;
	[SerializeField] PhysicsMaterial2D borderMaterial;
	[SerializeField] PhysicsMaterial2D ballMaterial;
	[SerializeField] Rigidbody2D ballRb;

	void Awake(){
		editFields = new Dictionary<string,InputField>();
		foreach(var o in settings){
			editFields.Add(o.name, o.input);
		}
	}

	public override void Open() {
		editFields["maxForce"].text = maxForce.ToString();
		editFields["minForce"].text = minForce.ToString();
		editFields["borderBounce"].text = borderMaterial.bounciness.ToString();
		editFields["borderFriction"].text = borderMaterial.friction.ToString();

		editFields["ballMass"].text = ballRb.mass.ToString();
		editFields["ballDrag"].text = ballRb.drag.ToString();
		editFields["ballADrag"].text = ballRb.angularDrag.ToString();
		editFields["ballBounce"].text = ballMaterial.bounciness.ToString();
		editFields["ballFriction"].text = ballMaterial.friction.ToString();

		editFields["minDist"].text = (DragManager.minDistance * Screen.height / Screen.dpi * 2.54f).ToString();
		editFields["maxDist"].text = (DragManager.maxDistance * Screen.height / Screen.dpi * 2.54f).ToString();
		// editFields["attackThreshold"].text = Unit.attackThreshold.ToString();
	}

	public void Apply(){
    	maxForce = float.Parse(editFields["maxForce"].text.Trim());
    	minForce = float.Parse(editFields["minForce"].text.Trim());
    	borderMaterial.bounciness = float.Parse(editFields["borderBounce"].text.Trim());
    	borderMaterial.friction = float.Parse(editFields["borderFriction"].text.Trim());

    	ballRb.mass = float.Parse(editFields["ballMass"].text);
		ballRb.drag = float.Parse(editFields["ballDrag"].text);
		ballRb.angularDrag = float.Parse(editFields["ballADrag"].text);
		ballMaterial.bounciness = float.Parse(editFields["ballBounce"].text);
		ballMaterial.friction = float.Parse(editFields["ballFriction"].text);

		DragManager.minDistance = float.Parse(editFields["minDist"].text) / Screen.height * Screen.dpi / 2.54f;
		DragManager.maxDistance = float.Parse(editFields["maxDist"].text) / Screen.height * Screen.dpi / 2.54f;
		// Unit.attackThreshold = float.Parse(editFields["attackThreshold"].text);

    	UIManager.CloseActiveWindow();
	}
}
