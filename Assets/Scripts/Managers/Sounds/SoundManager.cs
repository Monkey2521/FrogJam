using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [Header("Debug settings")]
    [SerializeField] protected bool _isDebug;

    [Header("Settings")]
    [SerializeField] protected AudioSource _audioSource;
    [SerializeField] protected List<Sound> _sounds = new List<Sound>();

    protected bool _isPlaying;

    protected void Awake()
    {
        GetReferences();
    }

    protected void GetReferences ()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public bool PlaySound (SoundTypes type) 
    {
        if (FindClip(type, out AudioClip clip))
        {
            _audioSource.clip = clip;
            _audioSource.Play();

            if (_isDebug) Debug.Log("Playing '" + type.ToString() + "' sound.");

            return true;
        }

        if (_isDebug) Debug.Log("Sound '" + type.ToString() + "' not found!");

        return false;
    }

    protected bool FindClip(SoundTypes type, out AudioClip clip)
    {
        clip = null;

        foreach (Sound sound in _sounds)
        {
            if (sound.SoundType == type) {
                clip = sound.Clip;
                return true;
            }
        }

        return false;
    }
}
