using System.Collections;
using TMPro;
using UnityEngine;

public class SCR_Situation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text1;
    [SerializeField] private TextMeshProUGUI _text2;
    [SerializeField] private TextMeshProUGUI _text3;
    [SerializeField] private TextMeshProUGUI _text4;
    [SerializeField] private TextMeshProUGUI _text5;
    [SerializeField] private GameObject _button;
    //[SerializeField] private AudioClip _taptapSound;
    private string _situation1 = "After countless smuggling runs across the local nebula cluster, you and your partner, Lily, ultimately come to the conclusion that it may be time to consider retirement. Embracing this idea of the score to end all scores, you both decide to head for the Sarcophagus.";
    private string _situation2 = "The Sarcophagus is the largest known graveyard of abandoned and derelict spaceships in the charted regions of the Milky Way. It continually attracts private spacefarers and adventurers due to the immense amount of rare and potentially undiscovered resources and technologies it’s rumored to contain. Very few are brave enough to venture closer to its center.";
    private string _situation3 = "A large metal object slams against the main engine of your ship, and you’re eventually forced to make an emergency docking at a strange-looking ship. Its model is entirely unknown to either you or Lily.";
    private string _situation4 = "You equip your trusty exosuit. It requires a charged battery for all its defensive functions to work properly. You hope to find charging stations should the need arise.";
    private string _situation5 = "Your objectives are clear: find 3 spare parts aboard this massive structure in order to repair your only means of escape from this metal tomb. If only it were so simple…";
    private float _speed = 0.05f;
    private int _pause = 1;
    private bool _hasClicked = false;

    private void Start()
    {
        StartCoroutine(CoroutineAnimateText());
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
        _text1.text = "";
        _text2.text = "";
        _text3.text = "";
        _text4.text = "";
        _text5.text = "";

        int index = 0;
        while (index < _situation1.Length && !_hasClicked)
        {
            _text1.text += _situation1[index];
            index++;
            yield return new WaitForSeconds(_speed);
        }
        _text1.text = _situation1;
        _hasClicked = false;
        yield return new WaitForSeconds(_pause);

        index = 0;
        while (index < _situation2.Length && !_hasClicked)
        {
            _text2.text += _situation2[index];
            index++;
            yield return new WaitForSeconds(_speed);
        }
        _text2.text = _situation2;
        _hasClicked = false;
        yield return new WaitForSeconds(_pause);

        index = 0;
        while (index < _situation3.Length && !_hasClicked)
        {
            _text3.text += _situation3[index];
            index++;
            yield return new WaitForSeconds(_speed);
        }
        _text3.text = _situation3;
        _hasClicked = false;
        yield return new WaitForSeconds(_pause);

        index = 0;
        while (index < _situation4.Length && !_hasClicked)
        {
            _text4.text += _situation4[index];
            index++;
            yield return new WaitForSeconds(_speed);
        }
        _text4.text = _situation4;
        _hasClicked = false;
        yield return new WaitForSeconds(_pause);

        index = 0;
        while (index < _situation5.Length && !_hasClicked)
        {
            _text5.text += _situation5[index];
            index++;
            yield return new WaitForSeconds(_speed);
        }
        _text5.text = _situation5;
        _hasClicked = false;
        yield return new WaitForSeconds(_pause);

        _button.SetActive(true);
    }
}
