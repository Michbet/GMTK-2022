using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Text nameText;
    public Text LevelText;
    public Slider hpSlider;

    public void setHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        levelText.text = unit.unitlevel;
        hpSlider.maxHP = unit.maxHP;
        hpSlider.value = unit.currentHP;
    }

    public void setHP(int hp)
    {
        hpSlider.value = hp;
    }
}
