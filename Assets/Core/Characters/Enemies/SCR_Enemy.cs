using System.Collections;
using UnityEngine;

public class SCR_Enemy : SCR_Combatant
{
    [SerializeField] private StatusValues _statusValues;
    public StatusValues statusValues { get { return _statusValues; } }
    private SpriteRenderer _spriteRenderer;
    private Color _initialColor;
    private bool _isAttacking = false;
    public bool isAttacking { get { return _isAttacking; } }

    private void Awake()
    {
        _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
        _initialColor = new Color(_spriteRenderer.color.r, _spriteRenderer.color.b, _spriteRenderer.color.g, 1);
        _spriteRenderer.color = new Color(_initialColor.r, _initialColor.b, _initialColor.g, 0);
    }

    private void Start()
    {
        _statusValues.StartGame();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        SCR_Light SCR_Light = other.gameObject.GetComponent<SCR_Light>();

        if (SCR_Light != null) _spriteRenderer.color = _initialColor;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        SCR_Light SCR_Light = other.gameObject.GetComponent<SCR_Light>();

        if (SCR_Light != null) _spriteRenderer.color = new Color(_initialColor.r, _initialColor.b, _initialColor.g, 0);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        SCR_Light SCR_Light = other.gameObject.GetComponent<SCR_Light>();

        if (SCR_Light == null)
        {
            SCR_MC SCR_MC = other.gameObject.GetComponentInParent<SCR_MC>();
            
            if (SCR_MC != null && SCR_MC.isAttacking && !isBeingDamaged)
            {
                StartCoroutine(CoroutineDamage(SCR_MC.statusValues));
            }
            if (SCR_MC != null && !isAttacking)
            {
                StartCoroutine(CoroutineAttack());
            }
        }
    }

    private IEnumerator CoroutineAttack()
    {
        Debug.Log("Enemy attacks!");
        _isAttacking = true;
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
        yield return new WaitForSeconds(MC.ATKSpeed);
        isBeingDamaged = false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}