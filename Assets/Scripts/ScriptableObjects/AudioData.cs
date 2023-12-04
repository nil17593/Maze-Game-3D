using UnityEngine;
using System;

[CreateAssetMenu(fileName = "AudioData", menuName = "Audio/AudioData")]
public class AudioData : ScriptableObject
{
    public AudioSource soundEffect;
    public AudioSource soundMusic;
    public SoundType[] sounds;

    public SoundType GetSound(Sounds sound)
    {
        return Array.Find(sounds, i => i.soundType == sound);
    }
}
