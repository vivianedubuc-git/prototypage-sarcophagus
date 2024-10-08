using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_MapManager : MonoBehaviour
{
    static SCR_MapManager _instance;
    static public SCR_MapManager instance => _instance;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}