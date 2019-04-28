
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private bool m_Detected = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !m_Detected)
        {
            m_Detected = true;
            EnemySeeker enemy = GetComponentInParent<EnemySeeker>();
            if (enemy != null)
            {
                enemy.Seek(other.gameObject);
            }
            this.enabled = false;
        }
    }
}