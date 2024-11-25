using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_BatteryManager : MonoBehaviour
{
    [SerializeField] public StatusValues _statusValues;
    [SerializeField] private Collider2D _lightCollider;
    private Coroutine _coroutineSprint = null;
    public Image batteryBar;
    private bool _canInteract = false; 
    private SCR_RechargeStation _rechargeStation;
    private SCR_Door _door;
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
            _rechargeStation.Interact(this);
            _door.Interact(this);
            Debug.Log(_statusValues.battery);     
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
            Physics2D.IgnoreCollision(_lightCollider, other);
            _rechargeStation = SCR_RechargeStation;
            _canInteract = true;
        }
        
        SCR_Door SCR_Door = other.gameObject.GetComponent<SCR_Door>();
        if (SCR_Door != null) {
            Physics2D.IgnoreCollision(_lightCollider, other);
            _door = SCR_Door;
            _canInteract = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        SCR_Door SCR_Door = other.gameObject.GetComponent<SCR_Door>();

        if (SCR_Door != null) {
            _canInteract = false;
        }
    }

    private IEnumerator CoroutineSprint()
    {
        while (_mc.isSprinting)
        {
            UseBattery(50);
            yield return new WaitForSeconds(1);  
        }
        _coroutineSprint = null;
    }
}
