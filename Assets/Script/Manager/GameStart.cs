using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        WinsManager.Instance.OpenWin(WinType.GAME_MAIN_WIN);
    }

}
