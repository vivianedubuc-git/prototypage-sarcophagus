using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_SpriteEnemy : MonoBehaviour
{
         [SerializeField] private AudioClip[] _footStepSFX;
          public void FootStepSFX(){
         int maxNumber = _footStepSFX.Length;
         int randomNumber;
         randomNumber = Random.Range(0,maxNumber);
         SCR_SoundManager.instance.PlaySound(_footStepSFX[randomNumber]);
    }
}
