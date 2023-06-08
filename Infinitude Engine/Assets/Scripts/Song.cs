using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Song", menuName = "Song")]
public class Song : ScriptableObject
{
    public string SongDisplayName;
    public string SongID;

    public AudioClip Instrumental;
    public AudioClip Voices;

    public float InstrumentalVolume = 0.6f;
    public float VoicesVolume = 1f;

    public Chart chart;
    [HideInInspector]
    public AudioSource InstrumentalSource;
    [HideInInspector]
    public AudioSource VoicesSource;
}