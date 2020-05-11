using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatHUD : MonoBehaviour
{
    [SerializeField]
    private Text unitHPCurrent;
    [SerializeField]
    private Text unitSPCurrent;

    public Slider hpSlider;
    public Slider spSlider;


    public void SetHUD(Unit unit)
    {
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
        spSlider.maxValue = unit.maxTempora;
        spSlider.value = unit.currentTempora;

        unitHPCurrent.text = unit.currentHP.ToString();
        unitSPCurrent.text = unit.currentTempora.ToString();
    }

    public void SetHP(int hp)
    {
        hpSlider.value = hp;
		unitHPCurrent.text = hp.ToString();
	}

    public void SetSP(int sp)
    {
        spSlider.value = sp;
		unitSPCurrent.text = sp.ToString();
    }
}
