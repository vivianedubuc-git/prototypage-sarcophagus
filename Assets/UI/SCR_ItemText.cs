using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SCR_ItemText : MonoBehaviour
{
    private Coroutine _coroutineAnimateText = null;
    private int _multiply = 1;
    private int _waitTime = 1;
    private int _pos = 200;
    private Image _image;

    private void Start()
    {
        _image = gameObject.GetComponentInChildren<Image>();
    }

    public void AnimateText(float animationTime, Sprite sprite)
    {
        if (_coroutineAnimateText == null)
        {
            _coroutineAnimateText = StartCoroutine(CoroutineAnimateText(animationTime * _multiply));
        }
        _image.sprite = sprite;
    }

    private IEnumerator CoroutineAnimateText(float animationTime)
    {
        Vector2 startPos = transform.position;
        Vector2 finalPos = startPos - new Vector2(_pos, 0);
        float time = 0;
        while (time < animationTime)
        {
            transform.position = Vector3.Lerp(startPos, finalPos, (time / animationTime));
            time += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(_waitTime);
        time = 0;
        while (time < animationTime)
        {
            transform.position = Vector3.Lerp(finalPos, startPos, (time / animationTime));
            time += Time.deltaTime;
            yield return null;
        }
        _coroutineAnimateText = null;
    }
}