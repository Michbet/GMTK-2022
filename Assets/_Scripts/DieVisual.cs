using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class DieVisual : MonoBehaviour
{
    public TMP_Text text;
    [SerializeField] private Die _die;


    private int[] _dieFacesSet;
    private bool _notSet;
    private int _index;
    private int Index
    {
        get => _index;
        set
        {
            _index = value;
            if (_index >= _dieFacesSet.Length || _index < 0)
                _index = 0;
        }
    }

    private int DisplayedNum
    {
        // get => int.Parse(text.text);
        set => text.text = value.ToString();
    }

    public void SetUp(Die die)
    {
        _die = die;
        _dieFacesSet = (new HashSet<int>(_die.faces)).ToArray();
        SetNumber(-1);
    }

    /// <summary>
    /// Sets the number on the die visual, sets as question mark if number is less than 0
    /// </summary>
    /// <param name="num"></param>
    public void SetNumber(int num)
    {
        if (num < 0)
        {
            _notSet = true;
            text.text = "?";
        }
        else
        {
            DisplayedNum = num;
            _notSet = false;
        }
    }
    
    
    

    private IEnumerator ShowDieFaces(float timeInBetween)
    {
        while (_notSet)
        {
            DisplayedNum = _dieFacesSet[Index++];
            yield return new WaitForSeconds(timeInBetween);
        }
    }

    public void SpinOnce()
    {
        StartCoroutine(SpinOnceRoutine(10));
    }
    //_die.faces
    private IEnumerator SpinOnceRoutine(float speed)
    {
        var startRotation = transform.rotation;
        
        for (float i = 0; i < 360; i += speed)
        {
            var eulerAngles = transform.rotation.eulerAngles;
            eulerAngles.z = i;
            transform.rotation = Quaternion.Euler(eulerAngles);
            yield return null;
            
        }
        //
        // var duration = 5;
        // for (float i = duration; i >= 0; i -= Time.deltaTime)
        // {
        //     Debug.Log(i);
        //     yield return null;
        // }
        

        transform.rotation = startRotation;

    }
}
