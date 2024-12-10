using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_SpriteEnemy : MonoBehaviour
{
         [SerializeField] private AudioClip[] _footStepSFX;
         private Color _color;

         private void Start()
         {
            _color = GetComponent<SpriteRenderer>().color;
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
        _SpriteRenderer.color = _color;
        yield return new WaitForSeconds(0.1f);
        _SpriteRenderer.color = new Color(255,0,0);
        yield return new WaitForSeconds(0.1f);
        _SpriteRenderer.color = _color;
    }
}
