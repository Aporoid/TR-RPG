using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Tooltip("The name of the character in control.")]
    [SerializeField]
    private string playerName;

    [Tooltip("The amount of Health the player has.")]
    [SerializeField]
    public int playerHP;

    [Tooltip("The amount of SP the player has for abilities.")]
    [SerializeField]
    public int playerTempora;

    [Tooltip("The amount of ammo the player has for their guns.")]
    [SerializeField]
    public int ammoCounter;

    private int minHP = 1;
    private int maxHP = 999;

    private int minTempora = 1;
    private int maxTempora = 999;

    private bool isAlive;
    private bool isDead;

    // Update is called once per frame
    void Update()
    {
        CheckMortality();
    }

    void CheckMortality()
    {
        if(playerHP > minHP)
        {
            isAlive = true;
            isDead = false;
        }
        else if (playerHP < minHP)
        {
            isAlive = false;
            isDead = true;
        }
    }

}
