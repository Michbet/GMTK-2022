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

    [SerializeField] private Binomial enemyStatsFunction;
    [SerializeField] private Linear healthFunction;
    [SerializeField] private Linear goldFunction;

    // [SerializeField] private AnimationCurve enemyStatsCurve;
    // // [SerializeField] private AnimationCurve healthCurve;
    // [SerializeField] private AnimationCurve goldCurve;

    [SerializeField] private int levelInit = 1;
    [SerializeField] private TMP_Text levelText;

    [SerializeField] private EnemyDice enemyPrefab;

    private StatsDice _enemyStatsDice;
    private StatsDice _playerInitDice;
    public static int level;

    public void TransitionToShop()
    {
        // todo: Play shop music
        
        _shopManager.gameObject.SetActive(true);
        _battleSystem.gameObject.SetActive(false);
        
        // Debug.Log("Calculating gold added");
        // var targetValue = Mathf.RoundToInt(goldFunction.Calculate(level)) + _playerInitDice.TotalValue;
        // Debug.Log("function value: " + (int) goldFunction.Calculate(level));
        // Debug.Log("init value: " +  _playerInitDice.TotalValue);
        // Debug.Log("target value (function + init): " +  targetValue);
        // Debug.Log("current player value: " + player.dice.TotalValue);
        // Debug.Log("gold to add: " + (targetValue - player.dice.TotalValue));
        
        ShopManager.goldCount += Mathf.RoundToInt(goldFunction.Calculate(level));
        
        level++;
        _enemyStatsDice = GenerateEnemyDice(level);
        _shopManager.SetUp(_enemyStatsDice);
    }

    public void TransitionToBattle()
    {
        MusicManager.Play("Battle Music");
        
        _shopManager.gameObject.SetActive(false);
        _battleSystem.gameObject.SetActive(true);
        
        _battleSystem.SetupBattle(Mathf.RoundToInt(healthFunction.Calculate(level)), _enemyStatsDice);
    }
    private void Update()
    {
        levelText.text = "Lvl " +  level;
    }
    private void Start()
    {
        level = levelInit;
        _playerInitDice = player.dice;
        _enemyStatsDice = GenerateEnemyDice(level);
        TransitionToBattle();
    }

    private StatsDice GenerateEnemyDice(int lvl)
    {
        var total = Mathf.RoundToInt(enemyStatsFunction.Calculate(lvl));
        Debug.Log("Enemies Total " + total);
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
        }
        else
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
