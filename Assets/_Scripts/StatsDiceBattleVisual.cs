using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;

public class StatsDiceBattleVisual : MonoBehaviour
{
    public DieVisual speedDieVisual;
    public DieVisual blockDieVisual;
    public DieVisual attackDieVisual;

    private DieVisual[] _dice => new[] {speedDieVisual, blockDieVisual, attackDieVisual};

    public void SetUp(StatsDice statsDice)
    {
        speedDieVisual.SetUp(statsDice.speedDie);
        blockDieVisual.SetUp(statsDice.blockDie);
        attackDieVisual.SetUp(statsDice.attackDie);
    }
}
