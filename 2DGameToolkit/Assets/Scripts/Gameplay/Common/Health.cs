using UnityEngine;

public class DamageGameEvent : GameEvent
{
    public DamageGameEvent(string tag, float damage) : base(tag)
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
    private bool m_Enable;

	protected void Start ()
	{
		m_CurrentHealth = m_MaxHealth;
        m_DamageModifier = 1;
        m_Enable = true;
	}

    public void Heal (int value)
    {
        if (!m_Enable)
        {
            return;
        }
        m_CurrentHealth = System.Math.Max(m_MaxHealth, m_CurrentHealth + value);
    }

    public void LoseHealth (int damage)
	{
        if (!m_Enable)
        {
            return;
        }

		m_CurrentHealth = System.Math.Max (0, m_CurrentHealth - m_DamageModifier * damage);
        new DamageGameEvent (gameObject.name, damage).Push();

		CheckIfGameOver ();
	}

	public int GetCurrentHealth ()
	{
		return m_CurrentHealth;
	}

    public void SetMaxHealth (int value)
    {
        m_MaxHealth = value;
    }

    public float GetTotalHealth ()
	{
		return m_MaxHealth;
	}

	private void CheckIfGameOver ()
	{
		if (m_CurrentHealth <= 0)
		{
			new GameOverGameEvent (gameObject.name).Push ();
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
}

