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

    public Slider hpSlider;
    public Slider spSlider;


    public void SetHUD(Unit unit)
    {
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        spSlider.maxValue = unit.maxTempora;
        spSlider.value = unit.currentTempora;

        unitHPCurrent.text = unit.currentHP.ToString();
        unitHPMax.text = unit.maxHP.ToString();
        unitSPCurrent.text = unit.currentTempora.ToString();
        unitSPMax.text = unit.maxTempora.ToString();
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
    }

    public void SetSP(int sp)
    {
        spSlider.value = sp;
    }
}
