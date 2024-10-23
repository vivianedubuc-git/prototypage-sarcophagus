using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_RechargeStation : MonoBehaviour
{
    private bool stationActivation;
    private float rechargeCapacity = 500f;
    Animator animator;

    void Start(){

        animator = GetComponentInChildren<Animator>();
        stationActivation = true;
        animator.SetBool("isRunning", true);

    }
    public void Interact(SCR_BatteryManager battery) { 
         float batterySpace = battery.maxBatteryCapacity - battery.batteryAmount;
        if(stationActivation == true && batterySpace >= rechargeCapacity){
            battery.batteryAmount += rechargeCapacity;
            battery.batteryBar.fillAmount = battery.batteryAmount/1000f;
            rechargeCapacity = 0;
            stationActivation = false;
            animator.SetBool("isRunning", false);
            Debug.Log("Replenishing battery");
            Debug.Log(battery.batteryAmount);
            
        }else if(stationActivation == true && batterySpace < rechargeCapacity && battery.batteryAmount != battery.maxBatteryCapacity){
           battery.batteryAmount += batterySpace;
           battery.batteryBar.fillAmount = battery.batteryAmount/1000f;
           rechargeCapacity -= batterySpace;
           Debug.Log("Replenishing full battery");
           Debug.Log(battery.batteryAmount);
           Debug.Log("il reste " + rechargeCapacity + "pts à la station");

        }else if(stationActivation == false){
           Debug.Log("Recharge Station has no energy left"); 
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
