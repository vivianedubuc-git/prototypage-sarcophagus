using UnityEngine;

public class SCR_ButtonMap : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        SCR_MapManager.instance.ChangeScene(scene);
    }
}