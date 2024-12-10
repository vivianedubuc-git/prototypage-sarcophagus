using System.Collections;
using UnityEngine;

public class SCR_MC : SCR_Combatant
{
    [SerializeField] private StatusValues _statusValues;
    public StatusValues statusValues { get { return _statusValues; } }
    //[SerializeField] private AudioClip[] _soundAttack;
    [SerializeField] private AudioClip _soundAttackWhiff;
    [SerializeField] private AudioClip[] _soundDamage;
    [SerializeField] private AudioClip _soundDeath;
    private SCR_Pause _pause;
    private bool _isAttacking = false;
    private bool _isInAttackRange = false;
    public bool isSprinting = false;
    public bool isAttacking { get { return _isAttacking; } }
    private float _speed = 0;
    private float _speedMultiply = 2;
    Animator animator;
    SpriteRenderer _SpriteRenderer;
    private Rigidbody2D _rb;
    private Vector2 _moveVector;

    private void Start()
    {
        _statusValues.StartGame();
        animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _pause = GetComponentInChildren<SCR_Pause>();
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }
    
    private void Update()
    {
        _moveVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        _moveVector = _moveVector.normalized;
        if (_moveVector.magnitude != 0 && !_pause.isPaused)
        {
            if(_moveVector.x < 0){
                _SpriteRenderer.flipX = true;
            
            }else if(_moveVector.x > 0){  
                 _SpriteRenderer.flipX = false;
            }
            animator.SetFloat("Speed", _speed);
        }
        else animator.SetFloat("Speed", 0);

        if(Input.GetButton("Sprint") && !_pause.isPaused){
            if(_statusValues.battery > 0){
                _speed = _statusValues.speed * _speedMultiply;
                if(_moveVector.magnitude != 0){
                    isSprinting = true;
                    animator.SetBool("IsSprinting", true);
                }
                else{
                    isSprinting = false; 
                    animator.SetBool("IsSprinting", false);  
                }
            }
            else{
                _speed = _statusValues.speed;
                isSprinting = false; 
                animator.SetBool("IsSprinting", false);
            }  
        } 
        else{
            _speed = _statusValues.speed;
            isSprinting = false; 
            animator.SetBool("IsSprinting", false);
        } 

        if (Input.GetMouseButtonDown(0) && !_isAttacking && !_pause.isPaused)
        {
            StartCoroutine(CoroutineAttack());
        }
    }

    private void FixedUpdate()
    {
        if (!_pause.isPaused) _rb.MovePosition(_rb.position + _moveVector * _speed * Time.fixedDeltaTime);
    }

    void OnTriggerStay2D(Collider2D other)
    {
        SCR_Enemy SCR_Enemy = other.gameObject.GetComponentInParent<SCR_Enemy>();

        if(SCR_Enemy != null){
            _isInAttackRange = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        SCR_Enemy SCR_Enemy = other.gameObject.GetComponentInParent<SCR_Enemy>();
        
        if(SCR_Enemy != null){
            _isInAttackRange = false;
        }
    }

    private IEnumerator CoroutineAttack()
    {
        Debug.Log("MC attacks!");
        _isAttacking = true;
        animator.SetTrigger("Attack");
        if(_isInAttackRange == false) SCR_SoundManager.instance.PlaySound(_soundAttackWhiff);
        yield return new WaitForSeconds(_statusValues.ATKSpeed);
        _isAttacking = false;
    }

    public void GetDamaged(StatusValues enemy)
    {
        StartCoroutine(CoroutineDamage(enemy));
    }

    private IEnumerator CoroutineDamage(StatusValues enemy)
    {
        isBeingDamaged = true;
        animator.SetTrigger("Hit");
      
        int damage = CalculateDamage(enemy.ATK, _statusValues.DEF);
        int tempHP = _statusValues.HP;
        if(_statusValues.battery <= 0)  _statusValues.HP -= damage;
        else  gameObject.GetComponent<SCR_BatteryManager>().UseBattery(damage);
        Debug.Log("MC has " + _statusValues.battery + " battery left, MC lost " + damage + " HP!");
        if (_statusValues.HP <= 0){
            animator.SetBool("Dead", true);
             SCR_SoundManager.instance.PlaySound(_soundDeath);
        } 
        else if (_statusValues.battery < _statusValues.maxBattery) AnimateDamage();
        HurtSFX();
        yield return new WaitForSeconds(_statusValues.invicibility);
        isBeingDamaged = false;
    }

    private void HurtSFX(){
        int maxNumber = _soundDamage.Length;
        int randomNumber;
        randomNumber = Random.Range(0,maxNumber);
        SCR_SoundManager.instance.PlaySound(_soundDamage[randomNumber]);
    }
}