using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST };

public class BattleSystem : MonoBehaviour
{

    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    PlayerStats player;
    PlayerStats enemy;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        SetupBattle();
    }

    void SetupBattle()
    {
        Instantiate(playerPrefab);
        Instantiate(enemyPrefab);

        playerHUD.SetHUD(player);
        enemyHUD.SetHUD(enemy);

    }

}
