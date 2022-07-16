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
