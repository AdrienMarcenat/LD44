using System;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class Connection
{
    public ConnectionPoint m_InPoint;
    public ConnectionPoint m_OutPoint;
    private readonly Action<Connection> m_OnClickRemoveConnection;

    // parameterless constructo for xml serialization
    public Connection() { }

    public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint, Action<Connection> OnClickRemoveConnection)
    {
        m_InPoint = inPoint;
        m_OutPoint = outPoint;
        m_OnClickRemoveConnection = OnClickRemoveConnection;
    }

    public void Draw()
    {
        Handles.DrawBezier(
            m_InPoint.GetGlobalCenter(),
            m_OutPoint.GetGlobalCenter(),
            m_InPoint.GetGlobalCenter() + Vector2.left * 50f,
            m_OutPoint.GetGlobalCenter() - Vector2.left * 50f,
            Color.white,
            null,
            2f
        );

        if (Handles.Button((m_InPoint.GetGlobalCenter() + m_OutPoint.GetGlobalCenter()) * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleHandleCap))
        {
            if (m_OnClickRemoveConnection != null)
            {
                m_OnClickRemoveConnection(this);
            }
        }
    }

    public void OnConnectionPointRemoved()
    {
        if (m_OnClickRemoveConnection != null)
        {
            m_OnClickRemoveConnection (this);
        }
    }
}