using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;


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

    [SerializeField] private EnemyDice enemyPrefab;

    [SerializeField] private StatsDice _enemyStatsDice;

    public static int level;

    public void TransitionToShop()
    {
        _shopManager.gameObject.SetActive(true);
        _battleSystem.gameObject.SetActive(false);
        
        ShopManager.goldCount += (int)goldScaler.Value(level);
        _enemyStatsDice = GenerateEnemyDice(level);
        _shopManager.SetUp(_enemyStatsDice);
    }

    public void TransitionToBattle()
    {
        _shopManager.gameObject.SetActive(false);
        _battleSystem.gameObject.SetActive(true);
        
        _battleSystem.SetupBattle((int)healthScaler.Value(level), _enemyStatsDice);
    }
    private void Update()
    {
        levelText.text = "Lvl " +  level;
    }
    private void Start()
    {
        level = levelInit;
        _enemyStatsDice = GenerateEnemyDice(level);
        TransitionToBattle();
    }

    private StatsDice GenerateEnemyDice(int lvl)
    {
        
        var total = (int) enemyStatsScaler.Value(lvl);
        return StatsDice.GenerateStatsDice(total, enemyPrefab.speedPercent, enemyPrefab.blockPercent,
            enemyPrefab.speedPercent);
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
            SceneManager.LoadScene("GameOver");
        }
    }
}
