using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_SpriteMC : MonoBehaviour
{
    public void Die()
    {
        SCR_MapManager.instance.ChangeScene("Defeat");
    }
}
