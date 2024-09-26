using System;
using UnityEngine;

public class SCR_Combatant : MonoBehaviour
{
    protected bool isBeingDamaged = false;

    public int CalculateDamage(int ATK, int DEF)
    {
        float damage = ATK / ((50 + DEF) / 50);
        return (int)Math.Ceiling(damage);
    }

    public void AnimateDamage()
    {
        Debug.Log("A combatant got damaged!");
    }

    public void Die()
    {
        Debug.Log("A combatant died!");
    }
}