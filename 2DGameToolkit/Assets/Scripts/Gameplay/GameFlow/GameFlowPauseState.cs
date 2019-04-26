﻿using UnityEngine;

public class PauseEvent : GameEvent
{
    public PauseEvent(bool isPaused) : base("Game")
    {
        m_IsPaused = isPaused;
    }

    public bool IsPaused()
    {
        return m_IsPaused;
    }

    private bool m_IsPaused;
}

public class GameFlowPauseState : HSMState
{
    public override void OnEnter ()
    {
        UpdaterProxy.Get ().SetPause (true);
        Time.timeScale = 0;
        this.RegisterAsListener ("Game", typeof (GameFlowEvent));
        this.RegisterAsListener ("Player", typeof (PlayerInputGameEvent));
        new PauseEvent (true).Push ();
    }

    public void OnGameEvent (GameFlowEvent flowEvent)
    {
        switch (flowEvent.GetAction ())
        {
            case EGameFlowAction.Quit:
                ChangeNextTransition (HSMTransition.EType.Clear, typeof (GameFlowMenuState));
                break;
            case EGameFlowAction.Retry:
                ChangeNextTransition (HSMTransition.EType.Clear, typeof (GameFlowNormalState));
                break;
            case EGameFlowAction.Resume:
                ChangeNextTransition (HSMTransition.EType.Exit);
                break;
        }
    }

    public void OnGameEvent (PlayerInputGameEvent inputEvent)
    {
        if (inputEvent.GetInput () == "Pause" && inputEvent.GetInputState () == EInputState.Down)
        {
            ChangeNextTransition (HSMTransition.EType.Exit);
        }
    }

    public override void OnExit ()
    {
        new PauseEvent (false).Push ();
        this.UnregisterAsListener ("Player");
        this.UnregisterAsListener ("Game");
        Time.timeScale = 1;
        UpdaterProxy.Get ().SetPause (false);
    }
}