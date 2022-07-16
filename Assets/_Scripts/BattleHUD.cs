using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BattleHUD : MonoBehaviour
{
<<<<<<< Updated upstream
    public DiceHolder DiceHolder;
    public StatsDiceBattleVisual StatsDiceVisual;
    // public HealthBar HealthBar;
    
    public void setHUD(DiceHolder diceHolder)
    {
        // Set Dice visual
        StatsDiceVisual.SetUp(diceHolder.dice);
=======
    public Text nameText;
    public Text LevelText;
    public Slider hpSlider;

    public void setHUD(Unit unit)
    {
        nameText.text = unit.unitName;
        LevelText.text = "LVL"+ unit.unitLevel;
        hpSlider.maxValue = unit.maxHP;
        hpSlider.value = unit.currentHP;
>>>>>>> Stashed changes
    }

    public void UpdateDice(TurnStats stats)
    {
        StatsDiceVisual.SetDiceNums(stats);
    }
}
