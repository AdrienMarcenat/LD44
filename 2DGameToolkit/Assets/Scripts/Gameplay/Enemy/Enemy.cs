using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected int m_Type;
    [SerializeField] protected int m_PlayerDamageOnCollision;
    [SerializeField] private AudioClip m_Sound;
    [SerializeField] private float m_HitColorTime;

    protected Health m_Health;
    protected Animator m_Animator;
    protected SpriteRenderer m_Sprite;
    protected Color m_InitialColor;
    protected bool m_IsDying = false;

    protected void Awake ()
    {
        m_Health = GetComponent<Health> ();
        m_Sprite = GetComponent<SpriteRenderer> ();
        m_Animator = GetComponent<Animator>();
        m_InitialColor = m_Sprite.color;

        this.RegisterAsListener (gameObject.name, typeof (GameOverGameEvent), typeof (DamageGameEvent));
    }

    IEnumerator HitRoutine (float damage)
    {
        m_Sprite.color = Color.Lerp (Color.blue, Color.red, damage / 100);
        yield return new WaitForSeconds (m_HitColorTime);
        m_Sprite.color = m_InitialColor;
    }

    public void OnGameEvent (DamageGameEvent damageEvent)
    {
        StartCoroutine (HitRoutine (damageEvent.GetDamage ()));
    }

    public void OnGameEvent (GameOverGameEvent gameOverEvent)
    {
        m_IsDying = true;
        m_Animator.SetTrigger("death");
        OnGameOver();
        // Remove collision to avoid hurting player or being hurt
        GetComponent<BoxCollider2D> ().enabled = false;
        // Let some time for the dying animation to be played
        Destroy (gameObject, 1);
    }

    protected void OnDestroy ()
    {
        // Call the cleanup code if it has not been done
        if (!m_IsDying)
        {
            OnGameOver();
        }
        this.UnregisterAsListener (gameObject.name);
    }

    private void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Health playerHealth = other.gameObject.GetComponent<Health> ();
            playerHealth.LoseHealth (m_PlayerDamageOnCollision);
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if(playerController != null)
            {
                playerController.KnockBack(transform);
            }
            OnPlayerCollision();
        }
    }

    protected virtual void OnPlayerCollision() { }
    protected virtual void OnGameOver() { }

    public bool IsDying()
    {
        return m_IsDying;
    }
}