using UnityEngine;
using System.Collections;

public class PlayerInvicibleState : HSMState
{
    private float m_InvulnerabilitySeconds = 1f;
    private float m_BlinkingRate = 0.1f;

    private Health m_Health;
    private SpriteRenderer m_Sprite;

    private float m_InvulnerabilitySecondsDelay;
    private float m_Timer;
    
    public override void OnEnter ()
    {
        GameObject player = PlayerManagerProxy.Get().GetPlayer();
        m_Health = player.GetComponent<Health>();
        m_Sprite = player.GetComponent<SpriteRenderer>();
        m_Health.Enable (false);
        m_InvulnerabilitySecondsDelay = m_InvulnerabilitySeconds;
        m_Sprite.enabled = false;
    }

    public override void OnExit ()
    {
        m_Sprite.enabled = true;
        m_Health.Enable (true);
    }

    public override bool OnUpdate()
    {
        m_Timer += Time.deltaTime;
        if (m_Timer > m_BlinkingRate)
        {
            m_Timer = 0;
            if (m_InvulnerabilitySecondsDelay > 0)
            {
                m_InvulnerabilitySecondsDelay -= Time.deltaTime + m_BlinkingRate;
                m_Sprite.enabled = !m_Sprite.enabled;
            }
            else
            {
                ChangeNextTransition(HSMTransition.EType.Exit);
            }
        }
        return false;
    }
}

