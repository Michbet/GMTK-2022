using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]private DieUpgrader speedDieUpgrader;
    [SerializeField]private DieUpgrader blockDieUpgrader;
    [SerializeField]private DieUpgrader attackDieUpgrader;

    [SerializeField]private DiceHolder player;

    [SerializeField] private TMP_Text enemySpeedDieText;
    [SerializeField] private TMP_Text enemyBlockDieText;
    [SerializeField] private TMP_Text enemyAttackDieText;

    public Text goldDisplay;

    public static int goldCount;
    
    public event Action OnShopExit = delegate {  };

    void Start() {
        speedDieUpgrader.SetUpSlots(player.dice.speedDie);
        blockDieUpgrader.SetUpSlots(player.dice.blockDie);
        attackDieUpgrader.SetUpSlots(player.dice.attackDie);
    }

    public void SetUp(StatsDice nextEnemyDice)
    {
        enemySpeedDieText.text = nextEnemyDice.speedDie.faces.Max().ToString();
        enemyBlockDieText.text = nextEnemyDice.blockDie.faces.Max().ToString();
        enemyAttackDieText.text = nextEnemyDice.attackDie.faces.Max().ToString();
    }
    
    private void Update()
    {
        goldDisplay.text = goldCount.ToString();
    }

    public void ExitShop()
    {
        foreach (var face in speedDieUpgrader.dieUpgradeSlots)
        {
            face.EndUpgradeTurn();
        }
        foreach (var face in blockDieUpgrader.dieUpgradeSlots)
        {
            face.EndUpgradeTurn();
        }
        foreach (var face in attackDieUpgrader.dieUpgradeSlots)
        {
            face.EndUpgradeTurn();
        }
        OnShopExit.Invoke();
    }

}
