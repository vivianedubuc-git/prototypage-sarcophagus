using System.Collections;
using UnityEngine;

public class SCR_Item : MonoBehaviour
{
    [SerializeField] private SO_Item _item;
    public SO_Item item { get { return _item; } }
    [SerializeField] private SCR_ItemText _itemText;
    public SCR_ItemText itemText { get { return _itemText; } set { _itemText = value; } }
    private Vector2 _finalPos = Vector2.up;
    private float _animationTime = 1f;

    public void AnimatePickUp()
    {
        StartCoroutine(CoroutineAnimatePickUp());
        itemText.AnimateText(_animationTime, item.sprite);
    }

    private IEnumerator CoroutineAnimatePickUp()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        Vector2 startPos = transform.position;
        Vector2 finalPos = startPos + _finalPos;
        SpriteRenderer sprite = gameObject.GetComponent<SpriteRenderer>();
        Color startColor = sprite.color;
        Color finalColor = new Color(startColor.r, startColor.g, startColor.b, 0);
        float time = 0;
        while (time < _animationTime)
        {
            transform.position = Vector3.Lerp(startPos, finalPos, (time / _animationTime));
            sprite.color = Color.Lerp(startColor, finalColor, time / _animationTime);
            time += Time.deltaTime;
            yield return null;
        }
        gameObject.SetActive(false);
    }
}