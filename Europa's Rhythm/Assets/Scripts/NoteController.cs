using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script should manage the note's movement (for now variable for speed)
//as well as detect collisions, if the collision is with the miss detection line
//(see layers) then destroy the object, show feedback (later) and update score

public class NoteController : MonoBehaviour
{
    public ScoreManager scoreManager;
    public static float NoteSpeed = 2.0f;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * NoteController.NoteSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log("collision");
        GameObject other = collider.gameObject;
        //layer check may be redundant, should be assured by physics collision matrix
        if (other.layer == LayerMask.NameToLayer("Miss")) {
            scoreManager.OnNoteMiss();
            Destroy(gameObject);
        }
    }
}
