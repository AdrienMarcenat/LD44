using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    [SerializeField] Transform m_MinX;
    [SerializeField] Transform m_MaxX;

    public float GetMinX()
    {
        return m_MinX.position.x; ;
    }

    public float GetMaxX()
    {
        return m_MaxX.position.x;
    }
}
