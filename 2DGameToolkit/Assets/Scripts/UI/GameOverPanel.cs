using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] AudioClip m_GameOverMusic;

    private void Awake ()
    {
        gameObject.SetActive (false);
        this.RegisterAsListener ("Player", typeof (GameOverGameEvent));
    }

    public void OnGameEvent (GameOverGameEvent gameOver)
    {
        gameObject.SetActive (true);
        SoundManagerProxy.Get().PlayMusic(m_GameOverMusic);
    }

    private void OnDestroy ()
    {
        this.UnregisterAsListener ("Player");
    }
}
