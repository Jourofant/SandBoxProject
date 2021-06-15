using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 窗体基类
/// </summary>
public abstract class BaseWin : MonoBehaviour
{
    /// <summary>
    /// 窗体类型
    /// </summary>
    protected WinType winType = WinType.NULL;
    /// <summary>
    /// 窗体打开类型
    /// </summary>
    protected WinOpenType winOpenType = WinOpenType.NULL;
    /// <summary>
    /// 上一个打开的窗体类型
    /// </summary>
    protected WinType lastWinType = WinType.NULL;

    public WinOpenType WinOpenType{
        get {
            return winOpenType;
        }
    }

    /// <summary>
    /// 初始化窗体数据
    /// </summary>
    protected abstract void InitializeWin();

    void Awake()
    {
        InitializeWin(); 
        transform.localPosition = new Vector3(0, 0, 0);
        transform.localScale = new Vector3(1, 1, 1);
        print("===================== " + "OPEN WIN ：" + winType + " ======================");
    }

    /// <summary>
    /// 打开窗体触发
    /// </summary>
    protected abstract void OpenWin();

    /// <summary>
    /// 添加事件监听方法，打开窗体后触发，执行顺序晚于OpenWin()
    /// </summary>
    protected abstract void AddEvent();

    private void OnEnable()
    {
        OpenWin();
        AddEvent();
    }

    /// <summary>
    /// 关闭窗体触发
    /// </summary>
    protected abstract void CloseWin();

    /// <summary>
    /// 移除事件监听方法，关闭窗体前触发，执行顺序先于CloseWin()
    /// </summary>
    protected abstract void RemoveEvent();

    private void OnDisable()
    {
        RemoveEvent();
        CloseWin();
    }

    void Update()
    {
        
    }
}