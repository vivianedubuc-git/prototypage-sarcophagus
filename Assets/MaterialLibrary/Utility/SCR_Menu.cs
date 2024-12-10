using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Menu : MonoBehaviour
{
    public void QuitGame()
    {
        SCR_MapManager.instance.QuitGame();
    }
}