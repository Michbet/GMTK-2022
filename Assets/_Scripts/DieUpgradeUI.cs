using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class DieUpgradeUI : MonoBehaviour
{
    public DieUpgradeSlot[] dieUpgradeSlots;

    private Die _die;

    public void SetUpSlots(Die die)
    {
        _die = die;
        for (int i = 0; i < _die.faces.Length; i++)
        {
            dieUpgradeSlots[i].SetUp(_die, i);
        }
    }
}
