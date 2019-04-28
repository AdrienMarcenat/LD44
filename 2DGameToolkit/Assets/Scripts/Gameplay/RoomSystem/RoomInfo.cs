using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    private Transform m_MinX;
    private Transform m_MaxX;
    private Transform m_MinY;
    private Transform m_MaxY;

    private void Awake()
    {
        m_MinX = transform.Find("MinX");
        m_MaxX = transform.Find("MaxX");
        m_MinY = transform.Find("MinY");
        m_MaxY = transform.Find("MaxY");
        if(tag != "FirstRoom")
        {
            gameObject.SetActive(false);
        }
    }

    public float GetMinX()
    {
        return m_MinX.position.x;
    }

    public float GetMaxX()
    {
        return m_MaxX.position.x;
    }
    public float GetMinY()
    {
        return m_MinY.position.y;
    }

    public float GetMaxY()
    {
        return m_MaxY.position.y;
    }
}
