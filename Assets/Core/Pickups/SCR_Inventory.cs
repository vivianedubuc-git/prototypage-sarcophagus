using System.Collections.Generic;
using UnityEngine;

public class SCR_Inventory : MonoBehaviour
{
    [SerializeField] private StatusValues _statusValues;
    [SerializeField] private List<SCR_Item> _lItems;
    public List<SCR_Item> lItems
    {
        get { return _lItems; }
        set { _lItems = value; }
    }
    [SerializeField] private SO_Item _equipment;
    private bool _canInteract = false;
    private SCR_NPC _NPC;

    private void Start()
    {
        AddEquipment(_equipment);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && _canInteract)
        {
            _NPC.Interact(this);
        }
    }

    private void AddEquipment(SO_Item equipment)
    {
        if (equipment.itemType == ItemType.equipment)
        {
            Debug.Log("Equipment added to inventory!");
            _equipment = equipment;
            _statusValues.ATK = _statusValues.initialATK;
            _statusValues.ATKSpeed = _statusValues.initialATKSpeed;
            _statusValues.ATK += equipment.ATKBonus;
            _statusValues.ATKSpeed += equipment.ATKSpeedBonus;
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = equipment.sprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        SCR_Item SCR_Item = other.gameObject.GetComponent<SCR_Item>();
        
        if (SCR_Item != null && lItems.Count < _statusValues.inventoryCapacity)
        {
            other.gameObject.GetComponent<SCR_Item>().AnimatePickUp();
            lItems.Add(SCR_Item);
            AddEquipment(SCR_Item.item);
        }

        SCR_NPC SCR_NPC = other.gameObject.GetComponent<SCR_NPC>();

        if (SCR_NPC != null)
        {
            _NPC = SCR_NPC;
            _canInteract = true;
            _NPC.interactionText.SetActive(_canInteract); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        SCR_NPC SCR_NPC = other.gameObject.GetComponent<SCR_NPC>();

        if (SCR_NPC != null)
        {
            _canInteract = false;
            _NPC.interactionText.SetActive(_canInteract); 
        }
    }
}