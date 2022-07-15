using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DieUpgradeFace : MonoBehaviour
{
    private Die _die = null;
    private int _index;

    public TMP_Text text;

    public void SetUp(Die die, int index)
    {
        this._die = die;
        this._index = index;
        text.text = _die.faces[_index].ToString();
    }

    public void Set(int value)
    {
        _die.faces[_index] = value;
        text.text = _die.faces[_index].ToString();
    }

    public void AddOne()
    {
        _die.faces[_index] += 1;
        text.text = _die.faces[_index].ToString();
    }
    
    
    public void RemoveOne()
    {
        _die.faces[_index] -= 1;
        text.text = _die.faces[_index].ToString();
    }
}
