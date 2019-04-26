using System.Collections;
using UnityEngine;

public class SlowMotionOnHit : MonoBehaviour
{
    [SerializeField, Range(0,1)] private float m_SlowMotionTimeScale = 0.1f;
    [SerializeField] private float m_SlowMotionDuration = 0.5f;

    void Awake ()
    {
        this.RegisterAsListener ("Player", typeof (DamageGameEvent), typeof (GameOverGameEvent));
        this.RegisterAsListener ("Game", typeof (PauseEvent));
    }

    void OnDestroy ()
    {
        this.UnregisterAsListener ("Game");
        this.UnregisterAsListener ("Player");
    }

    public void OnGameEvent (PauseEvent pauseEvent)
    {
        // Stop the slow motion to not interfere with the pause state, who will set timescale to 0
        StopAllCoroutines ();
    }

    public void OnGameEvent (GameOverGameEvent gameOverGameEvent)
    {
        // Stop the slow motion to not interfere with the gameover state, who will set timescale to 0
        StopAllCoroutines ();
    }

    public void OnGameEvent (DamageGameEvent damageGameEvent)
    {
        StopAllCoroutines ();
        StartCoroutine (SlowMotionRoutine ());
    }

    private IEnumerator SlowMotionRoutine ()
    {
        Time.timeScale = m_SlowMotionTimeScale;
        yield return new WaitForSecondsRealtime (m_SlowMotionDuration);
        Time.timeScale = 1;
    }
}
