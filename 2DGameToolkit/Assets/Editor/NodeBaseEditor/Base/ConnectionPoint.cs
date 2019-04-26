using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public enum EConnectionPointType
{
    In,
    Out
}

public class ConnectionPoint
{
    public string m_Id;

    private Rect m_Rect;
    private readonly EConnectionPointType m_Type;
    private readonly Node m_Node;
    private readonly GUIStyle m_Style;
    private readonly Action<ConnectionPoint> m_OnClickConnectionPoint;
    private readonly bool m_IsMultipleConnectionAllowed;
    private float m_Offset;
    private List<Connection> m_Connections = new List<Connection> ();
    private bool m_IsBeingRemoved = false;

    // parameterless constructor for xml serialization
    public ConnectionPoint() { }

    public ConnectionPoint(Node node, EConnectionPointType type
        , GUIStyle style, Action<ConnectionPoint> OnClickConnectionPoint
        , float offset, bool isMultipleConnectionAllowed, string id = null)
    {
        m_Node = node;
        m_Type = type;
        m_Style = style;
        m_OnClickConnectionPoint = OnClickConnectionPoint;
        m_Rect = new Rect(0, 0, 10f, 20f);
        m_IsMultipleConnectionAllowed = isMultipleConnectionAllowed;
        m_Offset = offset;

        m_Id = id ?? Guid.NewGuid().ToString();
    }

    public Rect GetRect()
    {
        return m_Rect;
    }

    public Node GetNode()
    {
        return m_Node;
    }

    public Vector2 GetGlobalCenter()
    {
        return m_Rect.center + m_Node.m_Rect.position;
    }

    public void Draw()
    {
        m_Rect.y = m_Offset - m_Rect.height * 0.5f;

        switch (m_Type)
        {
            case EConnectionPointType.In:
                m_Rect.x = - m_Rect.width + 8f;
                break;

            case EConnectionPointType.Out:
                m_Rect.x = m_Node.m_Rect.width - 8f;
                break;
        }

        if (GUI.Button(m_Rect, "", m_Style) && (m_IsMultipleConnectionAllowed || !HasConnection ()))
        {
            if (m_OnClickConnectionPoint != null)
            {
                m_OnClickConnectionPoint(this);
            }
        }
    }

    public bool HasConnection()
    {
        return m_Connections.Count != 0;
    }

    public virtual void OnConnectionMade(Connection connection)
    {
        m_Connections.Add (connection);
    }

    public virtual void OnConnectionRemove(Connection connection)
    {
        if (!m_IsBeingRemoved)
        {
            m_Connections.Remove (connection);
        }
    }

    public void SetOffset(float offset)
    {
        m_Offset = offset;
    }

    public float GetOffset ()
    {
        return m_Offset;
    }

    public void OnBeingRemoved()
    {
        m_IsBeingRemoved = true;
        foreach (Connection connection in m_Connections)
        {
            connection.OnConnectionPointRemoved ();
        }
    }
}