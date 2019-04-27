using UnityEngine;

public class EnemyPath : Enemy
{
    [SerializeField] private BezierCurve m_Path;
    [SerializeField] private float m_Duration;
    [SerializeField] private bool m_Loop;
    private int m_Direction = 1;

    private float m_Progress;

    private void Update ()
    {
        m_Progress += m_Direction* (Time.deltaTime / m_Duration);
        if ((m_Direction == 1 && m_Progress > 1f) || (m_Direction == -1 && m_Progress < 0f))
        {
            if (m_Loop)
            {
                m_Direction *= -1;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        Vector2 position = m_Path.GetPoint (m_Progress);
        transform.position = position;
    }

    public void SetPath (BezierCurve path)
    {
        m_Path = path;
    }

    public void SetDuration (float duration)
    {
        m_Duration = duration;
    }
}

