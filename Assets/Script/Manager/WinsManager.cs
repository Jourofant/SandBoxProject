using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 窗体管理器
/// </summary>
public class WinsManager : Singleton<WinsManager>
{
    /// <summary>
    /// 窗体链
    /// </summary>
    private Stack<WinType> showedWins = new Stack<WinType>();
    /// <summary>
    /// 打开过的窗体缓存
    /// </summary>
    private Dictionary<WinType, GameObject> allShowedWins = new Dictionary<WinType, GameObject>();
    /// <summary>
    /// 当前正在显示的窗体
    /// </summary>
    private Dictionary<WinType, GameObject> showingWins = new Dictionary<WinType, GameObject>();

    //开始声明各layer父节点
    private Transform Layer_Full_Root;
    private Transform Layer_Fly_Root;
    private Transform Layer_Top_Root;

    private void Awake()
    {
        Layer_Full_Root = GameObject.Find("Layer_Full").transform;
        Layer_Fly_Root = GameObject.Find("Layer_Fly").transform;
        Layer_Top_Root = GameObject.Find("Layer_Top").transform;
    }

    public void OpenWin(WinType winType) {
        OpenWin(winType, WinOpenType.FULL);
    }

    public void OpenWin(WinType winType, WinOpenType winOpenType) {
        if (CheckIsShowing(winType))
        {
            //窗体已在显示的情况
            return;
        }
        else if (CheckIsShowed(winType))
        {
            //窗体打开过的情况
            GameObject curWin = null;
            allShowedWins.TryGetValue(winType, out curWin);
            if (curWin != null)
            {
                curWin.gameObject.SetActive(true);
                showingWins.Add(winType, curWin);
            }
        }
        else {
            //窗体未打开过的情况
            string winPath = WinsPrefabPath.Instance.GetPath(winType);
            GameObject curWin = Resources.Load<GameObject> (winPath);
            Transform curWinRoot = GetWinRoot(winOpenType);
            Instantiate(curWin, curWinRoot);
            showingWins.Add(winType, curWin);
            allShowedWins.Add(winType, curWin);
        }
    }

    public void CloseWin(WinType winType) 
    {
        if (CheckIsShowing(winType))
        {
            GameObject curWin = null;
            showingWins.TryGetValue(winType, out curWin);
            if (curWin != null) {
                curWin.SetActive(false);
                showingWins.Remove(winType);
            }
        }
        else 
        {
            return;
        }
    }

    /// <summary>
    /// 检查窗体是否正在显示
    /// </summary>
    /// <param name="winType">目标窗体类型</param>
    /// <returns></returns>
    public bool CheckIsShowing(WinType winType) {
        return showingWins.ContainsKey(winType);
    }

    /// <summary>
    /// 检查窗体是否显示过
    /// </summary>
    /// <param name="winType">目标窗体类型</param>
    /// <returns></returns>
    public bool CheckIsShowed(WinType winType)
    {
        return allShowedWins.ContainsKey(winType);
    }

    public Transform GetWinRoot(WinOpenType winOpenType) {
        switch (winOpenType)
        {
            case WinOpenType.FULL:
                return Layer_Full_Root;
            case WinOpenType.FLY:
                return Layer_Fly_Root;
            case WinOpenType.TOP:
                return Layer_Top_Root;
            default:
                break;
        }
        Debug.LogError("========== 未找到对应窗体打开类型的父结点 ===========");
        return null;
    }

}

