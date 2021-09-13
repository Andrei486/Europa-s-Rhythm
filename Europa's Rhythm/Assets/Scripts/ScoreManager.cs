using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script should manage the score system: other scripts will keep a reference
//to this script (attched to the same object as Player.cs presumably).
//This script should manage the combo bonus, which should be private.
//It will also draw the UI for score and update it as score changes.

public class ScoreManager : MonoBehaviour
{
    public float Score {get; set;}
    private float comboBonus = 1.0f;
    private int combo;
    [SerializeField] private Text comboText;
    //public int Combo {
        //get {
           // return combo;
       // }
        //set {
           // combo = value;
           // SetComboText(value);
        //}
    //}

    // Start is called before the first frame update
    void Start()
    {
        combo = 0;
        Score = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        comboText.text = combo.ToString();
    }

    //private void SetComboText(int value)
    //{
       // comboText.text = combo.ToString();
    //}

    public void OnClickMiss() {
        //update score & combo
        combo = 0;
        comboBonus = 1.0f;
        Score -= 1;
    }
    public void OnPerfect() {
        //update score & combo
        combo++;
        comboBonus += 0.5f;
        Score += 2f * comboBonus;
    }
    public void OnGood() {
        //update score & combo
        combo++;
        comboBonus += 0.25f;
        Score += 1f * comboBonus;
    }
    public void OnNoteMiss() {
        //update score & combo
        combo = 0;
        comboBonus = 1.0f;
        Score -= 4;
    }
}
