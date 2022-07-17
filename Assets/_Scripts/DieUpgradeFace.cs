using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DieUpgradeFace : MonoBehaviour
{
    private Die _die = null;
    private int _index;
    //private int initialVal;
    //initialVal = _die.faces[_index];
    // initialVal is basically a temp variable for the specified array index that ensures the player won't 
    // remove below the starting point
    private int upgrade_count = 0;

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
        if(ShopManager.goldCount > 0) {
            _die.faces[_index] += 1;
            ShopManager.goldCount--;
            text.text = _die.faces[_index].ToString();
            upgrade_count++;
            SFXManager.Play("Increase Face");
        }
    }
    
    
    public void RemoveOne()
    {
        
        if(upgrade_count > 0 && _die.faces[_index] > 0) {
            _die.faces[_index] -= 1;
            ShopManager.goldCount++;
            upgrade_count--;
            text.text = _die.faces[_index].ToString();
            SFXManager.Play("Decrease Face");
        }
    }

    public void EndUpgradeTurn() {
        //initialVal = _die.faces[_index];
        upgrade_count = 0;
    }
}
