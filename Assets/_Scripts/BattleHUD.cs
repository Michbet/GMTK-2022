using System.Collections;
using System.Collections.Generic;
using _Scripts;
using UnityEngine.UI;
using UnityEngine;

public class BattleHUD : MonoBehaviour
{
    public Text nameText;
    public Slider hpSlider;
    
    

    public void setHUD(DiceHolder diceHolder)
    {
        
    }

    public void setHP(int hp)
    {
        hpSlider.value = hp;
    }
}
