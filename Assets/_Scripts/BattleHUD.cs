using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine.UI;
using UnityEngine;

public class BattleHUD : MonoBehaviour
{
    public DiceHolder DiceHolder;
    public StatsDiceBattleVisual StatsDiceVisual;
    public HealthBar HealthBar;
    
    public void SetupHUD(DiceHolder diceHolder)
    {
        // Set Dice visual
        DiceHolder = diceHolder;
        StatsDiceVisual.SetUp(diceHolder.dice);
        HealthBar.SetMaxHealth(diceHolder.maxHealth);
    }

    public void UpdateDice(TurnStats stats)
    {
        StatsDiceVisual.SetDiceNums(stats);
    }

    public void UpdateHealth() => HealthBar.SetHealth(DiceHolder.currentHealth);
}
