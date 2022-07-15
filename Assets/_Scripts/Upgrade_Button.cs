using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade_Button : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int initialVal;
    // initialVal represents the value of the die face at the beginning of the upgrade turn
    int currVal;
    // currVal is initialVal, but changes as you increment in the turn. initialVal becomes currVal at the end of the turn
    int goldCount;

    void increment() {
        if(goldCount > 0) {
            currVal++;
            goldCount--;
        }
    }

    void decrement() {
        if(currVal > initialVal && currVal > 0) {
            currVal--;
            goldCount++;
        }
    }

    void endUpgradeTurn() {
        initialVal = currVal;
    }
}
