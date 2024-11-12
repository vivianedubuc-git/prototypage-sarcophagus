using UnityEngine;

[CreateAssetMenu(menuName = "StatusValues", fileName = "StatusValues")]
public class StatusValues : ScriptableObject
{
    [SerializeField][Range(1, 100)] private int _maxHP;
    public int maxHP { get { return _maxHP; } }
    private int _HP;
    public int HP
    {
        get { return _HP; }
        set
        {
            if (value > maxHP) value = maxHP;
            _HP = value;
        }
    }
    [SerializeField][Range(0, 2000)] private int _maxBattery;
    public int maxBattery
    {
        get { return _maxBattery; }
        set { _maxBattery = value; }
    }
    [SerializeField][Range(0, 1000)] private int _initialBattery;
    public int initialBattery
    {
        get { return _initialBattery; }
    }
    [SerializeField]private int _battery;
    public int battery
    {
        get { return _battery; }
        set
        {
            if (value > maxBattery) value = maxBattery;
            if (value < 0) value = 0;
            _battery = value;
        }
    }
    [SerializeField][Range(1, 100)] private int _initialATK;
    public int initialATK { get { return _initialATK; } }
    private int _ATK;
    public int ATK
    {
        get { return _ATK; }
        set { _ATK = value; }
    }
    [SerializeField][Range(1, 100)] private int _DEF;
    public int DEF
    {
        get { return _DEF; }
        set { _DEF = value; }
    }
    [SerializeField][Range(1, 10)] private float _initialSpeed;
    public float initialSpeed { get { return _initialSpeed; } }
    private float _speed;
    public float speed
    {
        get { return _speed; }
        set { _speed = value; }
    }
    [SerializeField][Range(1, 10)] private float _initialATKSpeed;
    public float initialATKSpeed { get { return _initialATKSpeed; } }
    private float _ATKSpeed;
    public float ATKSpeed
    {
        get { return _ATKSpeed; }
        set { _ATKSpeed = value; }
    }
    [SerializeField][Range(0, 10)] private float _invicibility;
    public float invicibility { get { return _invicibility; } }
    [SerializeField][Range(0, 5)] private int _initialInventoryCapacity;
    public int initialInventoryCapacity { get { return _initialInventoryCapacity; } }
    private int _inventoryCapacity;
    public int inventoryCapacity
    {
        get { return _inventoryCapacity; }
        set { _inventoryCapacity = value; }
    }

    public void StartGame()
    {
        _HP = _maxHP;
        _battery = _initialBattery;
        _ATK = _initialATK;
        _speed = _initialSpeed;
        _ATKSpeed = _initialATKSpeed;
        _inventoryCapacity = _initialInventoryCapacity;
    }
}