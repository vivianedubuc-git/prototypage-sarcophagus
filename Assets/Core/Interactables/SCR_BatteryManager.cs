using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_BatteryManager : MonoBehaviour
{
    [SerializeField] public StatusValues _statusValues;
    public Image batteryBar;
    public float batteryAmount;
    public float maxBatteryCapacity = 1000f;
    private bool _canInteract = false; 
    private SCR_RechargeStation _rechargeStation;
    // Start is called before the first frame update
    void Start()
    {
        batteryAmount = _statusValues.maxBattery;
        batteryBar.fillAmount = _statusValues.maxBattery/1000f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _canInteract) {
            _rechargeStation.Interact(this);
            Debug.Log(batteryAmount);
            UpdateBatteryStatus(); 
            
        }
        UpdateBatteryUI();
        // if (Input.GetKeyDown(KeyCode.P)) {
        //    UseBattery(250f); 
        // }
    }
    // public void UseBattery(int batteryUse){
    //     _statusValues.maxBattery -= batteryUse;
    //     // batteryBar.fillAmount = _statusValues.maxBattery/1000f;
    // }
    public void UpdateBatteryStatus(){
       _statusValues.maxBattery = (int)batteryAmount;
    }
    public void UpdateBatteryUI(){
      batteryAmount = _statusValues.maxBattery;
      batteryBar.fillAmount = _statusValues.maxBattery/1000f;
    }
    

    private void OnTriggerEnter2D(Collider2D other) {

        SCR_RechargeStation SCR_RechargeStation = other.gameObject.GetComponent<SCR_RechargeStation>();

        if (SCR_RechargeStation != null) {
            _rechargeStation = SCR_RechargeStation;
            _canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        SCR_RechargeStation SCR_RechargeStation = other.gameObject.GetComponent<SCR_RechargeStation>(); ;

        if (SCR_RechargeStation != null) {
            _canInteract = false;
        }
    }
}
