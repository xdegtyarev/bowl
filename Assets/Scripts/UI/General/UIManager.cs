using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;


public class UIManager:MonoBehaviour {
    [SerializeField]List<Window> windowBase;
    public static Dictionary<System.Type,Window> windows;

    public static event System.Action<Window> OnWindowOpen;
    public static event System.Action<Window> OnWindowClose;

    static Window activeWindow;

    public static T ToggleWindow<T>() where T:Window {
        T window = windows[typeof(T)] as T;
        GenericToggleWindow(window);
        return window;
    }

    public static T GetWindow<T>() where T:Window {
        if (windows.ContainsKey(typeof(T))) {
            return windows[typeof(T)] as T;
        } else {
            return null;
        }
    }

    static void GenericToggleWindow(Window window) {
        if (window.gameObject.activeSelf) {
            CloseWindow(window);
        } else {
            OpenWindow(window);
        }
    }

    static void OpenWindow(Window window) {
        if (activeWindow != null) {
            CloseActiveWindow();
        }
        activeWindow = window;
        window.gameObject.SetActive(true);
        if (OnWindowOpen != null) {
            OnWindowOpen(window);
        }
        window.Open();
    }


    public static void CloseActiveWindow() {
        if (activeWindow != null) {
            CloseWindow(activeWindow);
        }
    }

    public static bool IsAnyWindowOpen() {
        return activeWindow != null;
    }

    public static bool IsWindowOpen(Window window) {
        return (window != null && window == activeWindow);
    }

    static void CloseWindow(Window window, bool disableBlur = true) {
        window.Close();
        window.gameObject.SetActive(false);
        activeWindow = null;
        if (OnWindowClose != null) {
            OnWindowClose(window);
        }
    }

    void Awake() {
        windows = new Dictionary<System.Type,Window>();
        foreach (var o in windowBase) {
            windows.Add(o.GetType(), o);
        }
    }
}
