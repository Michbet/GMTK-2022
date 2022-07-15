using System;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Die
{
    // faces for the dice, can be changed in the editor
    public int[] faces = new int[] {1, 2, 3, 4, 5, 6};
    public int Roll()
    {
        // throw an error if faces doesn't exist
        if (faces == null || faces.Length <= 0)
            throw new Exception("can't roll die with no faces");
        
        
        var index = Random.Range(0, faces.Length);
        return faces[index];
    }
}