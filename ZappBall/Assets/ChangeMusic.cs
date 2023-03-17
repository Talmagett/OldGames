using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ChangeMusic : MonoBehaviour
{
    public AudioClip[] otherClip;
    public AudioClip WinClip;
    private int _orderIndex;
    private AudioSource audioS;

    private void Awake()
    {
        _orderIndex = Random.Range(0,otherClip.Length);
    }
    IEnumerator Start()
    {
        audioS = GetComponent<AudioSource>();
        audioS.clip = otherClip[_orderIndex];
        audioS.Play();
        yield return new WaitForSeconds(audioS.clip.length);
        if (_orderIndex < otherClip.Length-1)
            _orderIndex++;
        else _orderIndex = 0;
        StartCoroutine(Start());
    }

    public void WinMusic() {
        audioS.volume = 0.2f;
    }
}
