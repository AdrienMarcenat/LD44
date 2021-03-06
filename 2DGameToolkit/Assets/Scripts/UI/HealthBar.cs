﻿using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health m_Health;
    private float m_InitialXScale;

    private void OnEnable ()
    {
        m_InitialXScale = transform.localScale.x;
        this.RegisterAsListener (transform.parent.gameObject.GetInstanceID().ToString(), typeof (DamageGameEvent));
    }

    private void OnDisable ()
    {
        this.UnregisterAsListener (transform.parent.gameObject.GetInstanceID().ToString());
    }

    public void OnGameEvent (DamageGameEvent damageEvent)
    {
        float fraction = Mathf.Clamp01 (m_Health.GetCurrentHealth () / m_Health.GetTotalHealth ());
        transform.localScale = new Vector3 (fraction * m_InitialXScale, transform.localScale.y, transform.localScale.z);
    }
}
