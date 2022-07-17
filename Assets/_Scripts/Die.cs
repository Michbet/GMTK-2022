using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

[System.Serializable]
public class Die
{
    // faces for the dice, can be changed in the editor
    public int[] faces = new int[] {1, 2, 3, 4, 5, 6};

    public int Total => faces.Sum();

    public Die(int numFaces = 6)
    {
        this.faces = new int[numFaces];
    }
    public Die(int[] faces)
    {
        this.faces = faces;
    }
    public int Roll()
    {
        // throw an error if faces doesn't exist
        if (faces == null || faces.Length <= 0)
            throw new Exception("can't roll die with no faces");
        
        
        var index = Random.Range(0, faces.Length);
        return faces[index];
    }

    public void AddRandom(int amount = 1)
    {
        for (int i = 0; i < amount; i++)
        {
            int chosenIndex = Random.Range(0, faces.Length);
            faces[chosenIndex]++;
        }
        Sort();
    }

    public void Sort()
    {
        var sortedFaces = faces.ToList();
        sortedFaces.Sort();
        faces = sortedFaces.ToArray();
    }
    
    public static Die GenerateDie(int totalValue, int maxValueOnFace = 99, int numFaces = 6)
    {
        Die die = new Die();
        die.faces = new int[numFaces];
        for (int i = 0; i < totalValue; i++)
        {
            int chosenIndex = Random.Range(0, numFaces);
            while (die.faces[chosenIndex] >= maxValueOnFace)
            {
                chosenIndex = Random.Range(0, numFaces);
            }

            die.faces[chosenIndex]++;
        }
        die.Sort();
        return die;
    }

}