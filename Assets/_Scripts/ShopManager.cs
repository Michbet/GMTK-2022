using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]private DieUpgrader speedDieUpgrader;
    [SerializeField]private DieUpgrader blockDieUpgrader;
    [SerializeField]private DieUpgrader attackDieUpgrader;

    [SerializeField]private PlayerDice player;

    void Start() {
        speedDieUpgrader.SetUpSlots(player.speedDie);
        blockDieUpgrader.SetUpSlots(player.blockDie);
        attackDieUpgrader.SetUpSlots(player.attackDie);
    }
}
