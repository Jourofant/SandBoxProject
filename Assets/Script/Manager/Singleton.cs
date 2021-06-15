using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例基类
/// </summary>
public class Singleton <T> : MonoBehaviour where T : Component
{
    protected static GameObject root;
    protected static T instance;

    public static T Instance 
    {
        get {
            if (instance == null) {
                if (root == null) {
                    root = GameObject.Find("ManagerRoot").gameObject;
                }
                instance = root.GetComponent<T>();
            }
            return instance;
        }
    }

    protected Singleton() { }

}
