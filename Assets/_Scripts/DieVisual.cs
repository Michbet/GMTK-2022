using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DieVisual : MonoBehaviour
{
    public TMP_Text text;
    [SerializeField] private Die _die;

    public void SetUp(Die die)
    {
        _die = die;
        SetNumber(-1);
    }

    /// <summary>
    /// Sets the number on the die visual, sets as question mark if number is less than 0
    /// </summary>
    /// <param name="num"></param>
    public void SetNumber(int num) => text.text = num < 0 ? "?" : num.ToString();
}
