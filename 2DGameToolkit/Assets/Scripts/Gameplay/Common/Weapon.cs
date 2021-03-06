﻿using UnityEngine;
using System.Collections.Generic;

public class FireCommand
{
    public float m_NumberOfShots;
    public float m_SizeModifier;
    public Vector3 m_Target;

    public FireCommand (float numberOfShots, float sizeModifier, Vector3 target)
    {
        m_NumberOfShots = numberOfShots;
        m_SizeModifier = sizeModifier;
        m_Target = target;
    }

    public void DecreaseNumberOfShots ()
    {
        m_NumberOfShots--;
    }
}

public class Weapon : MonoBehaviour
{
    [SerializeField] string m_Type;
    [SerializeField] int m_TotalAmmo = int.MaxValue;
    [SerializeField] int m_CurrentAmmo;
    [SerializeField] float m_AmmoVelocity;
    [SerializeField] float m_FireRate;
    [SerializeField] AudioClip m_FireSound;


    [SerializeField] GameObject m_BulletPrefab;

    private Queue<FireCommand> m_FireCommands;
    private float m_FireDelay;
    private float m_FireCommandNumber;
    private bool m_IsRegistered = false;

    void Awake ()
    {
        m_FireCommands = new Queue<FireCommand> ();
        m_CurrentAmmo = m_TotalAmmo;
        m_FireDelay = m_FireRate;
        this.RegisterToUpdate(EUpdatePass.AI);
        this.RegisterAsListener(transform.parent.gameObject.GetInstanceID().ToString(), typeof(GameOverGameEvent));
        m_IsRegistered = true;
    }

    public void OnGameEvent(GameOverGameEvent gameOverEvent)
    {
        ClearFireCommands();
        this.UnregisterToUpdate(EUpdatePass.AI);
        m_IsRegistered = false;
    }

    private void OnDestroy()
    {
        ClearFireCommands();
        this.UnregisterAsListener(transform.parent.gameObject.GetInstanceID().ToString());
        if(m_IsRegistered)
        {
            this.UnregisterToUpdate(EUpdatePass.AI);
            m_IsRegistered = false;
        }
    }

    public void UpdateAI ()
    {
        if (m_FireDelay < m_FireRate)
        {
            m_FireDelay += Time.deltaTime;
        }
        else if (m_FireCommands.Count > 0)
        {
            ProcessFireCommand ();
        }
    }

    public void SetAmmo (int amount)
    {
        m_CurrentAmmo = Mathf.Clamp (m_CurrentAmmo + amount, 0, m_TotalAmmo);
    }

    public void AddFireCommand (float numberOfShots, float sizeModifier, Vector3 target)
    {
        m_FireCommands.Enqueue (new FireCommand(numberOfShots, sizeModifier, target));
    }

    public bool Fire (Vector3 fireDirection, float sizeModifier)
    {
        if (m_CurrentAmmo == 0 || m_FireDelay < m_FireRate)
        {
            return false;
        }

        m_FireDelay = 0;
        SetAmmo (-1);

        GameObject bullet = Instantiate (m_BulletPrefab);
        if (bullet.GetComponent<Bullet> ().IsFollowingShooter ())
        {
            bullet.transform.SetParent (transform, false);
        }
        else
        {
            bullet.transform.position = transform.position;
            bullet.GetComponent<SpriteRenderer>().flipX = fireDirection.x < 0;
        }
        bullet.transform.localScale *= sizeModifier;
        bullet.GetComponent<Rigidbody2D> ().velocity = m_AmmoVelocity * fireDirection.normalized;
        SoundManagerProxy.Get().PlayMultiple(m_FireSound);
        return true;
    }

    private void ProcessFireCommand ()
    {
        FireCommand fireCommand = m_FireCommands.Peek ();
        Vector3 fireDirection = fireCommand.m_Target.normalized;

        if (Fire (fireDirection, fireCommand.m_SizeModifier))
        {
            fireCommand.DecreaseNumberOfShots ();
            if (fireCommand.m_NumberOfShots == 0)
            {
                m_FireCommands.Dequeue ();
            }
        }
    }

    public void Reload ()
    {
        m_CurrentAmmo = m_TotalAmmo;
    }

    public string GetWeaponType ()
    {
        return m_Type;
    }

    public int GetAmmo ()
    {
        return m_CurrentAmmo;
    }

    public void ClearFireCommands()
    {
        m_FireCommands.Clear();
    }
}
