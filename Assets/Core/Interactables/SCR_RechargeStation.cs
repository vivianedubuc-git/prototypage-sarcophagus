using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Rendering.Universal;

public class SCR_RechargeStation : MonoBehaviour
{
    [SerializeField] private GameObject _retroactionText;
    [SerializeField] private GameObject _interactionText;
    public GameObject interactionText { get { return _interactionText; } }
    private bool stationActivation;
    private Light2D _rechargeStationLight;
    private float rechargeCapacity = 500f;
    Animator animator;
    private string _batteryFullText = "Your battery is full";
    private string _noEnergyLeftText = "Recharge Station has no energy left";
    private TextMeshProUGUI _textRetroaction;
    private int _waitTime = 3;
    [SerializeField] private AudioClip _rechargeSFX;
    [SerializeField] private AudioClip _noEnergySFX;

    void Start(){

        _textRetroaction = _retroactionText.GetComponentInChildren<TextMeshProUGUI>();
        animator = GetComponentInChildren<Animator>();
        _rechargeStationLight = GetComponentInChildren<Light2D>();
        stationActivation = true;
        animator.SetBool("isRunning", true);

    }
    public void Interact(SCR_BatteryManager battery) { 

        float batterySpace = battery._statusValues.maxBattery - battery._statusValues.battery;
        if(stationActivation == true && batterySpace >= rechargeCapacity){
            battery._statusValues.battery += (int)rechargeCapacity;
            battery.batteryBar.fillAmount = (float)battery._statusValues.battery/(float)battery._statusValues.maxBattery;
            rechargeCapacity = 0;
            stationActivation = false;
            animator.SetBool("isRunning", false);
            _rechargeStationLight.intensity = 0;
            Debug.Log("Replenishing battery");
            SCR_SoundManager.instance.PlaySound(_rechargeSFX);
            
        }else if(stationActivation == true && batterySpace < rechargeCapacity && battery._statusValues.battery != battery._statusValues.maxBattery){
           battery._statusValues.battery += (int)batterySpace;
           battery.batteryBar.fillAmount = (float)battery._statusValues.battery/(float)battery._statusValues.maxBattery;
           rechargeCapacity -= (int)batterySpace;

           Debug.Log("Replenishing full battery");
           Debug.Log("il reste " + rechargeCapacity + "pts à la station");
           SCR_SoundManager.instance.PlaySound(_rechargeSFX);

        }else if(stationActivation == false){
           Debug.Log("Recharge Station has no energy left");
           SCR_SoundManager.instance.PlaySound(_noEnergySFX);
           _retroactionText.SetActive(true);
           _textRetroaction.text = _noEnergyLeftText; 
        }
        else{
           Debug.Log("Your battery is full"); 
            _retroactionText.SetActive(true);
           _textRetroaction.text = _batteryFullText; 
        }
        // if(rechargeCapacity <= 0){
        //     stationActivation = false;
        //     animator.SetBool("isRunning", false);
        //     rechargeCapacity = 0;
        // }

        Invoke("DeactivateRetroaction", _waitTime);
    }
    private void DeactivateRetroaction()
    {
        _textRetroaction.text = "";
        _retroactionText.SetActive(false);
    }
}
