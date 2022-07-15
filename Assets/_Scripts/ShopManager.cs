using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField]private DieUpgrader speedDieUpgrader;
    [SerializeField]private DieUpgrader blockDieUpgrader;
    [SerializeField]private DieUpgrader attackDieUpgrader;

    [SerializeField]private PlayerDice player;

    public static int goldCount;
    [SerializeField]private int _gold;

    void Start() {
        speedDieUpgrader.SetUpSlots(player.speedDie);
        blockDieUpgrader.SetUpSlots(player.blockDie);
        attackDieUpgrader.SetUpSlots(player.attackDie);

        goldCount = _gold;
    }
}
