using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SCR_Light : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private SCR_Pause _pause;
    private Light2D _light;
    private Image _battery;
    private StatusValues _statusValues;
    private Vector3 _mousePos;
    private Vector3 _objectPos;
    private float _angle;
    private int _distanceMin = 20;

    private void Start()
    {
        _statusValues = GetComponentInParent<SCR_MC>().statusValues;
        _light = GetComponent<Light2D>();
        _battery = GetComponentInParent<SCR_BatteryManager>().batteryBar;
    }

    void Update()
    {
        _light.intensity = _battery.fillAmount;
        
        if (_statusValues.battery > 0 && !_pause.isPaused)
        {
            _light.enabled = true;
            _mousePos = Input.mousePosition;
            _objectPos = Camera.main.WorldToScreenPoint(_target.position);
            _mousePos.x = _mousePos.x - _objectPos.x;
            _mousePos.y = _mousePos.y - _objectPos.y;
            if (_mousePos.x < -_distanceMin || _mousePos.x > _distanceMin) {
                if (_mousePos.y < -_distanceMin || _mousePos.y > _distanceMin) {
                    _angle = Mathf.Atan2(_mousePos.y, _mousePos.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, 0, _angle - 90);
                }
            }
        }
        else
        {
            _light.enabled = false;
        }
    }
}