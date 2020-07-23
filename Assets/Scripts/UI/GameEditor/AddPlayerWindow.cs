using UnityEngine;
using System.Collections.Generic;
using Newtonsoft.Json;

public class AddPlayerWindow : Window {
	[SerializeField] GameObject itemPrefab;
	const string settingsKey = "EditorUnitPresets";
	IUIContainer container;
	public List<BaseCard> presets;
	IUIContainerItem selection;
	BaseCard selectedData;
	void Awake(){
		container = GetComponent<IUIContainer>();
		container.OnItemClickAction+=OnSelect;
	}

    public override void Open() {
        base.Open();
        container.Clear();
        foreach(var o in Resources.LoadAll<DataContainer<BaseCard>>("Settings/")){
        	container.CreateItem(o.GetData(), itemPrefab);
        }
        if(PlayerPrefs.HasKey(settingsKey)){
	        presets = JsonConvert.DeserializeObject<List<BaseCard>>(PlayerPrefs.GetString(settingsKey));
	        foreach(var o in presets){
	        	container.CreateItem(o, itemPrefab);
	        }
    	}
    }

    void OnSelect(object obj) {
    	selectedData = obj as BaseCard;
    }

	public void AddSelected(){
		if(selectedData!=null){
			DragManager.SetPlacementData(selectedData);
			UIManager.CloseActiveWindow();
		}

	}

	public void RemoveSelected(){
		if(presets!=null && presets.Contains(selectedData)){
			presets.Remove(selectedData);
			selectedData = null;
			PlayerPrefs.SetString(settingsKey, JsonConvert.SerializeObject(presets, Formatting.Indented));
		}
	}

}
