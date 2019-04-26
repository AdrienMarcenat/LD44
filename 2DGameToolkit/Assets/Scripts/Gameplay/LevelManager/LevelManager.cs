using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEvent : GameEvent
{
    public LevelEvent(int levelIndex, bool enter) : base("Game", EProtocol.Instant)
    {
        m_LevelIndex = levelIndex;
        m_Enter = enter;
    }

    public int GetLevelIndex()
    {
        return m_LevelIndex;
    }

    public bool IsEntered ()
    {
        return m_Enter;
    }

    private int m_LevelIndex;
    private bool m_Enter;
}

public class LevelManager
{
    private int m_CurrentLevel = 0;

    public void LoadScene (int levelIndex)
    {
        new LevelEvent (m_CurrentLevel, false).Push ();
        m_CurrentLevel = levelIndex;
        SceneManager.LoadScene (levelIndex);
    }

    public void LoadScene (string levelName)
    {
        new LevelEvent (m_CurrentLevel, false).Push ();
        m_CurrentLevel = SceneManager.GetSceneByName(levelName).buildIndex;
        SceneManager.LoadScene (levelName);
    }

    public string GetActiveSceneName()
    {
        return SceneManager.GetActiveScene ().name;
    }

    private void OnSceneLoaded (Scene scene, LoadSceneMode mode)
    {
        new LevelEvent (scene.buildIndex, true).Push ();
    }
}

public class LevelManagerProxy : UniqueProxy<LevelManager>
{ }
