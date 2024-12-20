using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Door : MonoBehaviour
{
    [SerializeField] private GameObject _interactionText;
    public GameObject interactionText { get { return _interactionText; } }
    public float energyCost;
    [SerializeField] private AudioClip _openDoor;
    [SerializeField] private AudioClip _noEnergy;

   
    
    public void Interact(SCR_BatteryManager battery) { 

        float batteryLevel = battery._statusValues.battery;
        if(batteryLevel >= energyCost){
            battery.UseBattery((int)energyCost);
            Debug.Log("Door Open");
            this.gameObject.SetActive(false);
            SCR_SoundManager.instance.PlaySound(_openDoor);
        }
        else{
            Debug.Log("Not enough energy to open the door"); 
            SCR_SoundManager.instance.PlaySound(_noEnergy);
        }
    }
}
