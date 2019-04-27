using UnityEngine;

public class PlayerNormalState : HSMState
{
	private Health m_Health;
	private MovingObject m_Body;
	private WeaponManager m_WeaponManager;
    
	public override void OnEnter ()
	{
        this.RegisterAsListener ("Player", typeof (GameOverGameEvent), typeof (DamageGameEvent));
	}

	public override void OnExit ()
    {
        this.UnregisterAsListener ("Player");
    }

    public void OnGameEvent (GameOverGameEvent gameOverEvent)
	{
        ChangeNextTransition (HSMTransition.EType.Clear, typeof(PlayerGameOverState));
	}

	public void OnGameEvent (DamageGameEvent damageEvent)
    {
        if (damageEvent.GetDamage() > 0)
        {
            ChangeNextTransition(HSMTransition.EType.Child, typeof(PlayerInvicibleState));
        }
    }
}

