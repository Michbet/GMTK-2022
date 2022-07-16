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
    // public HealthBar HealthBar;
    
    public void setHUD(DiceHolder diceHolder)
    {
        // Set Dice visual
        StatsDiceVisual.SetUp(diceHolder.dice);
    }

    public void UpdateDice(TurnStats stats)
    {
        StatsDiceVisual.SetDiceNums(stats);
    }
}
