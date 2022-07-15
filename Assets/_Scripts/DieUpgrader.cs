using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class DieUpgrader : MonoBehaviour
{
    public DieUpgradeFace[] dieUpgradeSlots;

    private Die _die;

    private void Start()
    {
        /*_die = new Die
        {
            faces = new int[] {1, 2, 3, 4, 5, 6}
        };
        SetUpSlots(_die); */
    }

    public void SetUpSlots(Die die)
    {
        _die = die;
        for (int i = 0; i < _die.faces.Length; i++)
        {
            if(i < dieUpgradeSlots.Length)
                dieUpgradeSlots[i].SetUp(_die, i);
        }
    }
}
