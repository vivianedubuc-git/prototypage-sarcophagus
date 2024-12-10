using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SCR_MapManager : MonoBehaviour
{
    [SerializeField] private Image _fadeIn;
    static SCR_MapManager _instance;
    static public SCR_MapManager instance => _instance;
    private int _animationDuration = 2;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            AnimateOpening();
        }
        else { Destroy(gameObject); return; }
        DontDestroyOnLoad(gameObject);
        if (GetSceneName() == "Game" && SCR_SoundManager.instance != null) SCR_SoundManager.instance.music.AdjustVolume(1);
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
        if (scene == "Game") SCR_SoundManager.instance.music.AdjustVolume(1);
        else 
        {
            Time.timeScale = 1;
            SCR_SoundManager.instance.music.AdjustVolume(0);
        }
    }

    private void AnimateOpening()
    {
        if (_fadeIn != null)
        {
            _fadeIn.gameObject.SetActive(true);
            StartCoroutine("CoroutineAnimateOpening");
        }
    }

    private IEnumerator CoroutineAnimateOpening()
    {
        float time = 0;
        Color initialColor = new Color(_fadeIn.color.r, _fadeIn.color.g, _fadeIn.color.b, 1);
        while (time < _animationDuration)
        {
            Vector4 color = Vector4.Lerp(initialColor, new Color(0, 0, 0, 0), (time / _animationDuration));
            _fadeIn.color = color;
            time += Time.deltaTime;
            yield return null;
        }
        _fadeIn.gameObject.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
#if (UNITY_EDITOR)
        if (EditorApplication.isPlaying)
        {
            EditorApplication.ExitPlaymode();
        }
#endif
    }
}