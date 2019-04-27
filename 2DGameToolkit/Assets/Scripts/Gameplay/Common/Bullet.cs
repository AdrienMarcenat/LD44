using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] protected int m_Damage;
	[SerializeField] private float m_Range;
	[SerializeField] private float m_Penetration;
	[SerializeField] private string m_TargetTag;
	[SerializeField] private bool m_FollowShooter;

	void Update ()
	{
        m_Range -= Time.deltaTime;
        if (m_Range < 0)
        {
            Destroy(gameObject);
        }
	}

	private void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == m_TargetTag)
		{
			Health targetHealth = other.GetComponent<Health> ();
			if (targetHealth)
			{
				targetHealth.LoseHealth (GetModifiedDamage());
			}

			Destroy (gameObject, m_Penetration);
		}
	}

	void OnDisable ()
	{
		Destroy (gameObject);
	}

	public bool IsFollowingShooter ()
	{
		return m_FollowShooter;
	}

    public virtual int GetModifiedDamage()
    {
        return m_Damage;
    }
}
