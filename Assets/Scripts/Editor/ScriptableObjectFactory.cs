// Based on Lior Tal code from
// https://github.com/liortal53/ScriptableObjectFactory
// http://www.tallior.com/unity-scriptableobject-factory/
//
// Scriptable object documentation 
// http://docs.unity3d.com/Manual/class-ScriptableObject.html

using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;


// Helper class for instantiating ScriptableObjects.
public class ScriptableObjectFactory {
	
	[MenuItem( "Assets/Create/ScriptableObject" )]
	public static void Create() {
		var assembly = GetAssembly();

		// Get all classes derived from ScriptableObject
		var allScriptableObjects = ( from t in assembly.GetTypes()
		                             where t.IsSubclassOf( typeof( ScriptableObject ) )
		                             select t).ToArray();

		// Show the selection window.
		var window = EditorWindow.GetWindow<ScriptableObjectWindow>( true, "Create a new ScriptableObject", true );
		window.ShowPopup();
		window.Types = allScriptableObjects;
	}

	// Returns the assembly that contains the script code for this project (currently hard coded)
	private static Assembly GetAssembly() {
		return Assembly.Load( new AssemblyName( "Assembly-CSharp" ) );
	}
}