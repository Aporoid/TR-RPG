using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script will create individual instances of enemy encounters, which will move onto the combat script.
/// This will be attached to every individual roaming enemy on the overworld, including bosses.
/// </summary>
public class OverworldEnemySpawner : MonoBehaviour
{
    [Tooltip("The name of the enemy in question.")]
    [SerializeField]
    private string enemyName;

    [Tooltip("The level the enemy will be created at.")]
    [SerializeField]
    private int enemyLevel;

    [Tooltip("How much HP each enemy will have.")]
    [SerializeField]
    private int enemyHP;

    [Tooltip("The corresponding image of the monster.")]
    [SerializeField]
    private Sprite enemyImage;

    [Tooltip("How many monsters will be in battle at once.")]
    [SerializeField]
    private int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
