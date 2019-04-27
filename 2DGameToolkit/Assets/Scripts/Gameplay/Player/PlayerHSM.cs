
public class PlayerHSM : HSM
{
    public PlayerHSM ()
        : base (new PlayerNormalState ()
              , new PlayerInvicibleState ()
              , new PlayerGameOverState()
        )
    {}

    public void StartFlow()
    {
        Start(typeof(PlayerNormalState));
        this.RegisterToUpdate(false, EUpdatePass.Last);
    }

    public void StopFlow()
    {
        this.UnregisterToUpdate(EUpdatePass.Last);
        Stop();
    }
}

