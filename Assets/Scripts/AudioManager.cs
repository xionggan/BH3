using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public AudioClip[] audios;

    public bool loopLast;

    private AudioSource audioSource;

    private int curPlayIndex;

	void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
	}

    public void PlayAllAudiosByOrder()
    {
        StartCoroutine("PlayOneByOne");
    }

    public void Play(int index,bool isLoop)
    {
        StopCoroutine("PlayOneByOne");
        audioSource.clip = audios[index];
        audioSource.loop = isLoop;
        audioSource.Play();
    }

    public void Stop()
    {
        StopCoroutine("PlayOneByOne");
        audioSource.Stop();
    }

    IEnumerator PlayOneByOne()
    {
        while(true)
        {
            if (!audioSource.isPlaying)
            {
                if (curPlayIndex < audios.Length)
                {
                    audioSource.clip = audios[curPlayIndex];
                    audioSource.Play();
                    if (curPlayIndex == audios.Length - 1 && loopLast)
                    {
                        audioSource.loop = true;
                    }
                    curPlayIndex++;
                }
                else 
                {
                    break;
                }
            }
            yield return null;
        }
    }
}
