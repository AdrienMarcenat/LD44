﻿using UnityEngine;

public class DamageGameEvent : GameEvent
{
    public DamageGameEvent(string tag, float damage) : base(tag, EProtocol.Instant)
    {
        m_Damage = damage;
    }

    public float GetDamage()
    {
        return m_Damage;
    }

    private float m_Damage;
}

public class GameOverGameEvent : GameEvent
{
    public GameOverGameEvent (string tag) : base (tag)
    {
    }
}

public class Health : MonoBehaviour
{
	[SerializeField] protected int m_MaxHealth;
	protected int m_CurrentHealth;
    private int m_DamageModifier;
    private bool m_Enable = true;
    protected string m_EventTag;

    protected virtual string GetEventTag() { return gameObject.GetInstanceID().ToString(); }

	protected void Start ()
	{
        m_EventTag = GetEventTag();

        m_CurrentHealth = m_MaxHealth;
        m_DamageModifier = 1;
	}

    public void Heal (int value)
    {
        if (!m_Enable)
        {
            return;
        }
        int newHealth = System.Math.Min(m_MaxHealth, m_CurrentHealth + value);
        int heal = newHealth - m_CurrentHealth;
        m_CurrentHealth = newHealth;
        PushDamageEvent(-heal);
    }

    public void FullHeal()
    {
        if (!m_Enable)
        {
            return;
        }
        int heal = m_MaxHealth - m_CurrentHealth;
        m_CurrentHealth = m_MaxHealth;
        PushDamageEvent(-heal);
    }

    public void LoseHealth (int damage)
	{
        if (!m_Enable)
        {
            return;
        }

		m_CurrentHealth = System.Math.Max (0, m_CurrentHealth - m_DamageModifier * damage);
        PushDamageEvent(damage);

		CheckIfGameOver ();
	}

    public void LoseMaxHealth(int newMaxHealth)
    {
        if (!m_Enable)
        {
            return;
        }

        int damage = m_MaxHealth - newMaxHealth;
        m_MaxHealth = System.Math.Max(0, newMaxHealth);
        m_CurrentHealth = System.Math.Min(0, m_CurrentHealth - m_DamageModifier * damage);
        PushDamageEvent(damage);

        CheckIfGameOver();
    }

    public int GetCurrentHealth ()
	{
		return m_CurrentHealth;
	}

    public int GetMaxHealth()
    {
        return m_MaxHealth;
    }

    public void SetMaxHealth (int value)
    {
        m_MaxHealth = value;
        FullHeal();
        CheckIfGameOver();
    }

    public float GetTotalHealth ()
	{
		return m_MaxHealth;
	}

	private void CheckIfGameOver ()
	{
		if (m_CurrentHealth <= 0)
		{
			new GameOverGameEvent (m_EventTag).Push ();
		}
	}

	public void Enable (bool enable)
	{
		m_Enable = enable;
	}

	public void SetDamageModifier (int modifier)
	{
		m_DamageModifier = modifier;
	}

    private void PushDamageEvent(int damage)
    {
        if (m_EventTag != null)
        {
            new DamageGameEvent(m_EventTag, damage).Push();
        }
    }
}

