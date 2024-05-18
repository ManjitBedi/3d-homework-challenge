using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameAudio
{
    Pop,
    Throw,
    Rocket,
    Grow
}

/// <summary>
/// Audio manager to deal with most of the audio in the level.
/// </summary>
public class AudioManager : MonoBehaviour
{
    [SerializeField]
    AudioSource popAudioSource;

    [SerializeField]
    AudioSource throwAudioSource;

    [SerializeField]
    AudioSource growingAudioSource;

    [SerializeField]
    AudioSource rocketAudioSource;

    public void PlayAudio(GameAudio gameAudio)
    {
        switch (gameAudio)
        {
            case GameAudio.Pop:
                popAudioSource.Play();
                break;
            case GameAudio.Throw:
                throwAudioSource.Play();
                break;
            case GameAudio.Rocket:
                rocketAudioSource.Play();
                break;
            case GameAudio.Grow:
                growingAudioSource.Play();
                break;
        }
    } 
}
