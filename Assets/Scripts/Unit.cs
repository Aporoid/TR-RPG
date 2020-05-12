using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [Tooltip("The name of the character in control.")]
    public string name;

    public int experienceGiven;

    [Tooltip("The amount of Health the player has.")]
    public int currentHP;
    public int maxHP;

    [Tooltip("The amount of SP the player has for abilities.")]
    public int currentTempora;
    public int maxTempora;

	public int damage;
	public int defense;
	public int concentration;
	public int resistance;
	public int gunDamage;
	public int ammo;

	//public GameObject[] TemporaChoices = new GameObject[6];

	[Tooltip("How to keep track of the number of party members")]
	public bool isAnAlly1;
	public bool isAnAlly2;

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
