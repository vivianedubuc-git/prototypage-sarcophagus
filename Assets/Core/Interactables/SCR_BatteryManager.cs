using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SCR_BatteryManager : MonoBehaviour
{
    [SerializeField] public StatusValues _statusValues;
    private Coroutine _coroutineSprint = null;
    public Image batteryBar;
    private bool _canInteract = false; 
    private SCR_RechargeStation _rechargeStation;
    private SCR_Door _door;
    public int sprintCost;
    [SerializeField] private SCR_MC _mc;
    // Start is called before the first frame update
    void Start()
    {
        //SCR_MC _mc = gameObject.GetComponent<SCR_MC>();
        Invoke("UpdateBatteryUI", 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _canInteract) {
            if (_rechargeStation != null) 
            { 
                 _rechargeStation.Interact(this); 
            } 
            if (_door != null) 
            { 
                 _door.Interact(this); 
            }      
        }
        if(_mc.isSprinting){
            if (_coroutineSprint == null)
            {
                _coroutineSprint = StartCoroutine(CoroutineSprint());
            }
        }else{
            StopCoroutine(CoroutineSprint());
        }
       
    }
    public void UseBattery(int batteryUse){
       _statusValues.battery -= batteryUse;
       batteryBar.fillAmount = (float)_statusValues.battery/(float)_statusValues.maxBattery;
    }
    public void UpdateBatteryUI(){
        Debug.Log("UPDATE");
      batteryBar.fillAmount = (float)_statusValues.battery/(float)_statusValues.maxBattery;
    }
    

    private void OnTriggerEnter2D(Collider2D other) {

        SCR_RechargeStation SCR_RechargeStation = other.gameObject.GetComponent<SCR_RechargeStation>();
        if (SCR_RechargeStation != null) {
            _rechargeStation = SCR_RechargeStation;
            _canInteract = true;
            SCR_RechargeStation.interactionText.SetActive(_canInteract);
        }
        
        SCR_Door SCR_Door = other.gameObject.GetComponent<SCR_Door>();
        if (SCR_Door != null) {
            _door = SCR_Door;
            _canInteract = true;
            SCR_Door.interactionText.SetActive(_canInteract);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        SCR_RechargeStation SCR_RechargeStation = other.gameObject.GetComponent<SCR_RechargeStation>();

        if (SCR_RechargeStation != null) {
            _canInteract = false;
            SCR_RechargeStation.interactionText.SetActive(_canInteract);
            _rechargeStation = null;
        }
        SCR_Door SCR_Door = other.gameObject.GetComponent<SCR_Door>();

        if (SCR_Door != null) {
            _canInteract = false;
            SCR_Door.interactionText.SetActive(_canInteract);
            _door = null;
        }
    }

    private IEnumerator CoroutineSprint()
    {
        while (_mc.isSprinting)
        {
            UseBattery(sprintCost);
            yield return new WaitForSeconds(1);  
        }
        _coroutineSprint = null;
    }
}
