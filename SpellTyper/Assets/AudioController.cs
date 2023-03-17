using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    
    public AudioClip[] ClipToPlay;
    private AudioSource Source;
    void Awake()
    {
        Source = GameObject.Find("Audio").GetComponent<AudioSource>();
    }
    private void Start()
    {
        int Rand = Random.Range(0,ClipToPlay.Length);
        Source.PlayOneShot(ClipToPlay[Rand]);
    }
}
