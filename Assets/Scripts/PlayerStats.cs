using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Tooltip("The name of the character in control.")]
    [SerializeField]
    private string playerName;

    //[SerializeField]
    //public int playerHP;

    //[SerializeField]
    //public int playerTempora;

    [Tooltip("The amount of Health the player has.")]
    public int currentHP;
    public int maxHP;

    [Tooltip("The amount of SP the player has for abilities.")]
    public int currentTempora;
    public int maxTempora;

    [Tooltip("The amount of ammo the player has for their guns.")]
    [SerializeField]
    public int ammoCounter;

    private bool isAlive;
    private bool isDead;

    [Tooltip("How many members are in the player's party.")]
    public int partyCount;

    void Start()
    {
        partyCount = 3;
    }

    // Update is called once per frame
    void Update()
    {
        CheckMortality();
    }

    void CheckMortality()
    {
        if(currentHP > 0)
        {
            isAlive = true;
            isDead = false;
        }
        else if (currentHP < 0)
        {
            isAlive = false;
            isDead = true;
        }
    }

}
