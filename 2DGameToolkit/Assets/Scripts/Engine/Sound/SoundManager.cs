using UnityEngine;

public class SoundManager : ISoundManager
{
    private readonly AudioSource m_EfxSource;
    private readonly AudioSource m_MusicSource;

    public SoundManager (AudioSource efxSource, AudioSource musicSource)
    {
        m_EfxSource = efxSource;
        m_MusicSource = musicSource;
    }

    public void PlaySingle (AudioClip clip)
    {
        m_EfxSource.clip = clip;
        m_EfxSource.Play ();
    }

    public void PlayMultiple (AudioClip clip)
    {
       m_EfxSource.PlayOneShot (clip);
    }

    public void PlayMusic (AudioClip clip)
    {
        if (m_MusicSource.clip != clip)
        {
           m_MusicSource.clip = clip;
           m_MusicSource.Play ();
           m_MusicSource.loop = true;
        }
    }
}