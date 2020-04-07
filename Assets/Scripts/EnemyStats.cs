using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Tooltip("The amount of Health the enemy has.")]
    [SerializeField]
    public int enemyHP;

    [Tooltip("The amount of SP the enemy has for abilities.")]
    [SerializeField]
    public int enemyTempora;

    private int minHP = 1;
    private int maxHP = 999;

    private int minTempora = 1;
    private int maxTempora = 999;

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
