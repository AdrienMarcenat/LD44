using System.Collections;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public static float m_CurrentMinX;
    public static float m_CurrentMaxX;

    [SerializeField] RoomInfo m_ExitRoom;
    [SerializeField] RoomInfo m_EnterRoom;

    [SerializeField] Transform m_PlayerNode;
    [SerializeField] float m_TransitionTime = 0.5f;

    private Transform m_PlayerTransform;
    private MovingObject m_PlayerMovement;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_PlayerTransform = player.transform;
        m_PlayerMovement = m_PlayerTransform.GetComponent<MovingObject>();
        RoomInfo firstRoom = GameObject.FindGameObjectWithTag("FirstRoom").GetComponent<RoomInfo>();
        m_CurrentMinX = firstRoom.GetMinX();
        m_CurrentMaxX = firstRoom.GetMaxX();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(TransitionRoutine());
        }
    }

    private IEnumerator TransitionRoutine()
    {
        new GameFlowEvent(EGameFlowAction.StartTransition).Push();
        // Teleport the player to the nex room
        m_PlayerTransform.position = new Vector3(m_PlayerNode.position.x, m_PlayerTransform.position.y, m_PlayerTransform.position.z);
        m_PlayerMovement.Freeze();

        yield return new WaitForSecondsRealtime(m_TransitionTime);

        m_CurrentMinX = m_EnterRoom.GetMinX();
        m_CurrentMaxX = m_EnterRoom.GetMaxX();
        m_EnterRoom.gameObject.SetActive(true);
        RoomEnemy enemies =m_EnterRoom.GetComponent<RoomEnemy>();
        if(enemies != null)
        {
            enemies.ResetEnemies();
        }
        m_ExitRoom.gameObject.SetActive(false);
        m_PlayerMovement.Unfreeze();
        new GameFlowEvent(EGameFlowAction.EndTransition).Push();
    }
}