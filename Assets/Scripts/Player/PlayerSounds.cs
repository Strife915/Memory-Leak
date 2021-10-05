using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public static PlayerSounds instance;
    public AudioSource playerAudioSource;
    public AudioClip bounceSound, fallSound, levelCompleteSound;

    private void Start()
    {
        instance = this;
        playerAudioSource = GetComponent<AudioSource>();
    }

    public void PlayBounceSound()
    {
        playerAudioSource.PlayOneShot(bounceSound);
    }
    public void PlayFallSound()
    {
        playerAudioSource.PlayOneShot(fallSound);
    }
    public void PlayLevelComplete()
    {
        playerAudioSource.PlayOneShot(levelCompleteSound);
    }
}
