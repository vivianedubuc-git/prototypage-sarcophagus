using System.Collections;
using TMPro;
using UnityEngine;

public class SCR_Situation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _button;
    private string _situation = "After several burglaries and contraband races through space, you and your comrade are preparing to carry out your last heist. You are heading towards the Sarcophagus, a veritable graveyard of ships coming from all corners of the galaxy. Rumors seem to indicate that unknown technologies are to be found. Unfortunately, your ship is damaged on the way and you have to dock right in the center of the Sarcophagus. You must quickly find your 3 missing spare parts and give them to your comrade, because strange creatures are lurking around...";
    private float _speed = 0.05f;

    private void Start()
    {
        StartCoroutine("CoroutineAnimateText");
    }

    private IEnumerator CoroutineAnimateText()
    {
        int index = 0;
        _text.text = "";
        while (index < _situation.Length)
        {
            _text.text += _situation[index];
            index++;
            yield return new WaitForSeconds(_speed);
        }
        _text.text = _situation;
        _button.SetActive(true);
    }
}
