using UnityEngine;
using UnityEditor;

public class MenuEditorTools : Editor {
	[MenuItem("xdegtyarev/BreakPrefabLink")]
	public static void BreakPrefabLinkFunc (MenuCommand command) {
		PrefabUtility.DisconnectPrefabInstance(Selection.activeGameObject);
		Debug.Log(("Breaking prefab link for " + Selection.activeGameObject));
	}

	[MenuItem("xdegtyarev/ClearPlayerPrefs")]
	public static void ClearPlayerPrefs (MenuCommand command) {
		PlayerPrefs.DeleteAll();
		Debug.Log(("Cleared playerprefs"));
	}

    [MenuItem( "xdegtyarev/Create Empty Child &#n" )]
    static void EmptyChild() {
        GameObject go = new GameObject( "Empty" );
        go.transform.parent = Selection.activeTransform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
        Selection.activeGameObject = go;
    }
}
