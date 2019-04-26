using UnityEngine;

public class World : MonoBehaviour
{
    [SerializeField] private AudioSource m_EfxSource;
    [SerializeField] private AudioSource m_MusicSource;

    private UnityLogger m_Logger;
    private Updater m_Updater;
    private GameEventManager m_GameEventManager;
    private InputManager m_InputManager;
    private LevelManager m_LevelManager;
    private SoundManager m_SoundManager;

    private GameFlowHSM m_GameFlowHSM;

    private static World ms_Instance;

    // This should be called before any other gameobject awakes
    private void Awake ()
    {
        // Singleton pattern : this is the only case where it should be used
        if(ms_Instance == null)
        {
            ms_Instance = this;
            DontDestroyOnLoad (gameObject);

            m_Logger = new UnityLogger();
            m_Updater = new Updater();
            m_GameEventManager = new GameEventManager();
            m_InputManager = new InputManager();
            m_LevelManager = new LevelManager();
            m_GameFlowHSM = new GameFlowHSM();
            m_SoundManager = new SoundManager (m_EfxSource, m_MusicSource);
            OpenProxies ();
            OnEngineStart();
        }
        else if (ms_Instance != this)
        {
            Destroy (gameObject);
            return;
        }
    }

    private void Shutdown()
    {
        if (ms_Instance == this)
        {
            OnEngineStop();
            CloseProxies();
        }
    }

    void OpenProxies()
    {
        LoggerProxy.Open(m_Logger);
        UpdaterProxy.Open(m_Updater);
        GameEventManagerProxy.Open(m_GameEventManager);
        InputManagerProxy.Open(m_InputManager);
        LevelManagerProxy.Open(m_LevelManager);
        SoundManagerProxy.Open (m_SoundManager);
    }

    void CloseProxies()
    {
        SoundManagerProxy.Close (m_SoundManager);
        LevelManagerProxy.Close (m_LevelManager);
        InputManagerProxy.Close(m_InputManager);
        GameEventManagerProxy.Close (m_GameEventManager);
        UpdaterProxy.Close (m_Updater);
        LoggerProxy.Close (m_Logger);
    }

    void OnEngineStart()
    {
        m_GameFlowHSM.StartFlow();
        m_InputManager.OnEngineStart();
        m_GameEventManager.OnEngineStart();
    }

    void OnEngineStop()
    {
        m_GameEventManager.OnEngineStop();
        m_InputManager.OnEngineStop();
        m_GameFlowHSM.StopFlow();
    }

    void Update ()
    {
        m_Updater.Update ();
    }
}
