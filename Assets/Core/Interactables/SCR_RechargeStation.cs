using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SCR_RechargeStation : MonoBehaviour
{
    [SerializeField] private GameObject _interactionText;
    public GameObject interactionText { get { return _interactionText; } }
    private bool stationActivation;
    private float rechargeCapacity = 500f;
    Animator animator;
    [SerializeField] private AudioClip _rechargeSFX;
    [SerializeField] private AudioClip _noEnergySFX;

    void Start(){

        animator = GetComponentInChildren<Animator>();
        stationActivation = true;
        animator.SetBool("isRunning", true);

    }
    public void Interact(SCR_BatteryManager battery) { 

        float batterySpace = battery._statusValues.maxBattery - battery._statusValues.battery;
        if(stationActivation == true && batterySpace >= rechargeCapacity){
            battery._statusValues.battery += (int)rechargeCapacity;
            battery.batteryBar.fillAmount = (float)battery._statusValues.battery/(float)battery._statusValues.maxBattery;
            battery.gameObject.GetComponentInChildren<Light2D>().intensity = battery.batteryBar.fillAmount;
            rechargeCapacity = 0;
            stationActivation = false;
            animator.SetBool("isRunning", false);
            gameObject.GetComponentInChildren<Light2D>().intensity = 0f;
            Debug.Log("Replenishing battery");
            SCR_SoundManager.instance.PlaySound(_rechargeSFX);
            
        }else if(stationActivation == true && batterySpace < rechargeCapacity && battery._statusValues.battery != battery._statusValues.maxBattery){
           battery._statusValues.battery += (int)batterySpace;
           battery.batteryBar.fillAmount = (float)battery._statusValues.battery/(float)battery._statusValues.maxBattery;
           battery.gameObject.GetComponentInChildren<Light2D>().intensity = battery.batteryBar.fillAmount;
           rechargeCapacity -= (int)batterySpace;

           Debug.Log("Replenishing full battery");
           Debug.Log("il reste " + rechargeCapacity + "pts à la station");
           SCR_SoundManager.instance.PlaySound(_rechargeSFX);

        }else if(stationActivation == false){
           Debug.Log("Recharge Station has no energy left");
           SCR_SoundManager.instance.PlaySound(_noEnergySFX); 
        }
        else{
           Debug.Log("Your battery is full");  
        }
        // if(rechargeCapacity <= 0){
        //     stationActivation = false;
        //     animator.SetBool("isRunning", false);
        //     rechargeCapacity = 0;
        // }
    }
}
