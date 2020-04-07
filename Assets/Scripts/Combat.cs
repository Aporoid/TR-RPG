using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    PlayerStats player = new PlayerStats();
    EnemyStats enemy = new EnemyStats();



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerMelee()
    {
        enemy.enemyHP -= 30;
    }
}
