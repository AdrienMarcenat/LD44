public class GameFlowDialogueState : HSMState
{
    public override void OnEnter ()
    {
        UpdaterProxy.Get ().SetPause (true);
        this.RegisterAsListener ("Game", typeof (GameFlowEvent));
    }

    public void OnGameEvent (GameFlowEvent flowEvent)
    {
        switch (flowEvent.GetAction ())
        {
            case EGameFlowAction.EndDialogue:
                ChangeNextTransition (HSMTransition.EType.Exit);
                break;
            case EGameFlowAction.Quit:
                ChangeNextTransition(HSMTransition.EType.Clear, typeof(GameFlowMenuState));
                break;
        }
    }

    public override void OnExit ()
    {
        this.UnregisterAsListener ("Game");
        UpdaterProxy.Get ().SetPause (false);
    }
}