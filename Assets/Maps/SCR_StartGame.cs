using GamingIsLove.ORKFramework;
using UnityEngine;

public class SCR_StartGame : MonoBehaviour
{
    private void Start()
    {
        ORK.Game.CallStartSchematic();
    }
}