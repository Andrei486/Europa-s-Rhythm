using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script should manage the score system: other scripts will keep a reference
//to this script (attched to the same object as Player.cs presumably).
//This script should manage the combo bonus, which should be private.
//It will also draw the UI for score and update it as score changes.

public class ScoreManager : MonoBehaviour
{
    public float Score {get; set;}
    private float comboBonus = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClickMiss() {
        //update score & combo
    }
    public void OnPerfect() {
        //update score & combo
    }
    public void OnGood() {
        //update score & combo
    }
    public void OnNoteMiss() {
        //update score & combo
    }
}
