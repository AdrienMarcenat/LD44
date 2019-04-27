using UnityEngine;

public class SaveAndQuitButton : MonoBehaviour
{
    public void SaveAndQuit()
    {
        PlayerManagerProxy.Get().SaveStat();
        new GameFlowEvent(EGameFlowAction.Quit).Push();
    }
}

