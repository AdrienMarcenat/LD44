
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            EnemySeeker enemy = GetComponentInParent<EnemySeeker>();
            if (enemy != null)
            {
                enemy.Seek(other.gameObject);
            }
            this.enabled = false;
        }
    }
}