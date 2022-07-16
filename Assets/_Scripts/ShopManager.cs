using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField]private DieUpgrader speedDieUpgrader;
    [SerializeField]private DieUpgrader blockDieUpgrader;
    [SerializeField]private DieUpgrader attackDieUpgrader;

    [SerializeField]private DiceHolder player;

    public Text goldDisplay;

    public static int goldCount;
    [SerializeField]private int _gold;
    
    public event Action OnShopExit = delegate {  };

    void Start() {
        speedDieUpgrader.SetUpSlots(player.dice.speedDie);
        blockDieUpgrader.SetUpSlots(player.dice.blockDie);
        attackDieUpgrader.SetUpSlots(player.dice.attackDie);

        goldCount = _gold;
    }
    private void Update()
    {
        goldDisplay.text = goldCount.ToString();
    }

    public void ExitShop()
    {
        OnShopExit.Invoke();
    }
    
}
