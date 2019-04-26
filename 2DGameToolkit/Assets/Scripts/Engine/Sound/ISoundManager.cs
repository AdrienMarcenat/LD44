using UnityEngine;

public interface ISoundManager
{
    void PlaySingle(AudioClip clip);
    void PlayMultiple(AudioClip clip);
    void PlayMusic(AudioClip clip);
}

public class SoundManagerProxy : UniqueProxy<ISoundManager>
{ }