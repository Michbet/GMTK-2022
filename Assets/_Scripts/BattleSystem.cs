using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using _Scripts;
using TMPro;
using UnityEngine.UI;
using UnityEngine;


public class BattleSystem : MonoBehaviour
{
    [SerializeField] private DiceHolder player;
    [SerializeField] private DiceHolder enemy;
    public string DialogueText
    {
        get => dialogueTextUI.text;
        set => dialogueTextUI.text = value;
    }

    [SerializeField] private TMP_Text dialogueTextUI;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;


    void Start()
    {
        SetupBattle();
    }

    public void StartTurn()
    {
        CalculateTurn();
    }

    private void SetupBattle()
    {
        // set health
        DialogueText = "bruh" ; //box dialogue 

        playerHUD.setHUD(player);
        enemyHUD.setHUD(enemy);
    }

    private void CalculateTurn()
    {
        var playerTurnStats = player.dice.Roll();
        var enemyTurnStats = enemy.dice.Roll();

        bool playerFirst = playerTurnStats.speed >= enemyTurnStats.speed;

        playerHUD.UpdateDice(playerTurnStats);
        enemyHUD.UpdateDice(enemyTurnStats);

        if (playerFirst)
        {
            StartCoroutine(PlayerAttack(playerTurnStats.attack, enemyTurnStats.block, 
                () => StartCoroutine(EnemyAttack(enemyTurnStats.attack, playerTurnStats.block))));
        }
        else
        {
            StartCoroutine(EnemyAttack(enemyTurnStats.attack, playerTurnStats.block,
                () => StartCoroutine(PlayerAttack(playerTurnStats.attack, enemyTurnStats.block))));
        }
        
    }

    IEnumerator PlayerAttack(int attackDamage, int opponentBlock, Action onFinish = null)
    {
        int damage = attackDamage - opponentBlock >= 0 ? attackDamage - opponentBlock : 0;
        bool isDead = enemy.TakeDamage(damage);

        DialogueText = "Player Attacking";
        Debug.Log($"Player dealt {damage} to the Enemy");
        yield return null;
        
        //check if the enemy is dead
        if(isDead)
        {
            EndBattle(true);
            yield break;
        }
        //change state based on what happened
        if (onFinish != null)
            onFinish();
    }

    void EndBattle(bool playerWon)
    {
        if(playerWon)
        {
            DialogueText = "You Won The Battle!";
        }else
        {
            DialogueText = "You were defeated";
        }
        player.ResetHealth();
        enemy.ResetHealth();
    }

    IEnumerator EnemyAttack(int attackDamage, int opponentBlock, Action onFinish = null)
    {
        int damage = attackDamage - opponentBlock >= 0 ? attackDamage - opponentBlock : 0;
        bool isDead = player.TakeDamage(damage);
        
        DialogueText = "Enemy Attacking";
        Debug.Log($"Enemy dealt {damage} to the Player");
        yield return null;
        
        //check if the enemy is dead
        if(isDead)
        {
            EndBattle(false);
            yield break;
        }
        //change state based on what happened
        if (onFinish != null)
            onFinish();

    }

}
