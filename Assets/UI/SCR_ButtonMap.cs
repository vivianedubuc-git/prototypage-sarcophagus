using UnityEngine;

public class SCR_ButtonMap : MonoBehaviour
{
    public void ChangeScene(string scene)
    {
        SCR_SoundManager.instance.PlaySound(SCR_SoundManager.instance.soundButton);
        SCR_MapManager.instance.ChangeScene(scene);
    }
}