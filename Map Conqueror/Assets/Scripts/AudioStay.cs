using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioStay : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        _audioSource = GetComponent<AudioSource>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if(objs.Length > 1)
        {
            Destroy(this.gameObject);
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void PlayMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}