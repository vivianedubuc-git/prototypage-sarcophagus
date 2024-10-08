using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_MC : SCR_Combatant
{
    [SerializeField] private StatusValues _statusValues;
    public StatusValues statusValues { get { return _statusValues; } }
    private bool _isAttacking = false;
    public bool isAttacking { get { return _isAttacking; } }

    private void Start()
    {
        _statusValues.StartGame();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isAttacking)
        {
            StartCoroutine(CoroutineAttack());
        }
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
        yield return new WaitForSeconds(10 / _statusValues.ATKSpeed);
        _isAttacking = false;
    }

    private IEnumerator CoroutineDamage(StatusValues enemy)
    {
        isBeingDamaged = true;
        int damage = CalculateDamage(enemy.ATK, _statusValues.DEF);
        int tempHP = _statusValues.HP;
        _statusValues.HP -= damage;
        Debug.Log("MC has " + _statusValues.HP + " HP left, MC lost " + damage + " HP!");
        if (_statusValues.HP <= 0) Die();
        else if (_statusValues.HP < tempHP) AnimateDamage();
        yield return new WaitForSeconds(10 / enemy.ATKSpeed);
        isBeingDamaged = false;
    }

    private void Die()
    {
        SCR_MapManager.instance.ChangeScene("Defeat");
    }
}