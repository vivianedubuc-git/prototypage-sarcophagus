using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Recharge : MonoBehaviour
{
    [SerializeField] private StatusValues _statusValues;
    private bool _canInteract = false;
    private SCR_RechargeStation _RechargeStation;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _canInteract) {
            _RechargeStation.Interact(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        SCR_RechargeStation SCR_RechargeStation = other.gameObject.GetComponent<SCR_RechargeStation>();

        if (SCR_RechargeStation != null) {
            _RechargeStation = SCR_RechargeStation;
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
