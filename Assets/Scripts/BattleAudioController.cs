using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAudioController : MonoBehaviour
{
    private AudioSource audio;

    public AudioClip battleMusic;
    public AudioClip victoryMusic;
    public AudioClip UIClick;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Play(0);
    }
}
