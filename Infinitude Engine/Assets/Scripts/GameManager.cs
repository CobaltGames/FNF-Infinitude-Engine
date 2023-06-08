using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public KeyCode[] Inputs = { KeyCode.S, KeyCode.D, KeyCode.K, KeyCode.L };
    public Song song;

    public Sprite[] Arrows;
    public Sprite[] Notes;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        if (song != null)
        {
            song.InstrumentalSource = gameObject.AddComponent<AudioSource>();
            song.VoicesSource = gameObject.AddComponent<AudioSource>();

            song.InstrumentalSource.clip = song.Instrumental;
            song.VoicesSource.clip = song.Voices;

            song.InstrumentalSource.volume = song.InstrumentalVolume;
            song.VoicesSource.volume = song.VoicesVolume;

            song.InstrumentalSource.loop = false;
            song.VoicesSource.loop = false;

            song.InstrumentalSource.playOnAwake = false;
            song.VoicesSource.playOnAwake = false;
        }
        else
        {
            Debug.Log("No Song in GameManager");
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Play();
    }

    public void Play ()
    {
        song.InstrumentalSource.clip.LoadAudioData();
        song.VoicesSource.clip.LoadAudioData();
        song.InstrumentalSource.Play();
        song.VoicesSource.Play();

        foreach (Note note in song.chart.notes)
        {
            if (note.holdLength <= 0)
            {
                GameObject newNote = new GameObject("Note");
                RectTransform NoteTransform = newNote.AddComponent<RectTransform>();
                Image image = newNote.AddComponent<Image>();
                BoxCollider2D collider = newNote.AddComponent<BoxCollider2D>();
                Rigidbody2D rigidBody = newNote.AddComponent<Rigidbody2D>();

                NoteTransform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
                NoteTransform.anchorMin = new Vector2(0f, 1f);
                NoteTransform.anchorMax = new Vector2(0f, 1f);

                image.sprite = Notes[note.ID];
                
                switch(note.ID)
                {
                    case 0:
                        NoteTransform.localPosition = new Vector3(548, (float)(-0.45 * (song.InstrumentalSource.time * 1000 - note.TimePos) * song.chart.ScrollSpeed), 0f);
                        break;
                }

                Instantiate(newNote, transform);
            }
        }
    }
}