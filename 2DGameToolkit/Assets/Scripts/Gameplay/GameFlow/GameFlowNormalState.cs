
public class GameFlowNormalState : HSMState
{
    public override void OnEnter ()
    {
        LevelManagerProxy.Get ().LoadScene (1);
        this.RegisterAsListener ("Game", typeof (GameFlowEvent));
        this.RegisterAsListener ("Player", typeof (GameOverGameEvent), typeof (PlayerInputGameEvent));
    }

    public void OnGameEvent (GameOverGameEvent gameOver)
    {
        ChangeNextTransition (HSMTransition.EType.Siblings, typeof (GameFlowGameOverState));
    }

    public void OnGameEvent (PlayerInputGameEvent inputEvent)
    {
        if (inputEvent.GetInput () == "Pause" && inputEvent.GetInputState() == EInputState.Down && !UpdaterProxy.Get().IsPaused())
        {
            ChangeNextTransition (HSMTransition.EType.Child, typeof (GameFlowPauseState));
        }
    }
    public void OnGameEvent (GameFlowEvent flowEvent)
    {
        switch (flowEvent.GetAction ())
        {
            case EGameFlowAction.StartDialogue:
                ChangeNextTransition (HSMTransition.EType.Child, typeof (GameFlowDialogueState));
                break;
        }
    }

    public override void OnExit ()
    {
        this.UnregisterAsListener ("Player");
        this.UnregisterAsListener ("Game");
    }
}