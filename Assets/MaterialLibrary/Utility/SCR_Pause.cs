using UnityEngine;

public class SCR_Pause : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private GameObject _canvasInventory;
    private bool _isPaused;
    public bool isPaused
    {
        get { return _isPaused; }
    }

    private void Start()
    {
        _isPaused = _pauseMenu.activeSelf;
        _canvasInventory.SetActive(!_isPaused);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            _isPaused = !_isPaused;
            _pauseMenu.SetActive(_isPaused);
            _canvasInventory.SetActive(!_isPaused);
            SCR_SoundManager.instance.PlaySound(SCR_SoundManager.instance.soundClic);

            if (_isPaused)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
