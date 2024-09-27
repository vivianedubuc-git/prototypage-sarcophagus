using System.Collections;
using UnityEngine;

public class SCR_Enemy : SCR_Combatant
{
    [SerializeField] private StatusValues _statusValues;
    public StatusValues statusValues { get { return _statusValues; } }
    private bool _isAttacking = false;
    public bool isAttacking { get { return _isAttacking; } }

    private void Start()
    {
        _statusValues.StartGame();
    }

    private void OnTriggerStay2D(Collider2D other)
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

    private IEnumerator CoroutineAttack()
    {
        _isAttacking = true;
        Debug.Log("Enemy attacks!");
        yield return new WaitForSeconds(10 / _statusValues.ATKSpeed);
        _isAttacking = false;
    }

    private IEnumerator CoroutineDamage(StatusValues MC)
    {
        isBeingDamaged = true;
        int damage = CalculateDamage(MC.ATK, _statusValues.DEF);
        int tempHP = _statusValues.HP;
        _statusValues.HP -= damage;
        Debug.Log("Enemy has " + _statusValues.HP + " HP left!");
        if (_statusValues.HP <= 0) Die();
        else if (_statusValues.HP < tempHP) AnimateDamage();
        yield return new WaitForSeconds(10 / MC.ATKSpeed);
        isBeingDamaged = false;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}