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

    private void OnTriggerEnter2D(Collider2D other)
    {
        SCR_Item SCR_Item = other.gameObject.GetComponent<SCR_Item>();

        if (SCR_Item != null && lItems.Count < _statusValues.inventoryCapacity)
        {
            other.gameObject.SetActive(false);
            lItems.Add(SCR_Item);

            if (SCR_Item.item.itemType == ItemType.equipment)
            {
                _statusValues.ATK += SCR_Item.item.ATKBonus;
                _statusValues.ATKSpeed += SCR_Item.item.ATKSpeedBonus;
            }
        }
    }
}