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
        text.text = die.faces[0].ToString();
    }

    public void SetNumber(int num) => text.text = num.ToString();
}
