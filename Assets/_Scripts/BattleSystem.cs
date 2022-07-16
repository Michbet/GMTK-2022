using System;
using System.Collections;
using _Scripts;
using TMPro;
using UnityEngine;


public class BattleSystem : MonoBehaviour
{
    [SerializeField] private DiceHolder player;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private Transform playerLocation;
    [SerializeField] private Transform enemyLocation;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    [SerializeField] private TMP_Text dialogueTextUI;

    [SerializeField] private DiceHolder enemy;
    
    public event Action<bool> BattleOver = delegate(bool b) {  };

    public string DialogueText
    {
        get => dialogueTextUI.text;
        set => dialogueTextUI.text = value;
    }

    public void StartTurn()
    {
        CalculateTurn();
    }

    public void SetupBattle(int maxHealth, int totalEnemyValue)
    {
        DialogueText = "bruh" ; //box dialogue 

        player.transform.position = playerLocation.position;
        
        // Generate Enemy & Set location
        enemy = Instantiate(enemyPrefab, enemyLocation.position, Quaternion.identity).GetComponent<DiceHolder>();
        if (enemy is EnemyDice enemyDice)
        {
            enemyDice.totalValue = totalEnemyValue;
            enemyDice.GenerateDice();
        }

        // Reset Healths
        player.maxHealth = maxHealth;
        enemy.maxHealth = maxHealth;
        player.ResetHealth();
        enemy.ResetHealth();

        // Update huds
        playerHUD.SetupHUD(player);
        enemyHUD.SetupHUD(enemy);
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
        
        enemyHUD.UpdateHealth();

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
        BattleOver.Invoke(playerWon);
    }

    IEnumerator EnemyAttack(int attackDamage, int opponentBlock, Action onFinish = null)
    {
        int damage = attackDamage - opponentBlock >= 0 ? attackDamage - opponentBlock : 0;
        bool isDead = player.TakeDamage(damage);
        
        playerHUD.UpdateHealth();
        
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
