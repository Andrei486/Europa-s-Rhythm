using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesManager : MonoBehaviour
{
    private List<Note> LevelNotes = new List<Note>();
    [SerializeField] private TextAsset _JsonAsset; //the level select will set this
    private float SongStopwatch = -5;
    [SerializeField] private GameObject notePrefab;
    [SerializeField] private AudioClip songAudio;
    [SerializeField] private AudioSource songSource;
    public bool Paused { get; set; }
    private float NoteDistance = 9;
    private readonly float NoteHeight = 5;
    [SerializeField] Material[] materials;
    private bool SongStarted;
    public TextAsset JsonAsset { get => _JsonAsset; set => _JsonAsset = value; }


    // Start is called before the first frame update
    void Start()
    {
        LoadNotesFromJson();
        Paused = false;
        SongStarted = false;
        songSource.clip = songAudio;
        
    }

    // Update is called once per frame
    void Update()
    {
        SongStopwatch += Time.deltaTime;
        if (SongStopwatch >= 0 && !SongStarted)
        {
            songSource.Play();
            SongStarted = true;
        }
        if (!(LevelNotes.Count == 0) && (SongStopwatch >= (LevelNotes[0].timestamp - (NoteDistance / NoteController.NoteSpeed))))
        {
            GenerateNotes(LevelNotes[0]);
            LevelNotes.RemoveAt(0);
            Debug.Log("levelNotes count -> " + LevelNotes.Count);
        }
    }

    private void LoadNotesFromJson()
    {

        NoteArray notes = JsonUtility.FromJson<NoteArray>(JsonAsset.text);
        Debug.Log(notes.notes);
        LevelNotes = new List<Note>(notes.notes);
        foreach (Note i in LevelNotes)
        {
            Debug.Log(i);
        }
        Debug.Log(notes);
        //transform.Translate(Vector2.up);

    }

    private void GenerateNotes(Note note)
    {
        Debug.Log(note.timestamp);
        /*
        Debug.Log(note.note1);
        Debug.Log(note.note2);
        Debug.Log(note.note3);
        Debug.Log(note.note4);
        */
        Debug.Log("materials count -> " + materials.Length);

        Vector3 note1Position = new Vector3(-3, NoteHeight, 0);
        Vector3 note2Position = new Vector3(-1, NoteHeight, 0);
        Vector3 note3Position = new Vector3(1, NoteHeight, 0);
        Vector3 note4Position = new Vector3(3, NoteHeight, 0);
        Quaternion quaternion = new Quaternion(0,0,0,0);
        if (note.note1)
        {
            //GameObject note1 = Instantiate(notePrefab, transform);
            GameObject note1 = Instantiate(notePrefab, note1Position, quaternion, transform);
            SpriteRenderer noteRenderer = note1.GetComponent<SpriteRenderer>();
            noteRenderer.material = materials[0];
            ChangeNoteTags(note1, "Note1");
        }
        if (note.note2)
        {
            GameObject note2 = Instantiate(notePrefab, note2Position, quaternion, transform);
            SpriteRenderer noteRenderer = note2.GetComponent<SpriteRenderer>();
            noteRenderer.material = materials[1];
            ChangeNoteTags(note2, "Note2");
        }
        if (note.note3)
        {
            GameObject note3 = Instantiate(notePrefab, note3Position, quaternion, transform);
            SpriteRenderer noteRenderer = note3.GetComponent<SpriteRenderer>();
            noteRenderer.material = materials[2];
            ChangeNoteTags(note3, "Note3");
        }
        if (note.note4)
        {
            GameObject note4 = Instantiate(notePrefab, note4Position, quaternion, transform);
            SpriteRenderer noteRenderer = note4.GetComponent<SpriteRenderer>();
            noteRenderer.material = materials[3];
            ChangeNoteTags(note4, "Note4");
        }
    }

    void ChangeNoteTags(GameObject note, string tag) {
        note.tag = tag;
        foreach (Transform child in note.transform) {
            child.gameObject.tag = tag;
        }
    }
    public void OnPause()
    {
        //not yet implemented
        //only toggles the music for now
        //assumes music is initially playing
        Paused = !Paused;
        if (Paused)
        {
            songSource.Pause();
        }
        else
        {
            songSource.UnPause();
        }
    }
    
}

[System.Serializable]
public class Note
{
    public float timestamp;
    public bool note1;
    public bool note2;
    public bool note3;
    public bool note4;

}

[System.Serializable]
public class NoteArray
{
    public Note[] notes;
}
