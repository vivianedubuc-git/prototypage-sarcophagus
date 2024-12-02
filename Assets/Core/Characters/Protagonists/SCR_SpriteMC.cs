using System.Collections;
using UnityEngine;

public class SCR_SpriteMC : MonoBehaviour
{

    public void Die()
    {
        SCR_MapManager.instance.ChangeScene("Defeat");
    }
    public void Hit(){
         StartCoroutine(CoroutineHitColor());
    }
     private IEnumerator CoroutineHitColor()
    {
        SpriteRenderer _SpriteRenderer = GetComponent<SpriteRenderer>();
        _SpriteRenderer.color = new Color(255,0,0);
        yield return new WaitForSeconds(0.1f);
        _SpriteRenderer.color = new Color(255,255,255);
        yield return new WaitForSeconds(0.1f);
        _SpriteRenderer.color = new Color(255,0,0);
        yield return new WaitForSeconds(0.1f);
        _SpriteRenderer.color = new Color(255,255,255);
    }
}