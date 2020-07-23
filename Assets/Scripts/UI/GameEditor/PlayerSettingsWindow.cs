using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerSettingsWindow : Window {
	[SerializeField] PropView[] settings;
	Dictionary<string,InputField> editFields;
	BaseCard currentPlayer;
	void Awake(){
		editFields = new Dictionary<string,InputField>();
		foreach(var o in settings){
			editFields.Add(o.name, o.input);
		}
	}

	 public override void Open() {
        base.Open();
        currentPlayer = SelectionManager.GetCurrentSelection() as BaseCard;
        if(currentPlayer==null){
        	UIManager.CloseActiveWindow();
        }else{
        	// editFields["name"].text = currentPlayer.data.key;
        	// editFields["hp"].text = currentPlayer.data.hp.ToString();
        	// editFields["dmg"].text = currentPlayer.data.dmg.ToString();
        	// editFields["mass"].text = currentPlayer.data.mass.ToString();
        	// editFields["drag"].text = currentPlayer.data.drag.ToString();
        	// editFields["angulardrag"].text = currentPlayer.data.angularDrag.ToString();
        	// editFields["bounciness"].text = currentPlayer.data.bounciness.ToString();
        	// editFields["friction"].text = currentPlayer.data.friction.ToString();
        	// editFields["scale"].text = currentPlayer.data.scale.ToString();
         //    editFields["speedCutOff"].text = currentPlayer.data.speedCutOff.ToString();
         //    editFields["cutOffTime"].text = currentPlayer.data.cutOffTime.ToString();
         //    editFields["teamId"].text = currentPlayer.data.teamId.ToString();
        }

    }

	public void Save(){
		// var data = new UnitData();
		// data.key = editFields["name"].text;
  //   	data.hp = float.Parse(editFields["hp"].text.Trim());
  //   	data.dmg = float.Parse(editFields["dmg"].text.Trim());
  //   	data.mass = float.Parse(editFields["mass"].text.Trim());
  //   	data.drag = float.Parse(editFields["drag"].text.Trim());
  //   	data.angularDrag = float.Parse(editFields["angulardrag"].text.Trim());
  //   	data.bounciness = float.Parse(editFields["bounciness"].text.Trim());
  //   	data.friction = float.Parse(editFields["friction"].text.Trim());
  //   	data.scale = float.Parse(editFields["scale"].text.Trim());
  //       data.speedCutOff = float.Parse(editFields["speedCutOff"].text.Trim());
  //       data.cutOffTime = float.Parse(editFields["cutOffTime"].text.Trim());
  //       data.teamId = int.Parse(editFields["teamId"].text.Trim());
  //   	var presets = JsonConvert.DeserializeObject<List<UnitData>>(PlayerPrefs.GetString("EditorPlayerPresets")) ?? new List<UnitData>();
  //   	presets.Add(data);
  //   	PlayerPrefs.SetString("EditorPlayerPresets", JsonConvert.SerializeObject(presets, Formatting.Indented));
	}

	public void Apply(){
		// var data = new UnitData();
		// data.key = editFields["name"].text;
  //   	data.hp = float.Parse(editFields["hp"].text.Trim());
  //   	data.dmg = float.Parse(editFields["dmg"].text.Trim());
  //   	data.mass = float.Parse(editFields["mass"].text.Trim());
  //   	data.drag = float.Parse(editFields["drag"].text.Trim());
  //   	data.angularDrag = float.Parse(editFields["angulardrag"].text.Trim());
  //   	data.bounciness = float.Parse(editFields["bounciness"].text.Trim());
  //   	data.friction = float.Parse(editFields["friction"].text.Trim());
  //   	data.scale = float.Parse(editFields["scale"].text.Trim());
  //       data.speedCutOff = float.Parse(editFields["speedCutOff"].text.Trim());
  //       data.cutOffTime = float.Parse(editFields["cutOffTime"].text.Trim());
  //       data.teamId = int.Parse(editFields["teamId"].text.Trim());
  //   	currentPlayer.Setup(data);
  //   	UIManager.CloseActiveWindow();
	}
}
