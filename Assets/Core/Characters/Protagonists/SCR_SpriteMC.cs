using System.Collections;
using UnityEngine;

public class SCR_SpriteMC : MonoBehaviour
{
    [SerializeField] private AudioClip[] _footStepSFX;
    public void Die()
    {
        SCR_MapManager.instance.ChangeScene("Defeat");
    }
    public void FootStepSFX(){
         int maxNumber = _footStepSFX.Length;
         int randomNumber;
         randomNumber = Random.Range(0,maxNumber);
         SCR_SoundManager.instance.PlaySound(_footStepSFX[randomNumber]);
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