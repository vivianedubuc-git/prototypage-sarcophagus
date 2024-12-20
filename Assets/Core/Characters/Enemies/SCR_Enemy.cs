using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class SCR_Enemy : SCR_Combatant
{
    [SerializeField] private StatusValues _statusValues;
    public StatusValues statusValues { get { return _statusValues; } }
    [SerializeField] private AudioClip[] _soundAttack;
    [SerializeField] private AudioClip _soundDamage;
    [SerializeField] private GameObject _lifeBar;
    [SerializeField] private Image _lifeFill;
    private int _HP = 0;
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
        _HP = _statusValues.HP;
        _lifeFill.fillAmount = (float)_statusValues.HP/(float)_statusValues.maxHP;
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

        _lifeBar.SetActive(isBeingDamaged);
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
                    SCR_MC.GetDamaged(_statusValues);
                    StartCoroutine(CoroutineAttack());
                }
            }
        }
    }

    private IEnumerator CoroutineAttack()
    {
        Debug.Log("Enemy attacks!");
        _isAttacking = true;
        _animator.SetTrigger("Attack");
        // SCR_SoundManager.instance.PlaySound(_soundAttack);
        yield return new WaitForSeconds(_statusValues.ATKSpeed);
        _isAttacking = false;
    }

    private IEnumerator CoroutineDamage(StatusValues MC)
    {
        isBeingDamaged = true;
        _animator.SetTrigger("Hit");
        PunchSFX();
        int damage = CalculateDamage(MC.ATK, _statusValues.DEF);
        int tempHP = _HP;
        _HP -= damage;
        Debug.Log("Enemy has " + _HP + " HP left, enemy lost " + damage + " HP!");
        _lifeFill.fillAmount = (float)_HP/(float)_statusValues.maxHP;
        if (_HP <= 0) Die();
        else if (_HP < tempHP) AnimateDamage();
        // SCR_SoundManager.instance.PlaySound(_soundDamage);
        yield return new WaitForSeconds(MC.ATKSpeed);
        isBeingDamaged = false;
    }
    private void PunchSFX(){
        int maxNumber = _soundAttack.Length;
        int randomNumber;
        randomNumber = Random.Range(0,maxNumber);
        SCR_SoundManager.instance.PlaySound(_soundAttack[randomNumber]);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}