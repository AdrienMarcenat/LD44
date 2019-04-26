using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent (typeof (AudioLowPassFilter))]
public class SoundFilterOnEvent : MonoBehaviour
{
    [SerializeField] private float m_TimeInSeconds = 0.5f;
    private AudioLowPassFilter m_AudioLowPassFilter;

    void Awake ()
    {
        m_AudioLowPassFilter = GetComponent<AudioLowPassFilter> ();
        this.RegisterAsListener ("Player", typeof (DamageGameEvent));
    }

    void OnDestroy ()
    {
        this.UnregisterAsListener ("Player");
    }

    public void OnGameEvent (DamageGameEvent damageGameEvent)
    {
        StopAllCoroutines ();
        StartCoroutine (LowPassRoutine());
    }

    private IEnumerator LowPassRoutine()
    {
        m_AudioLowPassFilter.cutoffFrequency = 1000;
        yield return new WaitForSecondsRealtime(m_TimeInSeconds);
        m_AudioLowPassFilter.cutoffFrequency = 5000;
    }
}
