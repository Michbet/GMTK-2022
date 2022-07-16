using System.Collections;
using System.Collections.Generic;
<<<<<<< Updated upstream
using System.Threading.Tasks;
using _Scripts;
using TMPro;
=======
>>>>>>> Stashed changes
using UnityEngine.UI;
using UnityEngine;


public class BattleSystem : MonoBehaviour
{
<<<<<<< Updated upstream
    [SerializeField] private DiceHolder player;
    [SerializeField] private DiceHolder enemy;
    public string DialogueText
    {
        get => dialogueTextUI.text;
        set => dialogueTextUI.text = value;
    }

    [SerializeField] private TMP_Text dialogueTextUI;
=======
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;
>>>>>>> Stashed changes

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;


    void Start()
    {
<<<<<<< Updated upstream
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

=======
        state = BattleState.Start;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab,playerBattleStation);
        playerGO.GetComponents<Unit>();

        GameObject enemyGO= Instantiate(enemyPrefab, enemyBattleStation);
        enemyGO.GetComponents<Unit>();

        dialogueText.text = "A wild " + enemyUnit.unitName + " aproaches..." ; //box dialogue 

        playerHUD.setHUD(playerUnit);
        enemyHUD.setHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PlayerTurn;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead =enemyUnit.TakeDamage(playerUnit.damage);

        enemyHUD.setHP(enemyUnit.currentHP);
        dialogueText.text = "Successful hit";


        //damage enemy
        yield return new WaitForSeconds(2f);
        //check if the enemy is dead
        if(isDead)
        {
            state = BattleState.Won;
            // end battle
        }else{
            state = BattleState.EnemyTurn;
            StartCoroutine(EnemyTurn());
            //enemy turn
        }
        //change state based on what happened
    }

    void EndBattle()
    {
        if(state == BattleState.Won)
        {
            dialogueText.text = "You Won The Battle!";
        }else if(state == BattleState.Lost){
            dialogueText.text = "You were defeated";
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.unitName + "attacked";
        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);
        playerHUD.setHP(playerUnit.currentHP);
        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = BattleState.Lost;
            EndBattle();
        }else{
            state = BattleState.PlayerTurn;
            PlayerTurn();
        }

    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an option";
    }

    void OnAttackButton()
    {
        if(state != BattleState.PlayerTurn)
        return;

        StartCoroutine(PlayerAttack());
    }

    
>>>>>>> Stashed changes
}
