using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SCR_Enemy : SCR_Combatant
{
    [SerializeField] private StatusValues _statusValues;
    public StatusValues statusValues { get { return _statusValues; } }
    [SerializeField] private AudioClip _soundAttack;
    [SerializeField] private AudioClip _soundDamage;
    private Transform _target;
    private NavMeshAgent _agent;
    private Animator _animator;
    SpriteRenderer _SpriteRenderer;
    private bool _isAttacking = false;
    public bool isAttacking { get { return _isAttacking; } }

    private void Start()
    {
        _SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _statusValues.StartGame();
        
    }

    private void Update()
    {
        if (_target != null)
        {
            _agent.SetDestination(_target.position);
            _animator.SetFloat("Speed", 1);
            if (_target.position.x < transform.position.x) _SpriteRenderer.flipX = true;
            else _SpriteRenderer.flipX = false;
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        SCR_SpriteMC SCR_SpriteMC = other.gameObject.GetComponent<SCR_SpriteMC>();
        if(SCR_SpriteMC != null) _target = SCR_SpriteMC.gameObject.GetComponent<Transform>();

        SCR_Pause SCR_Pause = other.gameObject.GetComponentInChildren<SCR_Pause>();
        if (SCR_Pause != null)
        {
            if (!SCR_Pause.isPaused)
            {
                SCR_MC SCR_MC = other.gameObject.GetComponentInParent<SCR_MC>();

                if (SCR_MC != null && SCR_MC.isAttacking && !isBeingDamaged)
                {
                    StartCoroutine(CoroutineDamage(SCR_MC.statusValues));
                    _target = null;
                }
                if (SCR_MC != null && !isAttacking)
                {
                    StartCoroutine(CoroutineAttack());
                }
            }
        }
    }

    private IEnumerator CoroutineAttack()
    {
        Debug.Log("Enemy attacks!");
        _isAttacking = true;
        // SCR_SoundManager.instance.PlaySound(_soundAttack);
        yield return new WaitForSeconds(_statusValues.ATKSpeed);
        _isAttacking = false;
    }

    private IEnumerator CoroutineDamage(StatusValues MC)
    {
        isBeingDamaged = true;
        int damage = CalculateDamage(MC.ATK, _statusValues.DEF);
        int tempHP = _statusValues.HP;
        _statusValues.HP -= damage;
        Debug.Log("Enemy has " + _statusValues.HP + " HP left, enemy lost " + damage + " HP!");
        if (_statusValues.HP <= 0) Die();
        else if (_statusValues.HP < tempHP) AnimateDamage();
        // SCR_SoundManager.instance.PlaySound(_soundDamage);
        yield return new WaitForSeconds(MC.ATKSpeed);
        isBeingDamaged = false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}