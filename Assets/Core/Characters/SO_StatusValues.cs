using UnityEngine;

[CreateAssetMenu(menuName = "StatusValues", fileName = "StatusValues")]
public class StatusValues : ScriptableObject
{
    [SerializeField][Range(10, 50)] private int _maxHP;
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
    [SerializeField][Range(100, 500)] private int _maxBattery;
    public int maxBattery
    {
        get { return _maxBattery; }
        set { _maxBattery = value; }
    }
    private int _battery;
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
    [SerializeField][Range(10, 100)] private int _initialATK;
    public int initialATK { get { return _initialATK; } }
    private int _ATK;
    public int ATK
    {
        get { return _ATK; }
        set { _ATK = value; }
    }
    [SerializeField][Range(15, 50)] private int _DEF;
    public int DEF
    {
        get { return _DEF; }
        set { _DEF = value; }
    }
    [SerializeField][Range(5, 10)] private int _initialATKSpeed;
    public int initialATKSpeed { get { return _initialATKSpeed; } }
    private int _ATKSpeed;
    public int ATKSpeed
    {
        get { return _ATKSpeed; }
        set { _ATKSpeed = value; }
    }
    [SerializeField][Range(5, 10)] private int _inventoryCapacity;
    public int inventoryCapacity
    {
        get { return _inventoryCapacity; }
        set { _inventoryCapacity = value; }
    }

    public void StartGame()
    {
        HP = maxHP;
        battery = maxBattery;
        ATK = initialATK;
        ATKSpeed = initialATKSpeed;
    }
}