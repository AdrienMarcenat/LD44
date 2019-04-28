
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
class Potion : MonoBehaviour
{
    [SerializeField] int m_Heal = 2;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Health playerHealth = other.gameObject.GetComponent<Health>();
            playerHealth.Heal(m_Heal);
            Destroy(gameObject);
        }
    }
}
