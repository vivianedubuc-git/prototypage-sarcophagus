using System.Collections;
using TMPro;
using UnityEngine;

public class SCR_Situation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private GameObject _button;
    private string _situation = "After countless smuggling runs across the local nebula cluster, you and your partner, Lily, ultimately come to the conclusion that it may be time to consider retirement. Embracing this idea of the score to end all scores, you both decide to head for the Sarcophagus. The Sarcophagus is the largest known graveyard of abandoned and derelict spaceships in the charted regions of the Milky Way. It continually attracts private spacefarers and adventurers due to the immense amount of rare and potentially undiscovered resources and technologies it’s rumored to contain. Very few are brave enough to venture closer to its center. A large metal object slams against the main engine of your ship, and you’re eventually forced to make an emergency docking at a strange-looking ship. Its model is entirely unknown to either you or Lily. You equip your trusty exosuit. It requires a charged battery for all its defensive functions to work properly. You hope to find charging stations should the need arrive. Your objectives are clear: find 3 spare parts aboard this massive structure in order to repair your only means of escape from this metal tomb. If only it were so simple…";
    private float _speed = 0.05f;
    private bool _hasClicked = false;

    private void Start()
    {
        StartCoroutine("CoroutineAnimateText");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _hasClicked = true;
        }
    }

    private IEnumerator CoroutineAnimateText()
    {
        int index = 0;
        _text.text = "";
        while (index < _situation.Length && !_hasClicked)
        {
            _text.text += _situation[index];
            index++;
            yield return new WaitForSeconds(_speed);
        }
        _text.text = _situation;
        _button.SetActive(true);
    }
}
