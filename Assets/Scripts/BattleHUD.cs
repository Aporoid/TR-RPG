using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
    [SerializeField]
    private Slider playerHPSlider;
    [SerializeField]
    private Slider playerSPSlider;

    public void SetHUD(PlayerStats player)
    {
        playerHPSlider.maxValue = player.maxHP;
        playerHPSlider.value = player.currentHP;
        playerSPSlider.maxValue = player.maxTempora;
        playerSPSlider.value = player.currentTempora;
    }

    public void SetHP(int hp)
    {
        playerHPSlider.value = hp;
    }
}
