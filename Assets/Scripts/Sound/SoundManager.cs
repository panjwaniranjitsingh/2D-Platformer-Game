using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }

    [SerializeField] private AudioSource m_soundEffect,m_soundMusic;

    public SoundType[] Sounds;

    [SerializeField] private bool m_IsMute = false;
    [SerializeField] private float m_Volume = 1f;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetVolume(0.5f);
        PlayMusic(global::SoundsForEvents.Music);
    }

    public void Mute(bool status)
    {
        m_IsMute = status;
    }

    public void SetVolume(float volume)
    {
        m_Volume = volume;
        m_soundEffect.volume = 2*m_Volume;
        m_soundMusic.volume = m_Volume;
    }

    public void PlayMusic(SoundsForEvents sound)
    {
        if (m_IsMute)
            return;
        AudioClip clip = getSoundClip(sound);
        if (clip != null)
        {
            m_soundMusic.clip = clip;
            m_soundMusic.Play();
        }
        else
        {
            Debug.LogError("Clip not found for sound type: " + sound);
        }
    }

    public void Play(SoundsForEvents sound)
    {
        if (m_IsMute)
            return;
        AudioClip clip = getSoundClip(sound);
        if(clip != null)
        {
            m_soundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Clip not found for sound type: "+ sound);
        }
    }

    private AudioClip getSoundClip(SoundsForEvents sound)
    {
        SoundType item = Array.Find(Sounds, i => i.soundType == sound);
        if (item != null)
            return item.soundClip;
        return null;
    }
}

[Serializable]
public class SoundType
{
    public SoundsForEvents soundType;
    public AudioClip soundClip;
}

public enum SoundsForEvents
{
    ButtonClick,
    PlayerMove,
    Music,
    PlayerDeath,
    EnemyDeath,
    FinishLevel,
    NewLevel,
    Collectible,
}
