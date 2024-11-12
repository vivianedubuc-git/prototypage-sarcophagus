using System.Collections;
using UnityEngine;

public class SCR_MC : SCR_Combatant
{
    [SerializeField] private StatusValues _statusValues;
    public StatusValues statusValues { get { return _statusValues; } }
    private bool _isAttacking = false;
    public bool isAttacking { get { return _isAttacking; } }
    private float _speed = 0;
    private float _speedMultiply = 2;
    Animator animator;
    private Rigidbody2D _rb;
    private Vector2 _moveVector;

    private void Start()
    {
        _statusValues.StartGame();
        animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _moveVector = _moveVector.normalized;
        if (_moveVector.magnitude != 0)
        {
            animator.SetFloat("Direction", _moveVector.x);
            animator.SetFloat("Speed", _speed);
        }
        else animator.SetFloat("Speed", 0);

        if(Input.GetButton("Sprint")) _speed = _statusValues.speed * _speedMultiply;
        else _speed = _statusValues.speed;

        if (Input.GetMouseButtonDown(0) && !_isAttacking)
        {
            StartCoroutine(CoroutineAttack());
        }
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _moveVector * _speed * Time.fixedDeltaTime);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        SCR_Enemy SCR_Enemy = other.gameObject.GetComponentInParent<SCR_Enemy>();

        if (SCR_Enemy != null && SCR_Enemy.isAttacking && !isBeingDamaged)
        {
            StartCoroutine(CoroutineDamage(SCR_Enemy.statusValues));
        }
    }

    private IEnumerator CoroutineAttack()
    {
        Debug.Log("MC attacks!");
        _isAttacking = true;
        yield return new WaitForSeconds(_statusValues.ATKSpeed);
        _isAttacking = false;
    }

    private IEnumerator CoroutineDamage(StatusValues enemy)
    {
        isBeingDamaged = true;
        int damage = CalculateDamage(enemy.ATK, _statusValues.DEF);
        int tempHP = _statusValues.HP;
        if(_statusValues.battery <= 0)  _statusValues.HP -= damage;
        else  gameObject.GetComponent<SCR_BatteryManager>().UseBattery(damage);
        Debug.Log("MC has " + _statusValues.battery + " battery left, MC lost " + damage + " HP!");
        if (_statusValues.HP <= 0) animator.SetBool("Dead", true);
        else if (_statusValues.battery < _statusValues.maxBattery) AnimateDamage();
        yield return new WaitForSeconds(_statusValues.invicibility);
        isBeingDamaged = false;
    }
}