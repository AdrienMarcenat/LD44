using UnityEngine;

public class RoomSystem : MonoBehaviour
{
    public static Transform m_CurrentRoom;

    [SerializeField] GameObject m_ExitRoom;
    [SerializeField] GameObject m_EnterRoom;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            m_CurrentRoom = m_EnterRoom.transform;
            m_EnterRoom.SetActive(true);
            m_ExitRoom.SetActive(false);
        }
    }
}