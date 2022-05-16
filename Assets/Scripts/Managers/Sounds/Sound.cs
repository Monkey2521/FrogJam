using UnityEngine;

[System.Serializable]
public class Sound
{
    [SerializeField] private SoundTypes _soundType;
    public SoundTypes SoundType => _soundType;

    [SerializeField] private AudioClip _clip;
    public AudioClip Clip => _clip;
}
