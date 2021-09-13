using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//This script should manage the player's inputs. Placed on an arbitrary "player" object
//in the scene, it checks every time the player presses one of the 4 note keys
//to see whether a note of the correct type is overlapping the target line.
//If there is such a note, the note should be destroyed and the score updated accordingly.

public class Player : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private string[] noteTags;
    private static string[] noteColliderLayers = {"Perfect", "Good"};
    [SerializeField] private GameObject targetLine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnNoteKey(int noteColumn) {
        HashSet<GameObject> overlappingNotes = new HashSet<GameObject>();

        string noteTag = noteTags[noteColumn - 1];
        int noteMask = LayerMask.GetMask(noteColliderLayers);
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(targetLine.transform.position, targetLine.transform.localScale, 0, noteMask);
        foreach (Collider2D collider in hitColliders) {
            if (collider.tag == noteTag) {
                //perfect and good colliders are on children of the note object
                overlappingNotes.Add(collider.transform.parent.gameObject);
            }
        }
        bool clickedNote = false;
        bool isPerfect = false;
        // need to somehow sort notes by 
        foreach (GameObject note in overlappingNotes) {
            Collider2D perfectCollider = note.transform.Find("Perfect Hitbox").GetComponent<Collider2D>();
            Collider2D goodCollider = note.transform.Find("Good Hitbox").GetComponent<Collider2D>();
            //check if the timing was perfect
            if (Array.IndexOf(hitColliders, perfectCollider) > -1) {
                scoreManager.OnPerfect();
                clickedNote = true;
                isPerfect = true;
            } else if (Array.IndexOf(hitColliders, goodCollider) > -1) {
                scoreManager.OnGood();
                clickedNote = true;
            }
            DestroyNote(note, isPerfect);
            if (clickedNote) {
                break; //don't click more than one note at a time
            }
        }
        if (!clickedNote) {
            scoreManager.OnClickMiss();
        }
    }
    public void DestroyNote(GameObject note, bool isPerfect) {
        Destroy(note); //also show feedback to player depending on perfect or not
    }
    //below: methods called from player input
    public void OnKey1() {
        OnNoteKey(1);
    }
    public void OnKey2() {
        OnNoteKey(2);
    }
    public void OnKey3() {
        OnNoteKey(3);
    }
    public void OnKey4() {
        OnNoteKey(4);
    }
    public void OnPause() {
        //not yet implemented
    }
}
