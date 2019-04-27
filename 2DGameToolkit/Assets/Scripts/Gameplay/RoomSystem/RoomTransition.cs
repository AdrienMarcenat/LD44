using System.Collections;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    public static Transform m_CurrentRoom;

    [SerializeField] GameObject m_ExitRoom;
    [SerializeField] GameObject m_EnterRoom;

    [SerializeField] Transform m_PlayerNode;
    [SerializeField] float m_TransitionTime = 0.5f;

    private Transform m_PlayerTransform;
    private MovingObject m_PlayerMovement;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_PlayerTransform = player.transform;
        m_PlayerMovement = m_PlayerTransform.GetComponent<MovingObject>();
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

        m_CurrentRoom = m_EnterRoom.transform;
        m_EnterRoom.SetActive(true);
        m_ExitRoom.SetActive(false);
        m_PlayerMovement.Unfreeze();
        new GameFlowEvent(EGameFlowAction.EndTransition).Push();
    }
}