using System;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class Node
{
    public string m_ID = "";
    public Rect m_Rect;

    public ConnectionPoint m_InPoint;
    public ConnectionPoint m_OutPoint;

    private readonly string m_Title;
    private bool m_IsDragged;
    private bool m_IsSelected;

    private GUIStyle m_Style;
    private readonly GUIStyle m_DefaultNodeStyle;
    private readonly GUIStyle m_SelectedNodeStyle;

    private readonly Action<Node> m_OnRemoveNode;

    // parameterless constructor for xml serialization
    public Node()
    {
    }

    public Node(Vector2 position, GUIStyle nodeStyle, GUIStyle selectedStyle
        , GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint,
        Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode
        , string inPointID, string outPointID, bool isMultipleConnectionAllowed, string id = null)
    {
        m_Rect = new Rect(position.x, position.y, GetWidth(), GetHeight());
        
         m_Style = nodeStyle;
        float connectionPointOffset = m_Rect.height * 0.5f;
        m_InPoint = new ConnectionPoint(this, EConnectionPointType.In, inPointStyle
            , OnClickInPoint, connectionPointOffset, isMultipleConnectionAllowed, inPointID);
        m_OutPoint = new ConnectionPoint(this, EConnectionPointType.Out, outPointStyle
            , OnClickOutPoint, connectionPointOffset, isMultipleConnectionAllowed, outPointID);
        m_DefaultNodeStyle = nodeStyle;
        m_SelectedNodeStyle = selectedStyle;
        m_OnRemoveNode = OnClickRemoveNode;
        m_ID = id ?? Guid.NewGuid().ToString();
    }

    protected virtual float GetWidth()
    {
        return 200f;
    }
    protected virtual float GetHeight()
    {
        return 50f;
    }

    public void Drag(Vector2 delta)
    {
        m_Rect.position += delta;
    }

    public virtual void Draw()
    {
        m_InPoint.Draw();
        m_OutPoint.Draw();
    }

    public virtual bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    GUI.changed = true;
                    if (m_Rect.Contains(e.mousePosition))
                    {
                        m_IsDragged = true;
                        m_IsSelected = true;
                        m_Style = m_SelectedNodeStyle;
                    }
                    else
                    {
                        m_IsSelected = false;
                        m_Style = m_DefaultNodeStyle;
                    }
                }

                if (e.button == 1 && m_IsSelected && m_Rect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                m_IsDragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && m_IsDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }

        return false;
    }

    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

    protected virtual void OnClickRemoveNode()
    {
        if (m_OnRemoveNode != null)
        {
            m_OnRemoveNode(this);
        }
    }

    public virtual void OnConnectionMade(Connection connection) { }
    public virtual void OnConnectionRemove(Connection connection) { }
}