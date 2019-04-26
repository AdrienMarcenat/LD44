using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private void Awake ()
    {
        gameObject.SetActive (false);
        this.RegisterAsListener ("Game", typeof (PauseEvent));
    }

    public void OnGameEvent (PauseEvent pause)
    {
        gameObject.SetActive (pause.IsPaused ());
    }

    private void OnDestroy ()
    {
        this.UnregisterAsListener ("Game");
    }
}

