﻿using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject m_InputConfiguration;

    private void Awake ()
    {
        gameObject.SetActive (false);
        m_InputConfiguration.SetActive(false);
        this.RegisterAsListener ("Game", typeof (PauseEvent));
    }

    public void OnGameEvent (PauseEvent pause)
    {
        gameObject.SetActive (pause.IsPaused ());
        m_InputConfiguration.SetActive(false);
    }

    private void OnDestroy ()
    {
        this.UnregisterAsListener ("Game");
    }
}

