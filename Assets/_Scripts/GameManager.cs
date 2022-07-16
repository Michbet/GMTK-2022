using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private ShopManager _shopManager;
    [SerializeField] private BattleSystem _battleSystem;
    [SerializeField] private DiceHolder player;

    [SerializeField] private Scaler enemyStatsScaler;
    [SerializeField] private Scaler healthScaler;
    [SerializeField] private Scaler goldScaler;

    [SerializeField] private int levelInit = 1;
    [SerializeField] private TMP_Text levelText;

    public static int level;

    public void TransitionToShop()
    {
        ShopManager.goldCount += (int)goldScaler.Value(level);
        _shopManager.gameObject.SetActive(true);
        _battleSystem.gameObject.SetActive(false);
    }

    public void TransitionToBattle()
    {
        _shopManager.gameObject.SetActive(false);
        _battleSystem.gameObject.SetActive(true);
        
        _battleSystem.SetupBattle((int)healthScaler.Value(level), (int)enemyStatsScaler.Value(level));
    }
    private void Update()
    {
        levelText.text = "Lvl " +  level;
    }
    private void Start()
    {
        TransitionToBattle();
        level = levelInit;
    }

    private void OnEnable()
    {
        _battleSystem.BattleOver += OnBattleOver;
        _shopManager.OnShopExit += OnShopExit;
    }

    private void OnDisable()
    {
        _battleSystem.BattleOver -= OnBattleOver;
        _shopManager.OnShopExit -= OnShopExit;
    }

    private void OnShopExit()
    {
        TransitionToBattle();
    }

    private void OnBattleOver(bool playerWon)
    {
        if (playerWon)
        {
            TransitionToShop();
            level++;
        }
        else
        {
            Debug.Log("Player died, try again");
            TransitionToBattle();
        }
    }
}
