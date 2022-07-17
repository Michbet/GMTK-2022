using System;
using System.Collections;
using _Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

[System.Serializable]
public class BattleEntity
{
    public string name;
    public DiceHolder holder;
    public BattleHUD hud;
    public Transform startingPosition;
}

public class BattleSystem : MonoBehaviour
{
    [SerializeField] private BattleEntity player;
    [SerializeField] private BattleEntity enemy;
    
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private TMP_Text dialogueTextUI;

    [SerializeField] private Button playButton;
    [SerializeField] private Button moveOnButton;
    
    public event Action<bool> BattleOver = delegate(bool b) {  };

    private bool _moveOnPressed = false;
    private bool _inTurn = false;

    public string DialogueText
    {
        get => dialogueTextUI.text;
        set => dialogueTextUI.text = value;
    }

    public void StartTurn()
    {
        StartCoroutine(CalculateTurn());
    }

    public void SetupBattle(int maxHealth, StatsDice enemyStatsDice)
    {
        DialogueText = "Time to Roll" ; //box dialogue 
        _inTurn = false;
        _moveOnPressed = false;

        // set player location
        player.startingPosition.gameObject.SetActive(false);
        player.holder.transform.SetParent(transform);
        player.holder.transform.position = player.startingPosition.position;
        player.holder.transform.localScale = player.startingPosition.localScale;
        
        // Generate Enemy & Set enemy location
        var enemyHolder = Instantiate(enemyPrefab, transform).GetComponent<DiceHolder>();
        enemy.startingPosition.gameObject.SetActive(false);
        if (enemyHolder is EnemyDice enemyDice) // should always be true, just for casting
        {
            enemyDice.dice = enemyStatsDice;
        }

        enemyHolder.transform.position = enemy.startingPosition.position;
        enemyHolder.transform.localScale = enemy.startingPosition.localScale;
        enemy.holder = enemyHolder;

        // Reset Healths
        player.holder.maxHealth = maxHealth;
        enemy.holder.maxHealth = maxHealth;
        player.holder.ResetHealth();
        enemy.holder.ResetHealth();

        // Setup huds
        player.hud.SetupHUD(player.holder);
        enemy.hud.SetupHUD(enemy.holder);
    }
    
    private IEnumerator CalculateTurn()
    {
        // Reset Dice values 
        player.hud.StatsDiceVisual.SetDiceNums(TurnStats.NotDecided);
        enemy.hud.StatsDiceVisual.SetDiceNums(TurnStats.NotDecided);
        
        player.holder.PlayAnim("Roll");
        enemy.holder.PlayAnim("Roll");
        
        // Calculate all the stats
        var playerTurnStats = player.holder.dice.Roll();
        var enemyTurnStats = enemy.holder.dice.Roll();
        _inTurn = true;
        _moveOnPressed = false;
        
        // Determine who goes first from speed;
        bool playerFirst = playerTurnStats.speed >= enemyTurnStats.speed;
        
        // todo: play dice roll animation (for speed)
        player.hud.StatsDiceVisual.speedDieVisual.SpinOnce();
        enemy.hud.StatsDiceVisual.speedDieVisual.SpinOnce();
       

        // play animation

        //yield return new WaitForSeconds(1.2f);

        // Update DieVisual
        player.hud.StatsDiceVisual.speedDieVisual.SetNumber(playerTurnStats.speed);
        
        enemy.hud.StatsDiceVisual.speedDieVisual.SetNumber(enemyTurnStats.speed);

        // wait for input after speed calculation
        DialogueText = playerFirst ? $"{player.name} move first!" : $"{enemy.name} moved first!";
        
        yield return new WaitUntil(() => _moveOnPressed);
        _moveOnPressed = false;
        
        // startup coroutines for gameplay
        if (playerFirst)
        {
            StartCoroutine(ProcessTurn(player, enemy, playerTurnStats.attack, enemyTurnStats.block,
            () => StartCoroutine(ProcessTurn(enemy, player, enemyTurnStats.attack, playerTurnStats.block, 
                () => _inTurn = false))));
        }
        else
        {
            StartCoroutine(ProcessTurn(enemy, player, enemyTurnStats.attack, playerTurnStats.block, 
                () => StartCoroutine(ProcessTurn(player, enemy, playerTurnStats.attack, enemyTurnStats.block, 
                    () => _inTurn = false))));
        }
        
        
    }

    private void Update()
    {
        playButton.gameObject.SetActive(!_inTurn);
        moveOnButton.enabled = _inTurn;

        if (_inTurn && !HelperFunctions.IsMouseOverButtonUI() && Input.GetMouseButtonDown(0))
        {
            MoveOn();
        }
    }

    IEnumerator ProcessTurn(BattleEntity attacker, BattleEntity defender, int attackDamage, int defenderBlock,
        Action onFinish = null)
    {
        // todo: play die visual animation
        attacker.hud.StatsDiceVisual.attackDieVisual.SpinOnce();
        defender.hud.StatsDiceVisual.blockDieVisual.SpinOnce();

        // update number
        attacker.hud.StatsDiceVisual.attackDieVisual.SetNumber(attackDamage); // todo: have easier access
        defender.hud.StatsDiceVisual.blockDieVisual.SetNumber(defenderBlock);

        int damage;
        if(attackDamage == defenderBlock && (attackDamage!=0&&defenderBlock!=0))
        {
            damage = 1;
        }
        else
            damage = attackDamage - defenderBlock >= 0 ? attackDamage - defenderBlock : 0;
        int blockedDamage = (attackDamage - damage);
        
        // Process damage and update UI
        SFXManager.Play("Damage");
        bool isDead = defender.holder.TakeDamage(damage);
        Debug.Log($"{attacker.name} dealt {damage} to {defender.name}");
        defender.hud.UpdateHealth();

        // wait for input after damage indication
        DialogueText = $"{attacker.name} attacked {defender.name} for {attackDamage} damage" +
                       $"{(blockedDamage > 0 ? $", but {defender.name} blocked {defenderBlock} damage." : "!" )}";
        yield return new WaitUntil(() => _moveOnPressed);
        _moveOnPressed = false;

        DialogueText = damage > 0 ? $"{defender.name} took {damage} damage." : $"{defender.name} took no damage!";
        yield return new WaitUntil(() => _moveOnPressed);
        _moveOnPressed = false;
        
        // // wait for input after blocked damage indication (if any)
        // if (blockedDamage > 0)
        // {
        //     DialogueText = $"but {defender.name} blocked {blockedDamage} damage, so only dealing {damage} damage!";
        //     yield return new WaitUntil(() => _moveOnPressed);
        //     _moveOnPressed = false;
        // }

        //check if the enemy is dead
        if(isDead)
        {
            StartCoroutine(EndBattle(defender == enemy));
            yield break;
        }
        
        // process other turn if it's not the last one
        if (onFinish != null)
            onFinish();
    }
    
    IEnumerator EndBattle(bool playerWon)
    {
        if(playerWon)
        {
            DialogueText = "You Won The Battle!";
            Destroy(enemy.holder.gameObject);
        }else
        {
            DialogueText = "You were defeated";
        }
        // wait for input to end battle
        yield return new WaitUntil(() => _moveOnPressed);
        _moveOnPressed = false;
        BattleOver.Invoke(playerWon);
    }

    public void MoveOn() => _moveOnPressed = true;

}
