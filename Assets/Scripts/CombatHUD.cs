using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatHUD : MonoBehaviour
{
    [SerializeField]
    private Text unitHPCurrent;
    [SerializeField]
    private Text unitHPMax;
    [SerializeField]
    private Text unitSPCurrent;
    [SerializeField]
    private Text unitSPMax;


    public void SetHUD(Unit unit)
    {
        unitHPCurrent.text = unit.currentHP.ToString();
        unitHPMax.text = unit.maxHP.ToString();
        unitSPCurrent.text = unit.currentTempora.ToString();
        unitSPMax.text = unit.maxTempora.ToString();
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }
}
