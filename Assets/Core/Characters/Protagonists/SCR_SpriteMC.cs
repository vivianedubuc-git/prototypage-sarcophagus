using UnityEngine;

public class SCR_SpriteMC : MonoBehaviour
{
    public void Die()
    {
        SCR_MapManager.instance.ChangeScene("Defeat");
    }
}