using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DieUpgradeSlot : MonoBehaviour
{
    private Die _die = null;
    private int _index;

    public TMP_Text text;

    public void SetUp(Die die, int index)
    {
        this._die = die;
        this._index = index;
    }

    public void Set(int value)
    {
        _die.faces[_index] = value;
    }

    public void AddOne()
    {
        _die.faces[_index] += 1;
    }
    
    
    public void RemoveOne()
    {
        _die.faces[_index] -= 1;
    }
}
