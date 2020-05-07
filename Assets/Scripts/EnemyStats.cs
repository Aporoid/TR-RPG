﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Tooltip("The name of the character in control.")]
    [SerializeField]
    private string enemyName;

    [Tooltip("The amount of Health the player has.")]
    public int currentHP;
    public int maxHP;

    [Tooltip("The amount of SP the player has for abilities.")]
    public int currentTempora;
    public int maxTempora;

    private bool isAlive;
    private bool isDead;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
