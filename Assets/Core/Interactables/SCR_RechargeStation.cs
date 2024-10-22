using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_RechargeStation : MonoBehaviour
{
    private bool stationActivation;
    private float rechargeValue = 500f;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        stationActivation = true;
        animator.SetBool("isRunning", true);


    }

    public void Interact(SCR_BatteryManager battery) { 
        if(stationActivation == true && battery.batteryAmount < battery.maxBatteryCapacity){
            battery.batteryAmount += rechargeValue;
            battery.batteryBar.fillAmount = battery.batteryAmount/1000f;
            Debug.Log("Replenishing battery...");
            animator.SetBool("isRunning", false);
            stationActivation = false;
        }else if(stationActivation ==false){
           Debug.Log("Recharge Station has no energy"); 
        }else{
           Debug.Log("Your battery is full");  
        }
        if(battery.batteryAmount > battery.maxBatteryCapacity){
            battery.batteryAmount = battery.maxBatteryCapacity;
        }
    }
}
