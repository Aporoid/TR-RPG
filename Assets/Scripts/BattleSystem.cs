using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST };

public class BattleSystem : MonoBehaviour
{

    public BattleState state;

    public BattleHUD playerHUD;

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

        playerHUD.SetHUD(player);

    }

}
