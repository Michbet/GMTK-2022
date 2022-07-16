using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine.UI;
using UnityEngine;


public enum BattleState {Start, PlayerTurn, EnemyTurn, Won, Lost}

public class BattleSystem : MonoBehaviour
{

    [SerializeField] private DiceHolder player;
    [SerializeField] private DiceHolder enemy;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;


    public BattleState state;
    void Start()
    {
        state = BattleState.Start;
        // StartCoroutine(SetupBattle());
    }
    
    // IEnumerator SetupBattle()
    // {
    //     playerHUD.setHUD(player);
    //     enemyHUD.setHUD(enemyUnit);
    //
    //     yield return new WaitForSeconds(2f);
    //
    //     state = BattleState.PLAYERTURN;
    //     playerTurn();
    // }
    //
    // IEnumerator PlayerAttack()
    // {
    //     bool isDead =enemyUnit.TakeDamage(playerUnit.damage);
    //
    //     enemyHUD.SetHP(enemyUnit.currentHP);
    //     dialogueText.text = "Successful hit";
    //
    //
    //     //damage enemy
    //     yield return new WaitForSeconds(2f);
    //     //check if the enemy is dead
    //     if(isDead)
    //     {
    //         state = BattleState.WON;
    //         // end battle
    //     }else{
    //         state = BattleState.EnemyTurn;
    //         StartCoroutine(EnemyTurn());
    //         //enemy turn
    //     }
    //     //change state based on what happened
    // }
    //
    // void EndBattle()
    // {
    //     if(state == BattleState.WON)
    //     {
    //         dialogueText.text = "You Won The Battle!";
    //     }else if(state == BattleState.Lost){
    //         dialogueText.text = "You were defeated";
    //     }
    // }
    //
    // IEnumerator EnemyTurn()
    // {
    //     dialogueText.text = enemyUnit.unitname + "attacked";
    //     yield return new WaitForSeconds(1f);
    //
    //     bool isDead = playeUnit.TakeDamage(enemyUnit.damage);
    //     playerHUD.SetHP(playerUnit.currentHP);
    //     yield return new WaitForSeconds(1f);
    //
    //     if(isDead)
    //     {
    //         state = BattleState.LOST;
    //         EndBattle();
    //     }else{
    //         state = BattleState.PLAYERTURN;
    //         PlayerTurn();
    //     }
    //
    // }
    //
    // void PlayerTurn()
    // {
    //     dialogueText.text = "Choose an option";
    // }
    //
    // void OnAttackButton()
    // {
    //     if(state != BattleState.PLAYERTURN)
    //         return;
    //
    //     StartCoroutine(PlayerAttack());
    // }

    
}
