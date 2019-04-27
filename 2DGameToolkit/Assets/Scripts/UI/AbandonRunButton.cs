using UnityEngine;

public class AbandonRunButton : MonoBehaviour
{
    public void AbandonRun()
    {
        PlayerManagerProxy.Get().ResetStat();
        new GameFlowEvent(EGameFlowAction.Quit).Push();
    }
}

