﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FadeImage : MonoBehaviour
{
    private Image m_FadeInOutImage;
    [SerializeField] float m_FadeSpeed;

    void OnEnable ()
    {
        m_FadeInOutImage = GetComponent<Image> ();
        this.RegisterAsListener ("Game", typeof (LevelEvent), typeof(GameFlowEvent));
        StartCoroutine (FadeIn ());
    }

    void OnDisable ()
    {
        this.UnregisterAsListener ("Game");
    }

    public void OnGameEvent (LevelEvent levelEvent)
    {
        if (levelEvent.IsEntered ())
        {
            StartCoroutine (FadeIn ());
        }
        else
        {
            StartCoroutine (FadeOut ());
        }
    }

    public void OnGameEvent(GameFlowEvent flowEvent)
    {
        StopAllCoroutines();
        if (flowEvent.GetAction() == EGameFlowAction.StartTransition)
        {
            BlackOut();
        }
        if (flowEvent.GetAction() == EGameFlowAction.EndTransition)
        {
            StartCoroutine(FadeIn());
        }
    }

    IEnumerator FadeIn ()
    {
        SetFadeInOutImageAlpha (1);
        yield return null;

        while (m_FadeInOutImage.color.a > 0)
        {
            AddToFadeInOutImageAlpha (-m_FadeSpeed);
            yield return null;
        }
    }

    IEnumerator FadeOut ()
    {
        SetFadeInOutImageAlpha (0);
        yield return null;

        while (m_FadeInOutImage.color.a < 1)
        {
            AddToFadeInOutImageAlpha (m_FadeSpeed);
            yield return null;
        }
    }

    private void AddToFadeInOutImageAlpha (float a)
    {
        Color c = m_FadeInOutImage.color;
        c.a += a;
        m_FadeInOutImage.color = c;
    }

    private void SetFadeInOutImageAlpha (float a)
    {
        Color c = m_FadeInOutImage.color;
        c.a = a;
        m_FadeInOutImage.color = c;
    }

    private void BlackOut()
    {
        Color c = m_FadeInOutImage.color;
        c.a = 1f;
        m_FadeInOutImage.color = c;
    }
}

