using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip audioClip;

    public bool loop;

    [Range(0, 2f)]
    public float pitch;

    [Range(.5f, 5f)]
    public float volume;

    [HideInInspector]
    public AudioSource audioSource;

    public AudioMixerGroup audioMixerGroup;


}
