using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Tooltip("The name of the character in control.")]
    [SerializeField]
    private string Name;

    [SerializeField]
    private int level;

    [Tooltip("The amount of Health the player has.")]
    public int currentHP;
    public int maxHP;

    [Tooltip("The amount of SP the player has for abilities.")]
    public int currentTempora;
    public int maxTempora;

    public int damage;

    private bool isAlive;
    private bool isDead;

    public bool TakeDamage(int dmg)
    {
        currentHP -= dmg;

        if (currentHP <= 0)
            return true;
        else
            return false;
    }

    public void Heal(int amount)
    {
        currentHP += amount;
        if (currentHP > maxHP)
            currentHP = maxHP;
    }
}
