using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager _instance;
    public Transform _viewroot;
    public Dictionary<string, View> ViewList = new Dictionary<string, View>();
    private void Awake()
    {
        Init();
        _instance = this;
    }
    void Init()
    {
        for (int i = 0; i < _viewroot.childCount; i++)
        {
            View view=_viewroot.GetChild(i).GetComponent<View>();
            ViewList.Add(view.GetType().ToString(), _viewroot.GetChild(i).GetComponent<View>() );
        }
    }
    public T GetView<T>() where T : View
    {
        if (!ViewList.ContainsKey(typeof(T).ToString()))
        {
            Debug.LogError("找不到" + typeof(T).ToString());
            return null;
        }
     
        return ViewList[typeof(T).ToString()] as T;
    }
    public bool IsOpening<T>() where T : View
    {
        if (!ViewList.ContainsKey(typeof(T).ToString()))
        {
            Debug.LogError("找不到" + typeof(T).ToString());
            return false;
        }

        return ViewList[typeof(T).ToString()].gameObject.activeSelf;
    }
    public T OpenView<T>() where T:View
    {
        if (!ViewList.ContainsKey(typeof(T).ToString()))
        {
            Debug.LogError("找不到" + typeof(T).ToString());
            return null;
        }
        ViewList[typeof(T).ToString()].gameObject.SetActive(true);
        return ViewList[typeof(T).ToString()] as T;
    }
    public T CloseView<T>() where T : View
    {
        if (!ViewList.ContainsKey(typeof(T).ToString()))
        {
            Debug.LogError("找不到" + typeof(T).ToString());
            return null;
        }
        ViewList[typeof(T).ToString()].gameObject.SetActive(false);
        return ViewList[typeof(T).ToString()] as T;
    }
}
