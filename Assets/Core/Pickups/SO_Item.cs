using UnityEngine;

public enum ItemType { equipment, valueables }

[CreateAssetMenu(menuName = "Item", fileName = "Item")]
public class SO_Item : ScriptableObject
{
    [SerializeField] private string _itemName;
    public string itemName { get { return _itemName; } }
    [SerializeField] private ItemType _itemType;
    public ItemType itemType { get { return _itemType; } }
    [SerializeField] private int _ATKBonus;
    public int ATKBonus { get { return _ATKBonus; } }
    [SerializeField] private int _ATKSpeedBonus;
    public int ATKSpeedBonus { get { return _ATKSpeedBonus; } }
}