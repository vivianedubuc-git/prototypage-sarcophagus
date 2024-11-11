using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_BatteryManager : MonoBehaviour
{
    [SerializeField] public StatusValues _statusValues;
    [SerializeField] private Collider2D _lightCollider;
    public Image batteryBar;
    //public float batteryAmount;
    //public float maxBatteryCapacity = 1000f;
    private bool _canInteract = false; 
    private SCR_RechargeStation _rechargeStation;
    // Start is called before the first frame update
    void Start()
    {
        //batteryAmount = _statusValues.battery;
        batteryBar.fillAmount = (float)_statusValues.battery/(float)_statusValues.maxBattery;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _canInteract) {
            _rechargeStation.Interact(this);
            Debug.Log(_statusValues.battery);     
        }
        //UpdateBatteryUI();
       
    }
    public void UseBattery(int batteryUse){
       _statusValues.battery -= batteryUse;
       batteryBar.fillAmount = (float)_statusValues.battery/(float)_statusValues.maxBattery;
    }
    public void UpdateBatteryUI(){
      batteryBar.fillAmount = (float)_statusValues.battery/(float)_statusValues.maxBattery;
    }
    

    private void OnTriggerEnter2D(Collider2D other) {

        SCR_RechargeStation SCR_RechargeStation = other.gameObject.GetComponent<SCR_RechargeStation>();
        if (SCR_RechargeStation != null) {
            Physics2D.IgnoreCollision(_lightCollider, other);
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
