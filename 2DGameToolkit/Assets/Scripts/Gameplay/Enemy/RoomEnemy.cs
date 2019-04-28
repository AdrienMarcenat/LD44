using UnityEngine;
using System.Collections;

public class RoomEnemy : MonoBehaviour
{
    [SerializeField] GameObject m_RoomEnemyPrefab;
    private GameObject m_EnemyInstance;

    public void ResetEnemies()
    {
        if(m_EnemyInstance != null)
        {
            Destroy(m_EnemyInstance);
        }
        m_EnemyInstance = Instantiate(m_RoomEnemyPrefab, transform);
    }
}
