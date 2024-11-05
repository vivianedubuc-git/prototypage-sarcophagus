using GamingIsLove.ORKFramework;
using UnityEngine;

public class SCR_TestGameOver : MonoBehaviour
{
    void Start()
    {
        Invoke("Die", 5);
    }

    private void Die()
    {
        ORK.Game.GameOver();
    }
}