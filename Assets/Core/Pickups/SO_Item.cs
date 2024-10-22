using UnityEngine;

public enum ItemType { equipment, valueables }

[CreateAssetMenu(menuName = "Item", fileName = "Item")]
public class SO_Item : ScriptableObject
{
    [SerializeField] private string _itemName;
    public string itemName { get { return _itemName; } }
    [SerializeField] private string _description;
    public string description { get { return _description; } }
    [SerializeField] private ItemType _itemType;
    public ItemType itemType { get { return _itemType; } }
    [SerializeField] private Sprite _sprite;
    public Sprite sprite { get { return _sprite; } }
    [SerializeField] private int _ATKBonus;
    public int ATKBonus { get { return _ATKBonus; } }
    [SerializeField] private float _ATKSpeed;
    public float ATKSpeed { get { return _ATKSpeed; } }
    [SerializeField] private int _batteryConsommation;
    public int batteryConsommation { get { return _batteryConsommation; } }
    [SerializeField] private int _inventorySpace;
    public int inventorySpace { get { return _inventorySpace; } }
}